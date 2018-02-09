using System.Linq;

namespace Multiformats.Base
{
    internal class Base32HexUpper : Base32
    {
        protected override string Name => "BASE32HEX";
        protected override char Prefix => 'V';
        protected override bool IsValid(string value) => value.All(c => AlphabetRfc4648HexUpper.Contains(c));

        public override byte[] Decode(string input) => Decode(input, AlphabetRfc4648HexUpper, false, LetterCasing.Upper);

        public override string Encode(byte[] bytes) => Encode(bytes, AlphabetRfc4648HexUpper, false);
    }
}