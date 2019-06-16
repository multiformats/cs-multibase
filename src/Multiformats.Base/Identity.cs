using System;

namespace Multiformats.Base
{
    internal class Identity : Multibase
    {
        protected override string Name => "identity";
        protected override char Prefix => '\0';
        protected override char[] Alphabet => Array.Empty<char>();

        protected override bool IsValid(string value) => true;
        public override byte[] Decode(string input) => Decode(input.AsSpan()).ToArray();
        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input)
        {
            Span<byte> result = stackalloc byte[input.Length];
            for (var i = 0; i < input.Length; ++i)
                result[i] = Convert.ToByte(input[i]);
            return result.ToArray();
        }

        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan()).ToString();
        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes)
        {
            Span<char> result = stackalloc char[bytes.Length];
            for (var i = 0; i < bytes.Length; ++i)
                result[i] = Convert.ToChar(bytes[i]);
            return result.ToArray();
        }
    }
}