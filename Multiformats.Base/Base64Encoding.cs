using System;
using WallF.BaseNEncodings;

namespace Multiformats.Base
{
    public class Base64Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { 'M', 'm', 'U', 'u' };
        public override char DefaultIdentifier => Identifiers[1];

        public override string Encode(byte[] data) => Encode(data, false);

        public string Encode(byte[] data, bool padding, bool url = false)
        {
            var id = url ? padding ? 'U' : 'u' : padding ? 'M' : 'm';
            var encoded = url ? BaseEncoding.Base64Safe.ToBaseString(data) : BaseEncoding.Base64.ToBaseString(data);

            if (!padding)
                encoded = UnpadBase64(encoded);

            return id + encoded;
        }

        public override byte[] Decode(string str)
        {
            bool padding, url;
            return Decode(str, out padding, out url);
        }

        public byte[] Decode(string str, out bool padding, out bool url)
        {
            var id = str[0];
            str = str.Substring(1);

            switch (id)
            {
                case 'U':
                    padding = true;
                    url = true;
                    return BaseEncoding.Base64Safe.FromBaseString(str);
                case 'u':
                    padding = false;
                    url = true;
                    return BaseEncoding.Base64Safe.FromBaseString(PadBase64(str));
                case 'M':
                    padding = true;
                    url = false;
                    return BaseEncoding.Base64.FromBaseString(str);
                case 'm':
                    padding = false;
                    url = false;
                    return BaseEncoding.Base64.FromBaseString(PadBase64(str));
                default:
                    throw new NotSupportedException($"Unsupported identifier: {id}");
            }
        }

        private static string UnpadBase64(string s) => s.TrimEnd(BaseEncoding.Base64.PaddingCharacter);
        private static string PadBase64(string s) => s.PadRight(s.Length + (s.Length % 4 == 0 ? 0 : (4 - (s.Length % 4))), BaseEncoding.Base64.PaddingCharacter);
    }
}