using System.Linq;

namespace Multiformats.Base
{
    internal class Base32HexPaddedUpper : Base32
    {
        protected override string Name => "BASE32HEXPAD";
        protected override char Prefix => 'T';
        protected override bool IsValid(string value) => value.All(c => c == '=' || AlphabetRfc4648HexUpper.Contains(c));

        public override byte[] Decode(string input) => Decode(input, AlphabetRfc4648HexUpper, true, LetterCasing.Upper);

        public override string Encode(byte[] bytes) => Encode(bytes, AlphabetRfc4648HexUpper, true);
    }
}