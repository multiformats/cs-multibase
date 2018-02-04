using System;
using System.Linq;
using System.Text;

namespace Multiformats.Base
{
    internal abstract class Base16 : Multibase
    {
        protected byte[] Decode(string input, LetterCasing casing)
        {
            if (casing == LetterCasing.Lower && input.Any(char.IsUpper))
                input = input.ToLower();

            if (casing == LetterCasing.Upper && input.Any(char.IsLower))
                input = input.ToUpper();

            return Enumerable.Range(0, input.Length / 2)
                .Select(i => (byte)Convert.ToInt32(input.Substring(i * 2, 2), 16))
                .ToArray();
        }

        protected string Encode(byte[] input, LetterCasing casing)
        {
            var format = casing == LetterCasing.Lower ? "{0:x2}" : "{0:X2}";

            return input.Aggregate(new StringBuilder(), (sb, b) => sb.AppendFormat(format, b)).ToString();
        }
    }
}