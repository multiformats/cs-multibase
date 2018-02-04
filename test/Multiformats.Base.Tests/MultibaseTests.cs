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

            var encoded = Multibase.Encode(encoding, buf);
            var decoded = Multibase.Decode(encoded);

            Assert.Equal(decoded, buf);
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

            var decoded = Multibase.Decode(encoded, out string mbEncoding);

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
            using (var csvFile = File.OpenText(_fileName))
            {
                //csvFile.ReadLine();// Delimiter Row: "sep=,". Comment out if not used
                csvFile.ReadLine(); // Headings Row. Comment out if not used
                string line;
                while ((line = csvFile.ReadLine()) != null)
                {
                    var row = line.Split(',').Select(c => c.Trim('"', ' ')).ToArray();
                    yield return ConvertParameters((object[])row, parameterTypes);
                }
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
