package limiter

import (
	"crypto/sha256"
	"fmt"
	"io"
	"sync"
	"time"
)


type Limiter struct {
	Tokens      int
	Bucket      int
	TimeUnit	int
	LastCheck 	int64
	Key 		string
}


func SHA256(data string) string {
	h256 := sha256.New()
	io.WriteString(h256, data)
	return fmt.Sprintf("%x", h256.Sum(nil))
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

	var hashedUrl = SHA256(fmt.Sprintf("%s", url))
	limiter, exists := i.urls[hashedUrl]

	if !exists {
		i.mu.Unlock()
		return i.AddUrl(hashedUrl)
	}

	i.mu.Unlock()

	return limiter
}

func (i *UrlRateLimiter) AddUrl(url string) * Limiter {
	i.mu.Lock()
	defer i.mu.Unlock()

	limiter := NewLimiter(i.r,i.b, url)

	i.urls[url] = limiter

	return limiter
}


func  NewLimiter(token int, time int, key string) *Limiter{
	return &Limiter{Tokens: token, TimeUnit: time, Key: key}
}

func (t* Limiter) Accept() bool {

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

