using System;
using System.Linq;

namespace Multiformats.Base
{
    public static class Multibase
    {
        public static readonly Base2Encoding Base2 = new Base2Encoding();
        public static readonly Base8Encoding Base8 = new Base8Encoding();
        public static readonly Base10Encoding Base10 = new Base10Encoding();
        public static readonly Base16Encoding Base16 = new Base16Encoding();
        public static readonly Base32Encoding Base32 = new Base32Encoding();
        public static readonly Base64Encoding Base64 = new Base64Encoding();
        public static readonly Base58Encoding Base58 = new Base58Encoding();

        private static readonly MultibaseEncoding[] _encodings =
        {
            Base2, Base8, Base10, Base16, Base32, Base58, Base64
        };

        /// <summary>
        /// Encode byte array
        /// </summary>
        /// <returns>Multibase encoded string</returns>
        public static string Encode<TEncoding>(byte[] data) where TEncoding : MultibaseEncoding
        {
            var encoding = _encodings.OfType<TEncoding>().SingleOrDefault();
            if (encoding == null)
                throw new NotSupportedException($"Encoding type is not supported: {typeof(TEncoding).Name}");

            return encoding.Encode(data);
        }

        /// <summary>
        /// Encode byte array
        /// </summary>
        /// <param name="encoding">Multibase encoding</param>
        /// <returns>Multibase encoded string</returns>
        public static string Encode(MultibaseEncoding encoding, byte[] data) => encoding.Encode(data);

        /// <summary>
        /// Encode byte array
        /// </summary>
        /// <returns>String without Multibase prefix</returns>
        public static string EncodeRaw<TEncoding>(byte[] data) where TEncoding : MultibaseEncoding => Encode<TEncoding>(data).Substring(1);

        /// <summary>
        /// Encode byte array
        /// </summary>
        /// <param name="encoding">Multibase encoding</param>
        /// <returns>String without Multibase prefix</returns>
        public static string EncodeRaw(MultibaseEncoding encoding, byte[] data) => encoding.Encode(data).Substring(1);

        /// <summary>
        /// Decode a Multibase encoded string
        /// </summary>
        /// <returns>Decoded bytes</returns>
        public static byte[] Decode(string s)
        {
            MultibaseEncoding encoding;
            return Decode(s, out encoding);
        }

        /// <summary>
        /// Decode a Multibase encoded string
        /// </summary>
        /// <param name="s">Multibase encoded string</param>
        /// <param name="encoding">Encoding used</param>
        /// <returns>Decoded bytes</returns>
        public static byte[] Decode(string s, out MultibaseEncoding encoding)
        {
            encoding = _encodings.SingleOrDefault(e => e.Identifiers.Contains(s[0]));
            if (encoding == null)
                throw new NotSupportedException($"Encoding type is not supported: {s[0]}");

            return encoding.Decode(s);
        }

        /// <summary>
        /// Decode an encoded string without Multibase prefix, but known encoding
        /// </summary>
        /// <param name="encoding">Encoding used in input</param>
        /// <param name="s">Encoded string</param>
        /// <returns>Decoded bytes</returns>
        public static byte[] DecodeRaw(MultibaseEncoding encoding, string s) => encoding.Decode(encoding.DefaultIdentifier + s);

        /// <summary>
        /// Decode an encoded string without Multibase prefix, but known encoding
        /// </summary>
        /// <param name="s">Encoded string</param>
        /// <returns>Decoded bytes</returns>
        public static byte[] DecodeRaw<TEncoding>(string s) where TEncoding : MultibaseEncoding
        {
            var encoding = _encodings.OfType<TEncoding>().SingleOrDefault();
            if (encoding == null)
                throw new NotSupportedException($"Encoding type is not supported: {typeof(TEncoding).Name}");

            return encoding.Decode(encoding.DefaultIdentifier + s);
        }
    }
}