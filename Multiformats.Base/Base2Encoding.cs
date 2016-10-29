using System;
using System.Linq;

namespace Multiformats.Base
{
    public class Base2Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { '0' };

        public override string Encode(byte[] data) => Identifiers[0] + string.Join(" ", data.Select(b => ToBase2((int)b)));
        public override byte[] Decode(string str) => str.Substring(1).Split(' ').Select(c => (byte)Convert.ToByte(c, 2)).ToArray();

        private static string ToBase2(int b)
        {
            var chars = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' };
            var i = chars.Length;
            while (b >= 2)
            {
                i--;
                chars[i] = (b & 1) == 1 ? '1' : '0';
                b >>= 1;
            }
            i--;
            chars[i] = b == 1 ? '1' : '0';
            return new string(chars.Skip(i).ToArray());
        }
    }
}