using System;

namespace Multiformats.Base
{
    internal class Base58Flickr : Base58
    {
        private const string s_alphabetStr = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
        private static readonly char[] s_alphabet = s_alphabetStr.ToCharArray();
        private static readonly byte[] s_codecMap = CreateDecodeMap(s_alphabetStr);
        protected override string AlphabetStr { get; } = s_alphabetStr;
        protected override ReadOnlyMemory<byte> CodecMap { get; } = s_codecMap;
        protected override string Name => "base58flickr";
        protected override char Prefix => 'Z';
        protected override char[] Alphabet { get; } = s_alphabet;
    }
}
