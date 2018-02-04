using System.Linq;

namespace Multiformats.Base
{
    internal class Base32HexUpper : Base32
    {
        protected override string Name => "BASE32HEX";
        protected override char Prefix => 'V';
        protected override bool IsValid(string value) => value.All(c => AlphabetRfc4648HexUpper.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, AlphabetRfc4648HexUpper, false, LetterCasing.Upper);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, AlphabetRfc4648HexUpper, false);
    }
}