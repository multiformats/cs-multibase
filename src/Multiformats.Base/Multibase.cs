using System;
using System.Collections.Generic;
using System.Linq;

namespace Multiformats.Base
{
    public abstract class Multibase
    {
        protected static readonly Dictionary<MultibaseEncoding, Multibase> _bases;

        static Multibase()
        {
            _bases = new Dictionary<MultibaseEncoding, Multibase>
            {
                {MultibaseEncoding.Identity, new Identity()},
                {MultibaseEncoding.Base2, new Base2()},
                {MultibaseEncoding.Base8, new Base8()},
                {MultibaseEncoding.Base10, new Base10()},
                {MultibaseEncoding.Base16Lower, new Base16Lower()},
                {MultibaseEncoding.Base16Upper, new Base16Upper()},
                {MultibaseEncoding.Base32Lower, new Base32Lower()},
                {MultibaseEncoding.Base32Upper, new Base32Upper()},
                {MultibaseEncoding.Base32PaddedLower, new Base32PaddedLower()},
                {MultibaseEncoding.Base32PaddedUpper, new Base32PaddedUpper()},
                {MultibaseEncoding.Base32HexLower, new Base32HexLower()},
                {MultibaseEncoding.Base32HexUpper, new Base32HexUpper()},
                {MultibaseEncoding.Base32HexPaddedLower, new Base32HexPaddedLower()},
                {MultibaseEncoding.Base32HexPaddedUpper, new Base32HexPaddedUpper()},
                {MultibaseEncoding.Base32Z, new Base32Z()},
                {MultibaseEncoding.Base58Btc, new Base58Btc()},
                {MultibaseEncoding.Base58Flickr, new Base58Flickr()},
                {MultibaseEncoding.Base64, new Base64Normal()},
                {MultibaseEncoding.Base64Padded, new Base64Padded()},
                {MultibaseEncoding.Base64Url, new Base64Url()},
                {MultibaseEncoding.Base64UrlPadded, new Base64UrlPadded()},
            };
        }

        protected abstract string Name { get; }
        protected abstract char Prefix { get; }
        protected abstract bool IsValid(string value);

        internal abstract byte[] DecodeCore(string input);
        internal abstract string EncodeCore(byte[] bytes);

        public static string Encode(MultibaseEncoding encoding, byte[] bytes)
        {
            if (!_bases.TryGetValue(encoding, out var @base))
                throw new NotSupportedException($"{encoding} is not supported.");

            return Encode(@base, bytes);
        }

        public static string Encode(string encoding, byte[] bytes)
        {
            var @base = _bases.Values.SingleOrDefault(b => b.Name.Equals(encoding));
            if (@base == null)
                throw new NotSupportedException($"{encoding} is not supported.");

            return Encode(@base, bytes);
        }

        private static string Encode(Multibase @base, byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                throw new ArgumentNullException(nameof(bytes));

            return @base.Prefix + @base.EncodeCore(bytes);
        }

        public static byte[] Decode(string input, out MultibaseEncoding encoding)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            var @base = _bases.Values.SingleOrDefault(b => b.Prefix == input[0]);
            if (@base == null)
                throw new NotSupportedException($"{input[0]} is an unknown encoding prefix.");

            var value = input.Substring(1);
            encoding = _bases.SingleOrDefault(kv => kv.Value.Equals(@base)).Key;

            if (!@base.IsValid(value))
                throw new InvalidOperationException($"{value} contains invalid chars for {encoding}.");

            return @base.DecodeCore(value);
        }

        public static byte[] Decode(string input, out string encoding)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            var @base = _bases.Values.SingleOrDefault(b => b.Prefix == input[0]);
            if (@base == null)
                throw new NotSupportedException($"{input[0]} is an unknown encoding prefix.");

            encoding = @base.Name;

            return @base.DecodeCore(input.Substring(1));
        }

        public static byte[] Decode(string input) => Decode(input, out string _);
    }
}
