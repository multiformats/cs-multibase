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

        public static string Encode(MultibaseEncoding encoding, byte[] data) => encoding.Encode(data);

        public static string EncodeRaw(MultibaseEncoding encoding, byte[] data) => encoding.Encode(data).Substring(1);

        public static byte[] Decode(string s)
        {
            var encoding = _encodings.SingleOrDefault(e => e.Identifiers.Contains(s[0]));
            if (encoding == null)
                throw new NotSupportedException($"Encoding type is not supported: {s[0]}");

            return encoding.Decode(s);
        }

        public static byte[] DecodeRaw(MultibaseEncoding encoding, string s) => encoding.Decode(encoding.DefaultIdentifier + s);
    }
}