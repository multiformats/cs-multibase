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

        // Implementation refers to unsafe code in https://github.com/ssg/SimpleBase/blob/master/src/Base58.cs
        protected string EncodeWithSpanInner(ReadOnlySpan<byte> b)
        {
            int bytesLen = b.Length;
            if (bytesLen == 0)
            {
                return string.Empty;
            }

            ReadOnlySpan<char> alphabetSpan = Alphabet;
            var alphabet0 = alphabetSpan[0];

            const int growthPercentage = 138;
            Span<byte> output = stackalloc byte[((bytesLen * growthPercentage) / 100) + 1];
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

            if (indexForward == bytesLen)
            {
                return new string(alphabet0, indexForward);
            }

            {
                var start = 0;
                for (; start < output.Length && output[start] == 0; start++)
                {
                }

                Span<char> sb = stackalloc char[indexForward + output.Length - start];
                int i;
                for (i = 0; i < indexForward; i++)
                {
                    sb[i] = alphabet0;
                }

                for (var j = start; j < output.Length;)
                {
                    sb[i++] = alphabetSpan[output[j++]];
                }

#if NETCOREAPP2_1
                return new string(sb);
#else
                return new string(sb.ToArray());
#endif
            }
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

        // Implementation refers to unsafe code in https://github.com/ssg/SimpleBase/blob/master/src/Base58.cs
        protected byte[] DecodeWithSpanInner(string b)
        {
            var textLen = b.Length;
            if (textLen == 0)
            {
                return Array.Empty<byte>();
            }

            // https://github.com/bitcoin/bitcoin/blob/master/src/base58.cpp
            const int reductionFactor = 733;

            ReadOnlySpan<byte> decodeMap = CodecMap.Span;
            ReadOnlySpan<char> alphabet = Alphabet;
            var alphabetLen = alphabet.Length;

            Span<byte> result = stackalloc byte[(int)Math.Round(((b.Length * reductionFactor) / 1000.0) + 1)];

            var indexForward = 0;
            var alphabet0 = alphabet[0];
            BigInteger bigInt = 0;
            var isConsecutive = true;
            foreach (var c in b)
            {
                if (isConsecutive)
                {
                    if (c == alphabet0)
                    {
                        indexForward += 1;
                        continue;
                    }
                    else
                    {
                        isConsecutive = false;
                    }
                }

                int carry = decodeMap[c];
                for (var indexBackward = result.Length - 1; indexBackward >= 0; indexBackward--)
                {
                    carry += 58 * result[indexBackward];
                    result[indexBackward] = (byte)carry;
                    carry = carry >> 8;
                }
            }

            if (indexForward == textLen)
            {
                return new byte[indexForward];
            }

            var numberOfLeading0 = 0;
            for (var i = 0; i < result.Length && result[numberOfLeading0] == 0; i++, numberOfLeading0++)
            {
            }

            if (numberOfLeading0 > 0)
            {
                var resultLen = result.Length - numberOfLeading0;
                Span<byte> newResult = stackalloc byte[indexForward + resultLen];
                result.Slice(numberOfLeading0).TryCopyTo(newResult.Slice(indexForward));
                return newResult.ToArray();
            }

            return result.ToArray();
        }
    }
}
