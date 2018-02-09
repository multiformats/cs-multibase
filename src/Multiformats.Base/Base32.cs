using System;
using System.Linq;

namespace Multiformats.Base
{
    internal abstract class Base32 : Multibase
    {
        protected byte[] Decode(string input, bool padding, LetterCasing casing)
        {
            if (padding)
                input = input.TrimEnd('=');

            if (casing == LetterCasing.Lower && input.Any(char.IsUpper))
                input = input.ToLower();

            if (casing == LetterCasing.Upper && input.Any(char.IsLower))
                input = input.ToUpper();

            var bits = 0;
            var value = 0;
            var index = 0;
            var output = new byte[(input.Length * 5 / 8) | 0];

            for (var i = 0; i < input.Length; i++)
            {
                value = (value << 5) | Array.IndexOf(Alphabet, input[i]);
                bits += 5;

                if (bits >= 8)
                {
                    output[index++] = (byte)(((uint)value >> (bits - 8)) & 255);
                    bits -= 8;
                }
            }

            return output;
        }

        protected string Encode(byte[] bytes, bool padding)
        {
            int bits = 0;
            int value = 0;
            string output = "";

            for (var i = 0; i < bytes.Length; i++)
            {
                value = (value << 8) | bytes[i];
                bits += 8;

                while (bits >= 5)
                {
                    output += Alphabet[(int)((uint)value >> (bits - 5)) & 31];
                    bits -= 5;
                }
            }

            if (bits > 0)
                output += Alphabet[(value << (5 - bits)) & 31];

            if (padding)
            {
                while ((output.Length % 8) != 0)
                    output += '=';
            }

            return output;
        }
    }
}
