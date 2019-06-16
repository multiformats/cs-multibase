using System;

namespace Multiformats.Base
{
    internal class Base64Url : Base64
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_".ToCharArray();

        protected override string Name => "base64url";
        protected override char Prefix => 'u';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan(), true, false).ToArray();
        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input) => Decode(input, true, false);
        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan(), true, false).ToString();
        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes) => Encode(bytes, true, false);
    }
}