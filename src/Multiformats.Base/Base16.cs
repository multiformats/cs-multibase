using System;
using System.Linq;
using Multiformats.Base.Extensions;

namespace Multiformats.Base
{
    internal abstract class Base16 : Multibase
    {
        protected ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input, LetterCasing casing)
        {
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

            Span<byte> result = stackalloc byte[data.Length / 2];
            for (var i = 0; i < result.Length; ++i)
            {
                result[i] = (byte)Convert.ToInt32(data.Slice(i * 2, 2).ToString(), 16);
            }
            return result.ToArray();
        }

        protected ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes, LetterCasing casing)
        {
            var format = casing == LetterCasing.Lower ? "{0:x2}" : "{0:X2}";

            Span<char> result = stackalloc char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; ++i)
            {
                string.Format(format, bytes[i]).AsSpan().CopyTo(result.Slice(i * 2));
            }
            return result.ToArray();
        }
    }
}