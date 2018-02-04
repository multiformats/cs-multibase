using System.Linq;

namespace Multiformats.Base
{
    internal class Base32HexPaddedLower : Base32
    {
        protected override string Name => "base32hexpad";
        protected override char Prefix => 't';
        protected override bool IsValid(string value) => value.All(c => c == '=' || AlphabetRfc4648HexLower.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, AlphabetRfc4648HexLower, true, LetterCasing.Lower);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, AlphabetRfc4648HexLower, true);
    }
}