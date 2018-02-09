namespace Multiformats.Base
{
    internal class Base32HexUpper : Base32
    {
        private static readonly char[] _alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUV".ToCharArray();

        protected override string Name => "BASE32HEX";
        protected override char Prefix => 'V';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, false, LetterCasing.Upper);

        public override string Encode(byte[] bytes) => Encode(bytes, false);
    }
}