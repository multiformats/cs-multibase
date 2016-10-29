# Multiformats.Base
C# implementation of [multiformats/multibase](https://github.com/multiformats/multibase).

## Usage
``` cs
var encoded = Multibase.Base32.Encode("Hello World");
var decoded = Multibase.Decode(encoded);
```

## Supported base encodings

* Base2
* Base8
* Base10
* Base16
* Base32 (w/o padding, hex, upper/lower, zbase32)
* Base58 (bitcoin, flickr)
* Base64 (w/o padding, url)
