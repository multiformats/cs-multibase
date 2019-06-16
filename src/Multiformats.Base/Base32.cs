using System;
using System.Linq;
using Multiformats.Base.Extensions;

namespace Multiformats.Base
{
    internal abstract class Base32 : Multibase
    {
        protected ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input, bool padding, LetterCasing casing)
        {
            if (padding)
                input = input.TrimEnd('=');

            Span<char> data = stackalloc char[input.Length];

            switch (casing)
            {
                case LetterCasing.Lower when input.Any(char.IsUpper):
                    if (input.ToLowerInvariant(data) != input.Length)
                        throw new Exception("ToLowerInvariant could not be performed.");
                    break;
                case LetterCasing.Upper when input.Any(char.IsLower):
                    if (input.ToUpperInvariant(data) != input.Length)
                        throw new Exception("ToUpperInvariant could not be performed.");
                    break;
                default:
                    input.CopyTo(data);
                    break;
            }

            var bits = 0;
            var value = 0;
            var index = 0;
            Span<byte> output = stackalloc byte[(data.Length * 5 / 8) | 0];

            for (var i = 0; i < data.Length; ++i)
            {
                value = (value << 5) | Array.IndexOf(Alphabet, data[i]);
                bits += 5;

                if (bits >= 8)
                {
                    output[index++] = (byte)(((uint)value >> (bits - 8)) & 255);
                    bits -= 8;
                }
            }

            return output.ToArray();
        }

        protected ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes, bool padding)
        {
            int bits = 0;
            int value = 0;
            int offset = 0;
            Span<char> result = stackalloc char[bytes.Length * 3];

            for (var i = 0; i < bytes.Length; ++i)
            {
                value = (value << 8) | bytes[i];
                bits += 8;

                while (bits >= 5)
                {
                    result[offset++] = Alphabet[(int)((uint)value >> (bits - 5)) & 31];
                    bits -= 5;
                }
            }

            if (bits > 0)
                result[offset++] = Alphabet[(value << (5 - bits)) & 31];

            while (padding && (offset % 8) != 0)
            {
                result[offset++] = '=';
            }

            return result.Slice(0, offset).ToArray();
        }
    }
}
