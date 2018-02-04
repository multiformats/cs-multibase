using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Multiformats.Base
{
    internal abstract class Base58 : Multibase
    {
        protected string Encode(byte[] b, string alphabet)
        {
            return new string(
                b.TakeWhile(c => c == 0)
                .Select(_ => alphabet[0])
                .Concat(ParseBigInt(b.Aggregate<byte, BigInteger>(0, (current, t) => current * 256 + t), alphabet).Reverse())
                .ToArray());
        }

        private static IEnumerable<char> ParseBigInt(BigInteger intData, string alphabet)
        {
            var len = alphabet.Length;
            while (intData > 0)
            {
                var rem = (int)(intData % len);
                intData /= len;
                yield return alphabet[rem];
            }
        }

        private static Dictionary<string, byte[]> _decodeMap;

        private static byte[] CreateDecodeMap(string alphabet)
        {
            var map = Enumerable.Range(0, 256).Select(b => (byte)0xFF).ToArray();
            for (var i = 0; i < alphabet.Length; i++)
                map[alphabet[i]] = (byte)i;
            return map;
        }

        private static byte[] GetDecodeMap(string alphabet)
        {
            if (_decodeMap == null)
                _decodeMap = new Dictionary<string, byte[]>();

            byte[] map;
            if (_decodeMap.TryGetValue(alphabet, out map))
                return map;

            map = CreateDecodeMap(alphabet);
            _decodeMap.Add(alphabet, map);
            return map;
        }

        protected byte[] Decode(string b, string alphabet)
        {
            var decodeMap = GetDecodeMap(alphabet);
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
    }
}
