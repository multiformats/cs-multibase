namespace Multiformats.Base
{
    internal class Base32HexPaddedUpper : Base32
    {
        private static readonly char[] _alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV=".ToCharArray();

        protected override string Name => "BASE32HEXPAD";
        protected override char Prefix => 'T';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, true, LetterCasing.Upper);

        public override string Encode(byte[] bytes) => Encode(bytes, true);
    }
}