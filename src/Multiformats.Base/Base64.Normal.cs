namespace Multiformats.Base
{
    internal class Base64Normal : Base64
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();

        protected override string Name => "base64";
        protected override char Prefix => 'm';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, false, false);

        public override string Encode(byte[] bytes) => Encode(bytes, false, false);
    }
}
