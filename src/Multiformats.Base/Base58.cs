using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Multiformats.Base
{
    internal abstract class Base58 : Multibase
    {
        protected abstract string AlphabetStr { get; }

        protected abstract ReadOnlyMemory<byte> CodecMap { get; }

        public override byte[] Decode(string input) => DecodeInner(input);

        public override string Encode(byte[] bytes) => EncodeInner(bytes);

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

        protected static byte[] CreateDecodeMap(string alphabet)
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

        protected string EncodeInner(byte[] b)
        {
            var alphabet0 = Alphabet[0];
            return new string(
                b.TakeWhile(c => c == 0)
                .Select(_ => alphabet0)
                .Concat(ParseBigInt(b.Aggregate<byte, BigInteger>(0, (current, t) => current * 256 + t), Alphabet).Reverse())
                .ToArray());
        }

        protected string EncodeWithSpanInner(ReadOnlySpan<byte> b)
        {
            ReadOnlySpan<char> alphabetSpan = Alphabet;
            var alphabet0 = alphabetSpan[0];

            const int growthPercentage = 138;
            Span<byte> output = stackalloc byte[((b.Length * growthPercentage) / 100) + 1];
            var isConsecutive = true;

            int length = 0;
            var indexForward = 0;
            foreach (var c in b)
            {
                if (isConsecutive)
                {
                    if (c == 0)
                    {
                        indexForward += 1;
                        continue;
                    }
                    else
                    {
                        isConsecutive = false;
                    }
                }

                int carry = c;
                int i = 0;
                for (var indexBackward = output.Length - 1; (c != 0 || i < length) && indexBackward >= 0; i++, indexBackward--)
                {
                    carry += (((int)output[indexBackward]) << 8);
                    output[indexBackward] = (byte)(carry % 58);
                    carry /= 58;
                }

                length = i;
            }

            if (indexForward == b.Length)
            {
                return new string(alphabet0, indexForward);
            }

            var sb = new StringBuilder(new string(alphabet0, indexForward), indexForward + output.Length);

            var start = 0;
            for (; start < output.Length && output[start] == 0; start++)
            {
            }

            for (var i = start; i < output.Length; i++)
            {
                sb.Append(alphabetSpan[output[i]]);
            }

            return sb.ToString();
        }

        protected byte[] DecodeInner(string b)
        {
            var decodeMap = GetDecodeMap(new string(Alphabet));
            var len = Alphabet.Length;

            var alphabet0 = Alphabet[0];
            return b.TakeWhile(c => c == alphabet0)
                .Select(_ => (byte)0)
                .Concat(b.Select(c => decodeMap[c])
                    .Aggregate<byte, BigInteger>(0, (current, c) => current * len + c)
                    .ToByteArray()
                    .Reverse()
                    .SkipWhile(c => c == 0))
                .ToArray();
        }

        protected byte[] DecodeWithSpanInner(string b)
        {
            // https://github.com/bitcoin/bitcoin/blob/master/src/base58.cpp
            const int reductionFactor = 733;

            ReadOnlySpan<byte> decodeMap = CodecMap.Span;
            ReadOnlySpan<char> alphabet = Alphabet;
            var len = alphabet.Length;

            Span<byte> result = stackalloc byte[(int)Math.Round(((b.Length * reductionFactor) / 1000.0) + 1)];
            var index = 0;

            var alphabet0 = alphabet[0];
            BigInteger bigInt = 0;
            var isConsecutive = true;
            foreach (var c in b)
            {
                if (isConsecutive)
                {
                    if (c == alphabet0)
                    {
                        result[index++] = 0;
                    }
                    else
                    {
                        isConsecutive = false;
                    }
                }

                bigInt = bigInt * len + decodeMap[c];
            }

#if NETCOREAPP2_1
            Span<byte> bigIntArray = stackalloc byte[bigInt.GetByteCount()];
            bigInt.TryWriteBytes(bigIntArray, out _);
#else
            Span<byte> bigIntArray = bigInt.ToByteArray();
#endif
            bigIntArray.Reverse();
            isConsecutive = true;
            foreach (var item in bigIntArray)
            {
                if (isConsecutive)
                {
                    if (item == 0)
                    {
                        continue;
                    }
                    else
                    {
                        isConsecutive = false;
                    }
                }

                result[index++] = item;
            }

            return result.Slice(0, index).ToArray();
        }
    }
}
