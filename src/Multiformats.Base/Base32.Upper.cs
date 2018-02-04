using System.Linq;

namespace Multiformats.Base
{
    internal class Base32Upper : Base32
    {
        protected override string Name => "BASE32";
        protected override char Prefix => 'B';
        protected override bool IsValid(string value) => value.All(c => AlphabetRfc4648Upper.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, AlphabetRfc4648Upper, false, LetterCasing.Upper);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, AlphabetRfc4648Upper, false);
    }
}