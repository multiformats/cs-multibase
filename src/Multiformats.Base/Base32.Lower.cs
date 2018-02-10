namespace Multiformats.Base
{
    internal class Base32Lower : Base32
    {
        private static readonly char[] _alphabet = "abcdefghijklmnopqrstuvwxyz234567".ToCharArray();

        protected override string Name => "base32";
        protected override char Prefix => 'b';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, false, LetterCasing.Lower);

        public override string Encode(byte[] bytes) => Encode(bytes, false);
    }
}