using System;

namespace Multiformats.Base
{
    internal class Base16Lower : Base16
    {
        private static readonly char[] _alphabet = "0123456789abcdef".ToCharArray();

        protected override string Name => "base16";
        protected override char Prefix => 'f';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan(), LetterCasing.Lower).ToArray();
        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input) => Decode(input, LetterCasing.Lower);
        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan(), LetterCasing.Lower).ToString();
        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes) => Encode(bytes, LetterCasing.Lower);
    }
}