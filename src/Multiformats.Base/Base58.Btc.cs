namespace Multiformats.Base
{
    internal class Base58Btc : Base58
    {
        private static readonly char[] _alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray();

        protected override string Name => "base58btc";
        protected override char Prefix => 'z';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input, _alphabet);

        public override string Encode(byte[] bytes) => Encode(bytes, _alphabet);
    }
}