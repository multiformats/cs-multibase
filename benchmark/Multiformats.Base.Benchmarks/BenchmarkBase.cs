using System;
using BenchmarkDotNet.Attributes;

namespace Multiformats.Base.Benchmarks
{
    public class BenchmarkBase<TEncoding> where TEncoding : MultibaseEncoding
    {
        private byte[] _bytes;
        private string _encoded;
        private TEncoding _encoding;

        [Setup]
        public void Setup()
        {
            _bytes = new byte[64];
            new Random(Environment.TickCount).NextBytes(_bytes);

            _encoding = Activator.CreateInstance<TEncoding>();
            _encoded = _encoding.Encode(_bytes);
        }

        [Benchmark]
        public string Encode() => _encoding.Encode(_bytes);

        [Benchmark]
        public byte[] Decode() => _encoding.Decode(_encoded);
    }
}