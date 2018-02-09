namespace Multiformats.Base
{
    internal class Base64Padded : Base64
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".ToCharArray();

        protected override string Name => "base64pad";
        protected override char Prefix => 'M';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, false, true);

        public override string Encode(byte[] bytes) => Encode(bytes, false, true);
    }
}