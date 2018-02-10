namespace Multiformats.Base
{
    internal class Base32Z : Base32
    {
        private static readonly char[] _alphabet = "ybndrfg8ejkmcpqxot1uwisza345h769".ToCharArray();

        protected override string Name => "base32z";
        protected override char Prefix => 'h';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, false, LetterCasing.Ignore);

        public override string Encode(byte[] bytes) => Encode(bytes, false);
    }
}