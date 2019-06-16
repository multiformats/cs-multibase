using System;

namespace Multiformats.Base
{
    internal class Base32PaddedLower : Base32
    {
        private static readonly char[] _alphabet = "abcdefghijklmnopqrstuvwxyz234567=".ToCharArray();

        protected override string Name => "base32pad";
        protected override char Prefix => 'c';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan(), true, LetterCasing.Lower).ToArray();
        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input) => Decode(input, true, LetterCasing.Lower);
        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan(), true).ToString();
        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes) => Encode(bytes, true);
    }
}