namespace Multiformats.Base
{
    internal class Base58Flickr : Base58
    {
        private static readonly char[] _alphabet = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ".ToCharArray();

        protected override string Name => "base58flickr";
        protected override char Prefix => 'Z';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, _alphabet);

        public override string Encode(byte[] bytes) => Encode(bytes, _alphabet);
    }
}