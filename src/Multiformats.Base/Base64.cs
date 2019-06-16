using System;
using Multiformats.Base.Extensions;

namespace Multiformats.Base
{
    internal abstract class Base64 : Multibase
    {
        private static ReadOnlyMemory<char> Pad(ReadOnlySpan<char> input)
        {
            var diff = input.Length % 4;
            if (diff == 0)
                return input.ToArray();

            Span<char> result = stackalloc char[input.Length + (4 - diff)];
            input.CopyTo(result);
            result.Slice(input.Length, 4 - diff).Fill('=');
            return result.ToArray();
        }

        protected ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes, bool urlSafe, bool padded)
        {
            var result = Convert.ToBase64String(bytes.ToArray());
            if (urlSafe)
                result = result.Replace('+', '-').Replace('/', '_');

            if (!padded)
                result = result.TrimEnd('=');

            return result.AsMemory();
        }

        protected ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input, bool urlSafe, bool padded)
        {
            if (urlSafe)
                input = input.Replace('-', '+').Replace('_', '/');

            input = Pad(input.TrimEnd('=')).Span;

            return Convert.FromBase64String(input.ToString());
        }
    }
}