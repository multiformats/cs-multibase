# Multiformats.Base

[![](https://img.shields.io/badge/project-multiformats-blue.svg?style=flat-square)](https://github.com/multiformats/multiformats)
[![](https://img.shields.io/badge/freenode-%23ipfs-blue.svg?style=flat-square)](https://webchat.freenode.net/?channels=%23ipfs)
[![Travis CI](https://img.shields.io/travis/multiformats/cs-multibase.svg?style=flat-square&branch=master)](https://travis-ci.org/multiformats/cs-multibase)
[![AppVeyor](https://img.shields.io/appveyor/ci/tabrath/cs-multihash/master.svg?style=flat-square)](https://ci.appveyor.com/project/tabrath/cs-multibase)
[![NuGet](https://buildstats.info/nuget/Multiformats.Base)](https://www.nuget.org/packages/Multiformats.Base/)
[![](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)
[![Codecov](https://img.shields.io/codecov/c/github/multiformats/cs-multibase/master.svg?style=flat-square)](https://codecov.io/gh/multiformats/cs-multibase)
[![Libraries.io](https://img.shields.io/librariesio/github/multiformats/cs-multibase.svg?style=flat-square)](https://libraries.io/github/multiformats/cs-multibase)

> C# implementation of [multiformats/multibase](https://github.com/multiformats/multibase).

As stated in the specs, multibase encoded strings are prefixed with an identifier for the base it's encoded in.
There's a lot of [discussion going on](https://github.com/multiformats/multibase/issues) about this spec, so consider this work in progress.

If you don't care about the multibase part, you can use the EncodeRaw/DecodeRaw methods to just encode/decode to bases without the prefix.

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Supported base encodings](#supported-base-encodings)
- [Maintainers](#maintainers)
- [Contribute](#contribute)
- [License](#license)
- [Third parties](#third-parties)

## Install

  PM> Install-Package Multiformats.Base

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

All `Encode` methods can be called with only one parameter, the data to be encoded, which will use the default options regarding padding and letter case.
Some encodings have overloaded versions of `Encode` to let you specify additional options, like Base58 lets you select between Bitcoin and Flickr alphabet.

``` cs
var encoded = Multibase.Base58.Encode(new byte[] {0,1,2,3}, Base58Alphabet.Flickr);
```

For base 2, 8 and 10 you can specify your own separator, default is space.

``` cs
// separator is dash ('-')
var encoded = Multibase.Base8.Encode(new byte[] {0,1,2,3}, '-');
// = "7150-145-154-154-157-40-167-157-162-154-144"
```

Be aware that you can not pass in additional parameters to `Multibase.Decode` to decode, so you have to know what encoding it's in and call the base encoder directly if you use custom separators.

## Supported base encodings

* Base2
* Base8
* Base10
* Base16
* Base32 (with and without padding, hex, upper/lower, zbase32)
  * Default: no padding, lowercase
* Base58 (bitcoin, flickr)
  * Default: bitcoin
* Base64 (with and without padding, url-safe)
  * Default: no padding, not url-safe

## Maintainers

Captain: [@tabrath](https://github.com/tabrath).

## Contribute

Contributions welcome. Please check out [the issues](https://github.com/multiformats/cs-multibase/issues).

Check out our [contributing document](https://github.com/multiformats/multiformats/blob/master/contributing.md) for more information on how we work, and about contributing in general. Please be aware that all interactions related to multiformats are subject to the IPFS [Code of Conduct](https://github.com/ipfs/community/blob/master/code-of-conduct.md).

Small note: If editing the README, please conform to the [standard-readme](https://github.com/RichardLitt/standard-readme) specification.

## License

[MIT](LICENSE) © 2016 Trond Bråthen

# Third parties

[SimpleBase](https://github.com/ssg/SimpleBase) Apache 2.0
[BaseNEncodings.Net](https://github.com/wujikui/BaseNEncodings.Net) Apache 2.0
