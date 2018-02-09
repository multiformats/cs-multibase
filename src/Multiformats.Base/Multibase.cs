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

        public static Multibase Base2 => _bases[MultibaseEncoding.Base2];
        public static Multibase Base8 => _bases[MultibaseEncoding.Base8];
        public static Multibase Base10 => _bases[MultibaseEncoding.Base10];
        public static Multibase Base16 => _bases[MultibaseEncoding.Base16Lower];
        public static Multibase Base32 => _bases[MultibaseEncoding.Base32Lower];
        public static Multibase Base58 => _bases[MultibaseEncoding.Base58Btc];
        public static Multibase Base64 => _bases[MultibaseEncoding.Base64];

        protected abstract string Name { get; }
        protected abstract char Prefix { get; }
        protected abstract char[] Alphabet { get; }
        protected virtual bool IsValid(string value) => value.Distinct().All(c => Array.IndexOf(Alphabet, c) > -1);

        public abstract byte[] Decode(string input);
        public abstract string Encode(byte[] bytes);

        /// <summary>
        /// Encode a byte array to multibase given encoding.
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="bytes">Bytes</param>
        /// <returns>Encoded string</returns>
        public static string Encode(MultibaseEncoding encoding, byte[] bytes)
        {
            if (!_bases.TryGetValue(encoding, out var @base))
                throw new NotSupportedException($"{encoding} is not supported.");

            return Encode(@base, bytes, true);
        }

        /// <summary>
        /// Encode a byte array to multibase given encoding in string format.
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="bytes">Bytes</param>
        /// <returns>Encoded string</returns>
        public static string Encode(string encoding, byte[] bytes)
        {
            var @base = _bases.Values.SingleOrDefault(b => b.Name.Equals(encoding));
            if (@base == null)
                throw new NotSupportedException($"{encoding} is not supported.");

            return Encode(@base, bytes, true);
        }

        private static string Encode(Multibase @base, byte[] bytes, bool prefix)
        {
            if (bytes == null || bytes.Length == 0)
                throw new ArgumentNullException(nameof(bytes));

            return prefix ? @base.Prefix + @base.Encode(bytes) : @base.Encode(bytes);
        }

        /// <summary>
        /// Encode a byte array given encoding (without multibase prefix).
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="bytes">Bytes</param>
        /// <returns>Encoded string</returns>
        public static string EncodeRaw(MultibaseEncoding encoding, byte[] bytes)
        {
            var @base = _bases[encoding];
            if (@base == null)
                throw new NotSupportedException($"{encoding} is not supported.");

            return Encode(@base, bytes, false);
        }

        /// <summary>
        /// Decode an multibase encoded string.
        /// </summary>
        /// <param name="input">Encoded string</param>
        /// <param name="encoding">Encoding used</param>
        /// <param name="strict">Don't allow non-valid characters for given encoding</param>
        /// <returns>Bytes</returns>
        public static byte[] Decode(string input, out MultibaseEncoding encoding, bool strict = true)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            var @base = _bases.Values.SingleOrDefault(b => b.Prefix == input[0]);
            if (@base == null)
                throw new NotSupportedException($"{input[0]} is an unknown encoding prefix.");

            var value = input.Substring(1);
            encoding = _bases.SingleOrDefault(kv => kv.Value.Equals(@base)).Key;

            if (strict && !@base.IsValid(value))
                throw new InvalidOperationException($"{value} contains invalid chars for {encoding}.");

            return @base.Decode(value);
        }

        /// <summary>
        /// Decode an multibase encoded string.
        /// </summary>
        /// <param name="input">Encoded string</param>
        /// <param name="encoding">Encoding used</param>
        /// <param name="strict">Don't allow non-valid characters for given encoding</param>
        /// <returns>Bytes</returns>
        public static byte[] Decode(string input, out string encoding, bool strict = true)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            var @base = _bases.Values.SingleOrDefault(b => b.Prefix == input[0]);
            if (@base == null)
                throw new NotSupportedException($"{input[0]} is an unknown encoding prefix.");

            var value = input.Substring(1);
            encoding = @base.Name;

            if (strict && !@base.IsValid(value))
                throw new InvalidOperationException($"{value} contains invalid chars for {encoding}.");

            return @base.Decode(value);
        }

        /// <summary>
        /// Decode an encoded string using given encoded (without multibase prefix).
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="input">Encoded string</param>
        /// <returns>Bytes</returns>
        public static byte[] DecodeRaw(MultibaseEncoding encoding, string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            if (!_bases.TryGetValue(encoding, out var @base))
                throw new NotSupportedException($"{encoding} is an unknown encoding.");

            return @base.Decode(input);
        }

        /// <summary>
        /// Try to decode an encoded string. If prefixed with a multibase prefix it's guaranteed to give the correct value, if not there's no guarantee it will pick the right encoding.
        /// </summary>
        /// <param name="input">Encoded string</param>
        /// <param name="encoding">Guessed encoding</param>
        /// <param name="bytes">Decoded bytes</param>
        /// <returns>True on success (no guarantee it's correct), false on error</returns>
        public static bool TryDecode(string input, out MultibaseEncoding encoding, out byte[] bytes)
        {
            try
            {
                // special case for base2 without prefix
                if (input[0] == '0' && input.Length % 8 == 0)
                    throw new Exception();

                bytes = Decode(input, out encoding);
                return true;
            }
            catch
            {
                foreach (var @base in _bases.Values.Skip(1).Where(b => b.IsValid(input)))
                {
                    try
                    {
                        bytes = @base.Decode(input);
                        encoding = _bases.SingleOrDefault(kv => kv.Value.Equals(@base)).Key;
                        return true;
                    }
                    catch
                    {
                    }
                }
            }

            encoding = default(MultibaseEncoding);
            bytes = null;
            return false;
        }
    }
}
