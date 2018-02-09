namespace Multiformats.Base
{
    internal class Base32Upper : Base32
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

        protected override string Name => "BASE32";
        protected override char Prefix => 'B';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, false, LetterCasing.Upper);

        public override string Encode(byte[] bytes) => Encode(bytes, false);
    }
}