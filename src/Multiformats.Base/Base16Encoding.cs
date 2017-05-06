using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multiformats.Base
{
    public class Base16Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { 'F', 'f' };

        public override string Encode(byte[] data) => Encode(data, true);

        public string Encode(byte[] data, bool uppercase)
        {
            return (uppercase ? 'F' : 'f') +
                   data.Aggregate(new StringBuilder(), (sb, b) => sb.Append(uppercase ? $"{b:X2}" : $"{b:x2}")).ToString();
        }

        public override byte[] Decode(string str)
        {
            var uppercase = str[0] == 'F';
            str = str.Substring(1);

            if (uppercase && str.Any(char.IsLower))
                throw new Exception("Mismatch, wanted uppercase, got lowercase chars");

            if (!uppercase && str.Any(char.IsUpper))
                throw new Exception("Mismatch, wanted lowercase, got uppercase chars");

            return Enumerable.Range(0, str.Length / 2)
                    .Select(i => (byte)Convert.ToInt32(str.Substring(i * 2, 2), 16))
                    .ToArray();
        }
    }
}