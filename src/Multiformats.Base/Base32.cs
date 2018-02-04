using System;
using System.Linq;

namespace Multiformats.Base
{
    internal abstract class Base32 : Multibase
    {
        internal static readonly string AlphabetRfc4648Lower = "abcdefghijklmnopqrstuvwxyz234567";
        internal static readonly string AlphabetRfc4648Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        internal static readonly string AlphabetRfc4648HexLower = "0123456789abcdefghijklmnopqrstuv";
        internal static readonly string AlphabetRfc4648HexUpper = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        internal static readonly string AlphabetZBase32 = "ybndrfg8ejkmcpqxot1uwisza345h769";

        protected byte[] Decode(string input, string alphabet, bool padding, LetterCasing casing)
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
                value = (value << 5) | alphabet.IndexOf(input[i]);
                bits += 5;

                if (bits >= 8)
                {
                    output[index++] = (byte)(((uint)value >> (bits - 8)) & 255);
                    bits -= 8;
                }
            }

            return output;
        }

        protected string Encode(byte[] bytes, string alphabet, bool padding)
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
                    output += alphabet[(int)((uint)value >> (bits - 5)) & 31];
                    bits -= 5;
                }
            }

            if (bits > 0)
                output += alphabet[(value << (5 - bits)) & 31];

            if (padding)
            {
                while ((output.Length % 8) != 0)
                    output += '=';
            }

            return output;
        }
    }
}
