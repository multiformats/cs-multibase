namespace Multiformats.Base
{
    internal class Base32HexPaddedLower : Base32
    {
        private static readonly char[] _alphabet = "0123456789abcdefghijklmnopqrstuv=".ToCharArray();

        protected override string Name => "base32hexpad";
        protected override char Prefix => 't';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, true, LetterCasing.Lower);

        public override string Encode(byte[] bytes) => Encode(bytes, true);
    }
}