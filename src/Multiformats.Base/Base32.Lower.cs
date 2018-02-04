using System.Linq;

namespace Multiformats.Base
{
    internal class Base32Lower : Base32
    {
        protected override string Name => "base32";
        protected override char Prefix => 'b';
        protected override bool IsValid(string value) => value.All(c => AlphabetRfc4648Lower.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, AlphabetRfc4648Lower, false, LetterCasing.Lower);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, AlphabetRfc4648Lower, false);
    }
}