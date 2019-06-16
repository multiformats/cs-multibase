using System;
using Multiformats.Base.Extensions;

namespace Multiformats.Base
{
    internal abstract class Base64 : Multibase
    {
        private static ReadOnlySpan<char> Pad(ReadOnlySpan<char> input)
        {
            var diff = input.Length % 4;
            if (diff == 0)
                return input;

            Span<char> result = new char[input.Length + (4 - diff)];
            input.CopyTo(result);
            result.Slice(input.Length, 4 - diff).Fill('=');
            return result;
        }

        protected ReadOnlySpan<char> Encode(ReadOnlySpan<byte> bytes, bool urlSafe, bool padded)
        {
            var result = Convert.ToBase64String(bytes.ToArray());
            if (urlSafe)
                result = result.Replace('+', '-').Replace('/', '_');

            if (!padded)
                result = result.TrimEnd('=');

            return result.AsSpan();
        }

        protected ReadOnlySpan<byte> Decode(ReadOnlySpan<char> input, bool urlSafe, bool padded)
        {
            if (urlSafe)
                input = input.Replace('-', '+').Replace('_', '/');

            input = Pad(input.TrimEnd('='));

            return Convert.FromBase64String(input.ToString());
        }
    }
}