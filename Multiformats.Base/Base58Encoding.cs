using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using SimpleBase;

namespace Multiformats.Base
{
    public class Base58Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { 'Z', 'z' };
        public override char DefaultIdentifier => Identifiers[1];

        public override string Encode(byte[] data) => Encode(data, Base58Alphabet.Bitcoin);

        /// <summary>
        /// Encode bytes
        /// </summary>
        /// <param name="data">Input data</param>
        /// <param name="alphabet">Alphabet to use</param>
        /// <returns>Encoded string</returns>
        public string Encode(byte[] data, Base58Alphabet alphabet)
        {
            return (alphabet == Base58Alphabet.Bitcoin ? 'z' : 'Z') +
                   (alphabet == Base58Alphabet.Bitcoin
                       ? EncodeAlphabet(data, BitcoinAlphabet)
                       : EncodeAlphabet(data, FlickrAlphabet));
        }

        public override byte[] Decode(string str)
        {
            var id = str[0];
            str = str.Substring(1);

            switch (id)
            {
                case 'Z':
                    return DecodeAlphabet(str, FlickrAlphabet);
                case 'z':
                    return DecodeAlphabet(str, BitcoinAlphabet);
                default:
                    throw new NotSupportedException($"Unsupported identifier: {id}");
            }
        }

        private const string BitcoinAlphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        private const string FlickrAlphabet = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";

        private static string EncodeAlphabet(byte[] b, string alphabet)
        {
            return
                new string(
                    b.TakeWhile(c => c == 0)
                        .Select(_ => alphabet[0])
                        .Concat(ParseBigInt(b.Aggregate<byte, BigInteger>(0, (current, t) => current * 256 + t),
                            alphabet))
                        .Reverse()
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

        private static byte[] DecodeAlphabet(string b, string alphabet)
        {
            var decodeMap = GetDecodeMap(alphabet);
            var len = alphabet.Length;

            return b.TakeWhile(c => c == alphabet[0])
                .Select(_ => (byte) 0)
                .Concat(b.Select(c => decodeMap[c])
                    .Aggregate<byte, BigInteger>(0, (current, c) => current * len + c)
                    .ToByteArray()
                    .Reverse()
                    .SkipWhile(c => c == 0))
                .ToArray();
        }
    }

    public enum Base58Alphabet
    {
        Bitcoin,
        Flickr
    }
}