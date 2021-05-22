package limiter

import (
	"testing"
)

// unit testing
func TestSHA256(t *testing.T) {
	data := []struct {
		url string
		expectedHash string
	}{
		{"http://example.com","f0e6a6a97042a4f1f1c87f5f7d44315b2d852c2df5c7991cc66241bf7072d1c4"},
		{"http://example.com","123"},
	}
	for _, data := range data {
		hashed := SHA256(data.url)
		if hashed != data.expectedHash {
			t.Errorf("Hash of (%s) was incorrect, got: %s, want: %s.", data.url, hashed, data.expectedHash)
		}
	}
}