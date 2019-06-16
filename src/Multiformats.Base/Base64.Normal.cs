using System;

namespace Multiformats.Base
{
    internal class Base64Normal : Base64
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();

        protected override string Name => "base64";
        protected override char Prefix => 'm';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan(), false, false).ToArray();
        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input) => Decode(input, false, false);
        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan(), false, false).ToString();
        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes) => Encode(bytes, false, false);
    }
}
