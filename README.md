# Multiformats.Base

[![Build Status](https://travis-ci.org/tabrath/cs-multibase.svg?branch=master)](https://travis-ci.org/tabrath/cs-multibase)
[![Build status](https://ci.appveyor.com/api/projects/status/w93pidw0npmvn5g4?svg=true)](https://ci.appveyor.com/project/tabrath/cs-multibase)
[![NuGet Badge](https://buildstats.info/nuget/Multiformats.Base)](https://www.nuget.org/packages/Multiformats.Base/)

C# implementation of [multiformats/multibase](https://github.com/multiformats/multibase). As stated in the specs, multibase encoded strings are prefixed with an identifier for the base it's encoded in. There's a lot of [discussion going](https://github.com/multiformats/multibase/issues) on about this spec, so consider this work in progress.

If you don't care about the multibase part, you can use the EncodeRaw/DecodeRaw methods to just encode/decode to bases without the prefix.

## Usage
``` cs
var encoded = Multibase.Base32.Encode(new byte[] {0,1,2,3);
var encoded = Multibase.Encode<Base32Encoding>(new byte[] {0,1,2,3);

// raw encode without multibase prefix
var rawEncoded = Multibase.Encode<Base32Encoding>(new byte[] {0,1,2,3);

var decoded = Multibase.Decode(encoded);

// decode and get encoding
var decoded = Multibase.Decode(encoded, out encoding);

// raw decode without multibase prefix
var rawDecoded = Multibase.DecodeRaw<Base32Encoding>(encoded);
```

All `Encode` methods can be called with only one parameter, the data to be encoded, which will use the default options regarding padding and letter case. Those encodings have overloaded versions of `Encode` to let you specify those parameters.

``` cs
var encoded = Multibase.Base58.Encode(new byte[] {0,1,2,3}, Base58Alphabet.Flickr);
```

## Supported base encodings

* Base2
* Base8
* Base10
* Base16
* Base32 (with and without padding, hex, upper/lower, zbase32)
* Base58 (bitcoin, flickr)
* Base64 (¨with and without padding, url)
