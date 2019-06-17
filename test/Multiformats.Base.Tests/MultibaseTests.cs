using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace Multiformats.Base.Tests
{
    public class MultibaseTests
    {
        [Fact]
        public void Decode_GivenInvalidChars_ThrowsInvalidOperationException()
        {
            // prefix 0 - base2
            // value 99 - invalid chars

            Assert.Throws<InvalidOperationException>(() => Multibase.Decode("099", out string _));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Decode_GivenEmptyInput_ThrowsArgumentNullException(string value)
        {
            Assert.Throws<ArgumentNullException>(() => Multibase.Decode(value, out string _));
        }

        [Fact]
        public void Decode_GivenUnknownPrefix_ThrowsUnsupportedException()
        {
            Assert.Throws<NotSupportedException>(() => Multibase.Decode("ø", out string _));
        }

        [Fact]
        public void DecodeRaw_GivenUnknownEncoding_ThrowsUnsupportedException()
        {
            Assert.Throws<NotSupportedException>(() => Multibase.DecodeRaw((MultibaseEncoding)0x2000, "abab"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DecodeRaw_GivenEmptyInput_ThrowsArgumentNullException(string value)
        {
            Assert.Throws<ArgumentNullException>(() => Multibase.DecodeRaw(MultibaseEncoding.Identity, value));
        }

        [Fact]
        public void Encode_GivenEmptyBytes_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Multibase.Encode(MultibaseEncoding.Base2, new byte[] { }));
        }

        [Fact]
        public void Encode_GivenNullBytes_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Multibase.Encode(MultibaseEncoding.Base2, null));
        }

        [Fact]
        public void Encode_GivenUnknownEncoding_ThrowsUnsupportedException()
        {
            Assert.Throws<NotSupportedException>(() => Multibase.Encode((MultibaseEncoding)0x2000, new byte[] { 0, 1, 2, 3 }));
        }

        [Theory]
        [InlineData(MultibaseEncoding.Identity)]
        [InlineData(MultibaseEncoding.Base2)]
        [InlineData(MultibaseEncoding.Base8)]
        [InlineData(MultibaseEncoding.Base10)]
        [InlineData(MultibaseEncoding.Base16Lower)]
        [InlineData(MultibaseEncoding.Base16Upper)]
        [InlineData(MultibaseEncoding.Base32Lower)]
        [InlineData(MultibaseEncoding.Base32Upper)]
        [InlineData(MultibaseEncoding.Base32PaddedLower)]
        [InlineData(MultibaseEncoding.Base32PaddedUpper)]
        [InlineData(MultibaseEncoding.Base32HexLower)]
        [InlineData(MultibaseEncoding.Base32HexUpper)]
        [InlineData(MultibaseEncoding.Base32HexPaddedLower)]
        [InlineData(MultibaseEncoding.Base32HexPaddedUpper)]
        [InlineData(MultibaseEncoding.Base58Btc)]
        [InlineData(MultibaseEncoding.Base58Flickr)]
        [InlineData(MultibaseEncoding.Base64)]
        [InlineData(MultibaseEncoding.Base64Padded)]
        [InlineData(MultibaseEncoding.Base64Url)]
        [InlineData(MultibaseEncoding.Base64UrlPadded)]
        public void TestRoundTrip(MultibaseEncoding encoding)
        {
            var rand = new Random(Environment.TickCount);
            var buf = new byte[rand.Next(16, 256)];
            rand.NextBytes(buf);

            var encoded = Multibase.EncodeRaw(encoding, buf);
            var decoded = Multibase.DecodeRaw(encoding, encoded);

            Assert.Equal(decoded, buf);
        }

        [Theory]
        [InlineData(MultibaseEncoding.Identity)]
        [InlineData(MultibaseEncoding.Base2)]
        [InlineData(MultibaseEncoding.Base8)]
        [InlineData(MultibaseEncoding.Base10)]
        [InlineData(MultibaseEncoding.Base16Lower)]
        [InlineData(MultibaseEncoding.Base16Upper)]
        [InlineData(MultibaseEncoding.Base32Lower)]
        [InlineData(MultibaseEncoding.Base32Upper)]
        [InlineData(MultibaseEncoding.Base32PaddedLower)]
        [InlineData(MultibaseEncoding.Base32PaddedUpper)]
        [InlineData(MultibaseEncoding.Base32HexLower)]
        [InlineData(MultibaseEncoding.Base32HexUpper)]
        [InlineData(MultibaseEncoding.Base32HexPaddedLower)]
        [InlineData(MultibaseEncoding.Base32HexPaddedUpper)]
        [InlineData(MultibaseEncoding.Base58Btc)]
        [InlineData(MultibaseEncoding.Base58Flickr)]
        [InlineData(MultibaseEncoding.Base64)]
        [InlineData(MultibaseEncoding.Base64Padded)]
        [InlineData(MultibaseEncoding.Base64Url)]
        [InlineData(MultibaseEncoding.Base64UrlPadded)]
        public void TestRoundTripRaw(MultibaseEncoding encoding)
        {
            var rand = new Random(Environment.TickCount);
            var buf = new byte[rand.Next(16, 256)];
            rand.NextBytes(buf);

            var encoded = Multibase.Encode(encoding, buf);
            var decoded = Multibase.Decode(encoded, out MultibaseEncoding decodedEncoding);

            Assert.Equal(encoding, decodedEncoding);
            Assert.Equal(decoded, buf);
        }

        [Theory]
        [InlineData("001000100011001010110001101100101011011100111010001110010011000010110110001101001011110100110010100100000011001010111011001100101011100100111100101110100011010000110100101101110011001110010000100100001", MultibaseEncoding.Base2)]
        [InlineData("71043126154533472162302661513646244031273145344745643206455631620441", MultibaseEncoding.Base8)]
        [InlineData("f446563656e7472616c697a652065766572797468696e672121", MultibaseEncoding.Base16Lower)]
        [InlineData("F446563656E7472616C697A652065766572797468696E672121", MultibaseEncoding.Base16Upper)]
        [InlineData("birswgzloorzgc3djpjssazlwmvzhs5dinfxgoijb", MultibaseEncoding.Base32Lower)]
        [InlineData("BIRSWGZLOORZGC3DJPJSSAZLWMVZHS5DINFXGOIJB", MultibaseEncoding.Base32Upper)]
        [InlineData("v8him6pbeehp62r39f9ii0pbmclp7it38d5n6e891", MultibaseEncoding.Base32HexLower)]
        [InlineData("V8HIM6PBEEHP62R39F9II0PBMCLP7IT38D5N6E891", MultibaseEncoding.Base32HexUpper)]
        [InlineData("cirswgzloorzgc3djpjssazlwmvzhs5dinfxgoijb", MultibaseEncoding.Base32PaddedLower)]
        [InlineData("CIRSWGZLOORZGC3DJPJSSAZLWMVZHS5DINFXGOIJB", MultibaseEncoding.Base32PaddedUpper)]
        [InlineData("t8him6pbeehp62r39f9ii0pbmclp7it38d5n6e891", MultibaseEncoding.Base32HexPaddedLower)]
        [InlineData("T8HIM6PBEEHP62R39F9II0PBMCLP7IT38D5N6E891", MultibaseEncoding.Base32HexPaddedUpper)]
        [InlineData("het1sg3mqqt3gn5djxj11y3msci3817depfzgqejb", MultibaseEncoding.Base32Z)]
        [InlineData("Ztwe7gVTeK8wswS1gf8hrgAua9fcw9reboD", MultibaseEncoding.Base58Flickr)]
        [InlineData("zUXE7GvtEk8XTXs1GF8HSGbVA9FCX9SEBPe", MultibaseEncoding.Base58Btc)]
        [InlineData("mRGVjZW50cmFsaXplIGV2ZXJ5dGhpbmchIQ", MultibaseEncoding.Base64)]
        [InlineData("MRGVjZW50cmFsaXplIGV2ZXJ5dGhpbmchIQ==", MultibaseEncoding.Base64Padded)]
        [InlineData("uRGVjZW50cmFsaXplIGV2ZXJ5dGhpbmchIQ", MultibaseEncoding.Base64Url)]
        [InlineData("URGVjZW50cmFsaXplIGV2ZXJ5dGhpbmchIQ==", MultibaseEncoding.Base64UrlPadded)]
        public void TestTryDecoding_GivenValidEncodedInput_Prefixed(string input, MultibaseEncoding encoding)
        {
            var expected = "Decentralize everything!!";
            var result = Multibase.TryDecode(input, out var decodedEncoding, out var decodedBytes);

            Assert.True(result);
            Assert.Equal(encoding, decodedEncoding);
            Assert.Equal(expected, Encoding.UTF8.GetString(decodedBytes));
        }

        [Theory]
        [InlineData("01000100011001010110001101100101011011100111010001110010011000010110110001101001011110100110010100100000011001010111011001100101011100100111100101110100011010000110100101101110011001110010000100100001", MultibaseEncoding.Base2)]
        [InlineData("1043126154533472162302661513646244031273145344745643206455631620441", MultibaseEncoding.Base8)]
        [InlineData("446563656e7472616c697a652065766572797468696e672121", MultibaseEncoding.Base16Lower)]
        [InlineData("446563656E7472616C697A652065766572797468696E672121", MultibaseEncoding.Base16Upper)]
        [InlineData("irswgzloorzgc3djpjssazlwmvzhs5dinfxgoijb", MultibaseEncoding.Base32Lower)]
        [InlineData("IRSWGZLOORZGC3DJPJSSAZLWMVZHS5DINFXGOIJB", MultibaseEncoding.Base32Upper)]
        [InlineData("8him6pbeehp62r39f9ii0pbmclp7it38d5n6e891", MultibaseEncoding.Base32HexLower)]
        [InlineData("8HIM6PBEEHP62R39F9II0PBMCLP7IT38D5N6E891", MultibaseEncoding.Base32HexUpper)]
        [InlineData("et1sg3mqqt3gn5djxj11y3msci3817depfzgqejb", MultibaseEncoding.Base32Z)]
        [InlineData("RGVjZW50cmFsaXplIGV2ZXJ5dGhpbmchIQ==", MultibaseEncoding.Base64Padded)]
        public void TestTryDecoding_GivenValidEncodedInput_Unprefixed(string input, MultibaseEncoding encoding)
        {
            var expected = "Decentralize everything!!";
            var result = Multibase.TryDecode(input, out var decodedEncoding, out var decodedBytes);

            Assert.True(result);
            Assert.Equal(encoding, decodedEncoding);
            Assert.Equal(expected, Encoding.UTF8.GetString(decodedBytes));
        }

        // Official test vectors
        private static void TestVector(string encoding, string encoded, string expected)
        {
            var decoded = Multibase.Decode(encoded, out string mbEncoding);

            Assert.Equal(encoding, mbEncoding);
            Assert.Equal(expected, Encoding.UTF8.GetString(decoded));

            var rencoded = Multibase.Encode(mbEncoding, decoded);

            Assert.Equal(encoded, rencoded);
        }

        [Theory]
        [CsvData("test1.csv")]
        public void TestVector_1(string encoding, string encoded)
        {
            var expected = "Decentralize everything!!";

            TestVector(encoding, encoded, expected);
        }

        [Theory]
        [CsvData("test2.csv")]
        public void TestVector_2(string encoding, string encoded)
        {
            var expected = "yes mani !";

            TestVector(encoding, encoded, expected);
        }

        [Theory]
        [CsvData("test3.csv")]
        public void TestVector_3(string encoding, string encoded)
        {
            var expected = "hello world";

            TestVector(encoding, encoded, expected);
        }

        [Theory]
        [CsvData("test4.csv")]
        public void TestVector_4(string encoding, string encoded)
        {
            var expected = "\x00yes mani !";

            TestVector(encoding, encoded, expected);
        }

        [Theory]
        [CsvData("test5.csv")]
        public void TestVector_5(string encoding, string encoded)
        {
            var expected = "\x00\x00yes mani !";

            TestVector(encoding, encoded, expected);
        }

        [Theory]
        [CsvData("test6.csv")]
        public void TestVector_6(string encoding, string encoded)
        {
            var expected = "hello world";

            var decoded = Multibase.Decode(encoded, out string mbEncoding, false);

            Assert.Equal(encoding, mbEncoding);
            Assert.Equal(expected, Encoding.UTF8.GetString(decoded));
        }

        [Theory]
        [InlineData("1thX6LZfHDZZKUs92febWaf4WJZnsKRiVwJusXxB7L")]
        [InlineData("11Ldp")]
        [InlineData("1FESiat4YpNeoYhW3Lp7sW1T6WydcW7vcE")]
        [InlineData("1mJKRNca45GU2JQuHZqZjHFNktaqAs7gh")]
        [InlineData("17f1hgANcLE5bQhAGRgnBaLTTs23rK4VGVKuFQ")]
        [InlineData("11111")]
        [InlineData("2vgLdhi")]
        [InlineData("3CSwN61PP")]
        [InlineData("1")]
        [InlineData("a")]
        [InlineData("zUXE7GvtEk8XTXs1GF8HSGbVA9FCX9SEBPe")]
        [InlineData("UT5UZnN38L6YtzvQwN257BNBTwkNYVHTHjMzndpyGxLUwNy2p4EoQHvn8yYAxxJLMEkmDUKxCUEhxL")]
        [InlineData("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")]
        public void Test_Base58BTC_Parity(string encoded)
        {
            var b1 = Multibase.Base58.Decode(encoded);
            var b2 = Multibase.Base58_V2.Decode(encoded);
            var b3 = SimpleBase.Base58.Bitcoin.Decode(encoded);
            Assert.Equal(b1.Length, b3.Length);
            Assert.Equal(b1.Length, b2.Length);
            Assert.True(b3.SequenceEqual(b1));
            Assert.True(b1.SequenceEqual(b2));
            Assert.Equal(Multibase.Base58.Encode(b1), encoded);
            Assert.Equal(Multibase.Base58_V2.Encode(b2), encoded);
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CsvDataAttribute : DataAttribute
    {
        private readonly string _fileName;
        public CsvDataAttribute(string fileName)
        {
            _fileName = fileName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var pars = testMethod.GetParameters();
            var parameterTypes = pars.Select(par => par.ParameterType).ToArray();
            foreach (var line in File.ReadLines(_fileName).Skip(1))
            {
                //csvFile.ReadLine();// Delimiter Row: "sep=,". Comment out if not used
                var row = line.Split(',').Select(c => c.Trim('"', ' ')).ToArray();
                yield return ConvertParameters(row, parameterTypes);
            }
        }

        private static object[] ConvertParameters(IReadOnlyList<object> values, IReadOnlyList<Type> parameterTypes)
        {
            var result = new object[parameterTypes.Count];
            for (var idx = 0; idx < parameterTypes.Count; idx++)
            {
                result[idx] = ConvertParameter(values[idx], parameterTypes[idx]);
            }

            return result;
        }

        private static object ConvertParameter(object parameter, Type parameterType)
        {
            return parameterType == typeof(int) ? Convert.ToInt32(parameter) : parameter;
        }
    }
}
