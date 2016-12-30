using System;
using System.Collections.Generic;
using System.Linq;

namespace Multiformats.Base
{
    public class Base2Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { '0' };

        public const char DefaultSeparator = ' ';

        public string Encode(byte[] data, char separator) => Identifiers[0] + string.Join(new string(separator, 1), data.Select(b => Convert.ToString(b, 2)));
        public override string Encode(byte[] data) => Encode(data, DefaultSeparator);

        public byte[] Decode(string str, char separator) => str.Substring(1).Split(separator).Select(c => Convert.ToByte(c, 2)).ToArray();
        public override byte[] Decode(string str) => Decode(str, DefaultSeparator);
    }
}