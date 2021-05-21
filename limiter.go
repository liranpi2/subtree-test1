package limiter

import (
	"sync"
	"time"
)


type Limiter struct {
	Tokens      int
	Bucket      int
	TimeUnit	int
	LastCheck 	int64
}


type userDB interface {
	userRoleByID(id string) string
}

type UrlRateLimiter struct {
	urls map[string]* Limiter
	mu  *sync.RWMutex
	r   int
	b   int
}

func NewUrlRateLimiter(r int, b int) * UrlRateLimiter {
	i := &UrlRateLimiter{
		urls: make(map[string]* Limiter),
		mu:  &sync.RWMutex{},
		r:   r,
		b:   b,
	}
	return i
}



func (i *UrlRateLimiter) GetLimiter(url string) *  Limiter{
	i.mu.Lock()
	limiter, exists := i.urls[url]

	if !exists {
		i.mu.Unlock()
		return i.AddUrl(url)
	}

	i.mu.Unlock()

	return limiter
}

func (i *UrlRateLimiter) AddUrl(url string) * Limiter {
	i.mu.Lock()
	defer i.mu.Unlock()

	limiter := NewLimiter(i.r,i.b)
	i.urls[url] = limiter

	return limiter
}


func  NewLimiter(token int, time int) *Limiter{
	return &Limiter{Tokens: token, TimeUnit: time}
}

func (t* Limiter) Accept(url string) bool {

	// get now (total seconds from 1970)
	var current = time.Now().Unix()

	// time elapsed between consecutive calls
	var elapsed = int64(current) - t.LastCheck

	var ratio = float64(t.Tokens) / float64(t.TimeUnit)

	// update last time to now
	t.LastCheck = current

	// set limit
	t.Bucket = t.Bucket + int(float64(elapsed) * ratio)

	if t.Bucket > t.Tokens {
		// update bounds
		t.Bucket = t.Tokens
	}
	if t.Bucket < 1 {
		return false
	} else {
		t.Bucket = t.Bucket-1
		return true
	}
}

