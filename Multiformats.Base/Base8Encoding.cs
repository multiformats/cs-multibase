using System;
using System.Linq;

namespace Multiformats.Base
{
    public class Base8Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { '7' };

        public override string Encode(byte[] data) => Identifiers[0] + string.Join(" ", data.Select(b => ToBase8((int)b)));
        public override byte[] Decode(string str) => str.Substring(1).Split(' ').Select(c => (byte)Convert.ToByte(c, 8)).ToArray();

        private const string digits = "0123456789abcdefx";

        private static string ToBase8(int b)
        {
            var chars = new char[16];
            var i = chars.Length;
            while (b >= 8)
            {
                i--;
                chars[i] = digits[b & 7];
                b >>= 3;
            }
            i--;
            chars[i] = digits[b];
            return new string(chars.Skip(i).ToArray());
        }
    }
}