namespace Multiformats.Base
{
    internal class Base32PaddedUpper : Base32
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567=".ToCharArray();

        protected override string Name => "BASE32PAD";
        protected override char Prefix => 'C';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, true, LetterCasing.Upper);

        public override string Encode(byte[] bytes) => Encode(bytes, true);
    }
}