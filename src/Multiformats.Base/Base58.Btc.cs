using System;

namespace Multiformats.Base
{
    internal class Base58Btc : Base58
    {
        private const string s_alphabetStr = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        private static readonly char[] s_alphabet = s_alphabetStr.ToCharArray();
        private static readonly byte[] s_codecMap = CreateDecodeMap(s_alphabetStr);
        protected override string AlphabetStr { get; } = s_alphabetStr;
        protected override ReadOnlyMemory<byte> CodecMap { get; } = s_codecMap;
        protected override string Name => "base58btc";
        protected override char Prefix => 'z';
        protected override char[] Alphabet { get; } = s_alphabet;
    }

    internal class Base58BtcV2 : Base58Btc
    {
        protected override string Name => "base58btcv2";

        public override string Encode(byte[] bytes) => base.EncodeWithSpanInner(bytes);

        public override byte[] Decode(string input) => base.DecodeWithSpanInner(input);
    }
}
