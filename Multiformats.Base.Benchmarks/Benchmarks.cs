using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Jobs;

namespace Multiformats.Base.Benchmarks
{
    [LegacyJitX86Job, LegacyJitX64Job, RyuJitX64Job, AllStatisticsColumn]
    public class Benchmarks
    {
        private byte[] _bytes;
        private Dictionary<MultibaseEncoding, string> _encoded;

        [Setup]
        public void Setup()
        {
            _bytes = new byte[64];
            new Random(Environment.TickCount).NextBytes(_bytes);

            _encoded = new Dictionary<MultibaseEncoding, string>
            {
                {Multibase.Base2, Multibase.Base2.Encode(_bytes)},
                {Multibase.Base8, Multibase.Base8.Encode(_bytes)},
                {Multibase.Base10, Multibase.Base10.Encode(_bytes)},
                {Multibase.Base16, Multibase.Base16.Encode(_bytes)},
                {Multibase.Base32, Multibase.Base32.Encode(_bytes)},
                {Multibase.Base58, Multibase.Base58.Encode(_bytes)},
                {Multibase.Base64, Multibase.Base64.Encode(_bytes)}
            };
        }

        [Benchmark]
        public string Encode_Base2() => Multibase.Base2.Encode(_bytes);

        [Benchmark]
        public byte[] Decode_Base2() => Multibase.Decode(_encoded[Multibase.Base2]);

        [Benchmark]
        public string Encode_Base8() => Multibase.Base8.Encode(_bytes);

        [Benchmark]
        public byte[] Decode_Base8() => Multibase.Decode(_encoded[Multibase.Base8]);

        [Benchmark]
        public string Encode_Base10() => Multibase.Base10.Encode(_bytes);

        [Benchmark]
        public byte[] Decode_Base10() => Multibase.Decode(_encoded[Multibase.Base10]);

        [Benchmark]
        public string Encode_Base16() => Multibase.Base16.Encode(_bytes);

        [Benchmark]
        public byte[] Decode_Base16() => Multibase.Decode(_encoded[Multibase.Base16]);

        [Benchmark]
        public string Encode_Base32() => Multibase.Base32.Encode(_bytes);

        [Benchmark]
        public byte[] Decode_Base32() => Multibase.Decode(_encoded[Multibase.Base32]);

        [Benchmark]
        public string Encode_Base58() => Multibase.Base58.Encode(_bytes);

        [Benchmark]
        public byte[] Decode_Base58() => Multibase.Decode(_encoded[Multibase.Base58]);

        [Benchmark]
        public string Encode_Base64() => Multibase.Base64.Encode(_bytes);

        [Benchmark]
        public byte[] Decode_Base64() => Multibase.Decode(_encoded[Multibase.Base64]);
    }
}
