using System;

namespace Multiformats.Base
{
    internal class Base2 : Multibase
    {
        private static readonly char[] _alphabet = { '0', '1' };

        protected override string Name => "base2";
        protected override char Prefix => '0';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan()).ToArray();
        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input)
        {
            Span<byte> result = stackalloc byte[input.Length / 8];
            for (var index = 0; index < input.Length / 8; ++index)
            {
                for (var i = 0; i < 8; ++i)
                    if (input[(index * 8) + i] == '1')
                        result[index] |= (byte)(1 << (7 - i));
            }

            return result.ToArray();
        }

        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan()).ToString();
        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes)
        {
            Span<char> result = stackalloc char[bytes.Length * 8];
            for (var index = 0; index < bytes.Length; ++index)
            {
                for (var i = 0; i < 8; ++i)
                {
                    result[(index * 8) + (7 - i)] = (bytes[index] & (1 << i)) != 0 ? '1' : '0';
                }
            }
            return result.ToArray();
        }
    }
}
