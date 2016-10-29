using System;
using System.Linq;
using SimpleBase;

namespace Multiformats.Base
{
    public class Base16Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { 'F', 'f' };

        public override string Encode(byte[] data) => Encode(data, true);

        public string Encode(byte[] data, bool uppercase) => (uppercase ? 'F' : 'f') + (uppercase ? Base16.EncodeUpper(data) : Base16.EncodeLower(data));

        public override byte[] Decode(string str)
        {
            var uppercase = str[0] == 'F';
            str = str.Substring(1);

            if (uppercase && str.Any(char.IsLower))
                throw new Exception("Mismatch, wanted uppercase, got lowercase chars");

            if (!uppercase && str.Any(char.IsUpper))
                throw new Exception("Mismatch, wanted lowercase, got uppercase chars");

            return Base16.Decode(str);
        }
    }
}