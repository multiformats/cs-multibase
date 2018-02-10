namespace Multiformats.Base
{
    internal class Base64UrlPadded : Base64
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_=".ToCharArray();

        protected override string Name => "base64urlpad";
        protected override char Prefix => 'U';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, true, true);

        public override string Encode(byte[] bytes) => Encode(bytes, true, true);
    }
}