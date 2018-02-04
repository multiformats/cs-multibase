using System.Linq;

namespace Multiformats.Base
{
    internal class Base32PaddedLower : Base32
    {
        protected override string Name => "base32pad";
        protected override char Prefix => 'c';
        protected override bool IsValid(string value) => value.All(c => c == '=' || AlphabetRfc4648Lower.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, AlphabetRfc4648Lower, true, LetterCasing.Lower);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, AlphabetRfc4648Lower, true);
    }
}