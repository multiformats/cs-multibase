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
