using System;

namespace Multiformats.Base
{
    internal abstract class Base64 : Multibase
    {
        private static string Pad(string input)
        {
            var diff = input.Length % 4;

            return diff > 0 ? input + new string('=', 4 - diff) : input;
        }

        protected string Encode(byte[] input, bool urlSafe, bool padded)
        {
            var result = Convert.ToBase64String(input);
            if (urlSafe)
                result = result.Replace('+', '-').Replace('/', '_');

            if (!padded)
                result = result.TrimEnd('=');

            return result;
        }

        protected byte[] Decode(string input, bool urlSafe, bool padded)
        {
            if (urlSafe)
                input = input.Replace('-', '+').Replace('_', '/');

            input = Pad(input.TrimEnd('='));

            return Convert.FromBase64String(input);
        }
    }
}