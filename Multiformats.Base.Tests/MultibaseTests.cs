using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Multiformats.Base.Tests
{
    [TestFixture]
    public class MultibaseTests
    {
        [Test]
        public void RoundTrip_Base2() => TestRoundTrip(Multibase.Base2.Encode);

        [Test]
        public void RoundTrip_Base8() => TestRoundTrip(Multibase.Base8.Encode);

        [Test]
        public void RoundTrip_Base10() => TestRoundTrip(Multibase.Base10.Encode);

        [Test]
        public void RoundTrip_Base16Upper() => TestRoundTrip(Multibase.Base16.Encode);

        [Test]
        public void RoundTrip_Base16Lower() => TestRoundTrip(b => Multibase.Base16.Encode(b, false));

        [Test]
        public void RoundTrip_Base32() => TestRoundTrip(Multibase.Base32.Encode);

        [Test]
        public void RoundTrip_Base32Padding() => TestRoundTrip(b => Multibase.Base32.Encode(b, true, false, false, false));

        [Test]
        public void RoundTrip_Base32Hex() => TestRoundTrip(b => Multibase.Base32.Encode(b, false, true, true, false));

        [Test]
        public void RoundTrip_Base32HexPadding() => TestRoundTrip(b => Multibase.Base32.Encode(b, true, true, true, false));

        [Test]
        public void RoundTrip_Base32Z() => TestRoundTrip(b => Multibase.Base32.Encode(b, false, false, false, true));

        [Test]
        public void RoundTrip_Base58Bitcoin() => TestRoundTrip(b => Multibase.Base58.Encode(b));

        [Test]
        public void RoundTrip_Base58Flickr() => TestRoundTrip(b => Multibase.Base58.Encode(b, false));

        [Test]
        public void RoundTrip_Base64() => TestRoundTrip(Multibase.Base64.Encode);

        [Test]
        public void RoundTrip_Base64Padding() => TestRoundTrip(b => Multibase.Base64.Encode(b, true, false));

        [Test]
        public void RoundTrip_Base64Url() => TestRoundTrip(b => Multibase.Base64.Encode(b, false, true));

        [Test]
        public void RoundTrip_Base64UrlPadding() => TestRoundTrip(b => Multibase.Base64.Encode(b, true, true));

        private static void TestRoundTrip(Func<byte[], string> encode)
        {
            var rand = new Random(Environment.TickCount);
            var buf = new byte[rand.Next(16, 256)];
            rand.NextBytes(buf);

            var encoded = encode(buf);
            var decoded = Multibase.Decode(encoded);

            Assert.That(decoded, Is.EqualTo(buf));
        }

        [Test]
        public void EncodeDecode_Base2() => TestEncodeDecode(Multibase.Base2.Encode, "1101000 1100101 1101100 1101100 1101111 100000 1110111 1101111 1110010 1101100 1100100");

        [Test]
        public void EncodeDecode_Base8() => TestEncodeDecode(Multibase.Base8.Encode, "150 145 154 154 157 40 167 157 162 154 144");

        [Test]
        public void EncodeDecode_Base10() => TestEncodeDecode(Multibase.Base10.Encode, "104 101 108 108 111 32 119 111 114 108 100");

        [Test]
        public void EncodeDecode_Base16Upper() => TestEncodeDecode(Multibase.Base16.Encode, "68656C6C6F20776F726C64");

        [Test]
        public void EncodeDecode_Base16Lower() => TestEncodeDecode(b => Multibase.Base16.Encode(b, false), "68656c6c6f20776f726c64");

        [Test]
        public void EncodeDecode_Base32() => TestEncodeDecode(Multibase.Base32.Encode, "NBSWY3DPEB3W64TMMQ");

        [Test]
        public void EncodeDecode_Base32Padding() => TestEncodeDecode(b => Multibase.Base32.Encode(b, true), "NBSWY3DPEB3W64TMMQ======");

        [Test]
        public void EncodeDecode_Base32Hex() => TestEncodeDecode(b => Multibase.Base32.Encode(b, false, hex: true), "D1IMOR3F41RMUSJCCG");

        [Test]
        public void EncodeDecode_Base32HexPadding() => TestEncodeDecode(b => Multibase.Base32.Encode(b, true, hex: true), "D1IMOR3F41RMUSJCCG======");

        [Test]
        public void EncodeDecode_Base32Z() => TestEncodeDecode(b => Multibase.Base32.Encode(b, false, zbase: true), "pb1sa5dxrb5s6hucco");

        [Test]
        public void EncodeDecode_Base58Bitcoin() => TestEncodeDecode(b => Multibase.Base58.Encode(b), "StV1DL6CwTryKyV");

        [Test]
        public void EncodeDecode_Base58Flickr() => TestEncodeDecode(b => Multibase.Base58.Encode(b, false), "rTu1dk6cWsRYjYu");

        [Test]
        public void EncodeDecode_Base64() => TestEncodeDecode(Multibase.Base64.Encode, "aGVsbG8gd29ybGQ");

        [Test]
        public void EncodeDecode_Base64Padding() => TestEncodeDecode(b => Multibase.Base64.Encode(b, true), "aGVsbG8gd29ybGQ=");

        [Test]
        public void EncodeDecode_Base64Url() => TestEncodeDecode(b => Multibase.Base64.Encode(b, false, url: true), "aGVsbG8gd29ybGQ");

        [Test]
        public void EncodeDecode_Base64UrlPadding() => TestEncodeDecode(b => Multibase.Base64.Encode(b, true, url: true), "aGVsbG8gd29ybGQ=");

        private static void TestEncodeDecode(Func<byte[], string> encode, string expected)
        {
            var bytes = Encoding.UTF8.GetBytes("hello world");
            var encoded = encode(bytes);

            Assert.That(encoded.Substring(1), Is.EqualTo(expected));

            var decoded = Multibase.Decode(encoded);
            Assert.That(decoded, Is.EqualTo(bytes));
        }
    }
}
