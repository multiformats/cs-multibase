namespace Multiformats.Base
{
    internal class Base58Btc : Base58
    {
        private static readonly char[] _alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray();

        protected override string Name => "base58btc";
        protected override char Prefix => 'z';
        protected override char[] Alphabet => _alphabet;
    }

    internal class Base58BtcV2 : Base58Btc
    {
        protected override string Name => "base58btcv2";

        public override byte[] Decode(string input) => base.DecodeWithSpan(input, Alphabet);
    }
}
