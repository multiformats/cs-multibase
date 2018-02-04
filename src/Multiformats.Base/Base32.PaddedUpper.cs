using System.Linq;

namespace Multiformats.Base
{
    internal class Base32PaddedUpper : Base32
    {
        protected override string Name => "BASE32PAD";
        protected override char Prefix => 'C';
        protected override bool IsValid(string value) => value.All(c => c == '=' || AlphabetRfc4648Upper.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, AlphabetRfc4648Upper, true, LetterCasing.Upper);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, AlphabetRfc4648Upper, true);
    }
}