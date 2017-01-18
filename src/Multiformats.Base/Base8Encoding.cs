using System;
using System.Linq;

namespace Multiformats.Base
{
    public class Base8Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { '7' };

        public const char DefaultSeparator = ' ';

        public string Encode(byte[] data, char separator) => Identifiers[0] + string.Join(new string(separator, 1), data.Select(b => Convert.ToString(b, 8)));
        public override string Encode(byte[] data) => Encode(data, DefaultSeparator);

        public byte[] Decode(string str, char separator) => str.Substring(1).Split(separator).Select(c => Convert.ToByte(c, 8)).ToArray();
        public override byte[] Decode(string str) => Decode(str, DefaultSeparator);
    }
}