using System;

namespace Multiformats.Base
{
    internal class Base16Upper : Base16
    {
        private static readonly char[] _alphabet = "0123456789ABCDEF".ToCharArray();

        protected override string Name => "BASE16";
        protected override char Prefix => 'F';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan(), LetterCasing.Upper).ToArray();
        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input) => Decode(input, LetterCasing.Upper);
        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan(), LetterCasing.Upper).ToString();
        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes) => Encode(bytes, LetterCasing.Upper);
    }
}