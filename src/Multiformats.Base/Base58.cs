using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Multiformats.Base
{
    internal abstract class Base58 : Multibase
    {
        protected string Encode(byte[] b, char[] alphabet)
        {
            return new string(
                b.TakeWhile(c => c == 0)
                .Select(_ => alphabet[0])
                .Concat(ParseBigInt(b.Aggregate<byte, BigInteger>(0, (current, t) => current * 256 + t), alphabet).Reverse())
                .ToArray());
        }

        private static IEnumerable<char> ParseBigInt(BigInteger intData, char[] alphabet)
        {
            var len = alphabet.Length;
            while (intData > 0)
            {
                var rem = (int)(intData % len);
                intData /= len;
                yield return alphabet[rem];
            }
        }

        private static readonly IDictionary<string, byte[]> _decodeMap = new ConcurrentDictionary<string, byte[]>();

        private static byte[] CreateDecodeMap(string alphabet)
        {
            var map = Enumerable.Range(0, 256).Select(b => (byte)0xFF).ToArray();
            for (var i = 0; i < alphabet.Length; i++)
            {
                map[alphabet[i]] = (byte)i;
            }

            return map;
        }

        private static byte[] GetDecodeMap(string alphabet)
        {
            if (_decodeMap.TryGetValue(alphabet, out var map))
            {
                return map;
            }

            map = CreateDecodeMap(alphabet);
            _decodeMap.Add(alphabet, map);
            return map;
        }

        public override byte[] Decode(string input) => Decode(input, Alphabet);

        public override string Encode(byte[] bytes) => Encode(bytes, Alphabet);

        protected byte[] Decode(string b, char[] alphabet)
        {
            var decodeMap = GetDecodeMap(new string(alphabet));
            var len = alphabet.Length;

            return b.TakeWhile(c => c == alphabet[0])
                .Select(_ => (byte)0)
                .Concat(b.Select(c => decodeMap[c])
                    .Aggregate<byte, BigInteger>(0, (current, c) => current * len + c)
                    .ToByteArray()
                    .Reverse()
                    .SkipWhile(c => c == 0))
                .ToArray();
        }

        protected byte[] DecodeWithSpan(string b, char[] alphabet)
        {
            ReadOnlySpan<byte> decodeMap = GetDecodeMap(new string(alphabet));
            var len = alphabet.Length;

            Span<byte> result = stackalloc byte[b.Length];
            var index = 0;

            var alphabet0 = alphabet[0];
            foreach (var _ in b.TakeWhile(c => c == alphabet0))
            {
                result[index++] = 0;
            }

            BigInteger bigInt = 0;
            foreach (var c in b)
            {
                bigInt = bigInt * len + decodeMap[c];
            }

#if NETCOREAPP2_1
            Span<byte> bigIntArray = stackalloc byte[bigInt.GetByteCount()];
            bigInt.TryWriteBytes(bigIntArray, out _);
#else
            Span<byte> bigIntArray = bigInt.ToByteArray();
#endif
            bigIntArray.Reverse();
            foreach (var item in bigIntArray)
            {
                if (item != 0)
                {
                    result[index++] = item;
                }
            }

            return result.Slice(0, index).ToArray();
        }
    }
}
