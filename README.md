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

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Supported base encodings](#supported-base-encodings)
- [Maintainers](#maintainers)
- [Contribute](#contribute)
- [License](#license)

## Install

  PM> Install-Package Multiformats.Base

  CLI> dotnet install Multiformats.Base

## Usage
``` csharp
using Multiformats.Base;
using System.Text;

string encoded = Multibase.Encode(MultibaseEncoding.Base32Lower, Encoding.UTF8.GetBytes("hello world"));
// bnbswy3dpeb3w64tmmq
byte[] decoded = Multibase.Decode(encoded, out MultibaseEncoding encoding);
```

## Supported base encodings

* Identity
* Base2
* Base8
* Base10
* Base16
* Base32
  * Lower / Upper
  * Padded Lower / Upper
  * Hex Lower / Upper
  * Hex Padded Lower / Upper
  * Z-Base32
* Base58
  * Bitcoin
  * Flickr
* Base64
  * Unpadded / Padded
  * UrlSafe Unpadded / Padded

## Maintainers

Captain: [@tabrath](https://github.com/tabrath).

## Contribute

Contributions welcome. Please check out [the issues](https://github.com/multiformats/cs-multibase/issues).

Check out our [contributing document](https://github.com/multiformats/multiformats/blob/master/contributing.md) for more information on how we work, and about contributing in general. Please be aware that all interactions related to multiformats are subject to the IPFS [Code of Conduct](https://github.com/ipfs/community/blob/master/code-of-conduct.md).

Small note: If editing the README, please conform to the [standard-readme](https://github.com/RichardLitt/standard-readme) specification.

## License

[MIT](LICENSE) © 2018 Trond Bråthen
