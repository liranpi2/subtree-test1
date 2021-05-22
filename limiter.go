package limiter

import (
	"hash/fnv"
	"sync"
	"time"
)

type Limiter struct {
	Timestamps []int64
	Ttl        int64
	Counter    int
	mu         *sync.Mutex
}

func hash64(data string) uint64 {
	var a = fnv.New64()
	a.Write([]byte(data))
	return a.Sum64()
}

type UrlRateLimiter struct {
	urls map[uint64]*Limiter
	mu   *sync.Mutex
	size int
	ttl  int64
}

func NewUrlRateLimiter(size int, ttl int64) *UrlRateLimiter {
	i := &UrlRateLimiter{
		urls: make(map[uint64]*Limiter),
		mu:   &sync.Mutex{},
		size: size,
		ttl:  ttl,
	}
	return i
}

func (i *UrlRateLimiter) GetLimiter(url string) *Limiter {
	var hashedUrl = hash64(url)
	limiter, exists := i.urls[hashedUrl]

	i.mu.Lock()
	defer i.mu.Unlock()
	if !exists {
		return i.AddUrl(hashedUrl)
	}

	return limiter
}

func (i *UrlRateLimiter) AddUrl(url uint64) *Limiter {
	limiter := NewLimiter(i.size, i.ttl)
	i.urls[url] = limiter

	return limiter
}

func NewLimiter(size int, ttl int64) *Limiter {
	return &Limiter{Timestamps: make([]int64, size), Ttl: ttl, mu: &sync.Mutex{}}
}

func (limiter *Limiter) Accept() bool {

	limiter.mu.Lock()
	defer limiter.mu.Unlock()

	// get now (total ms from 1970)
	var currentTime = time.Now().UnixNano() / 1_000_000
	var lastRequest = limiter.Timestamps[limiter.Counter]

	var interval = currentTime - lastRequest

	if interval < limiter.Ttl {
		return false
	}

	limiter.Timestamps[limiter.Counter] = currentTime
	limiter.Counter = (limiter.Counter + 1) % len(limiter.Timestamps)

	return true
}
