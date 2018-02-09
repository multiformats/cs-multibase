using System.Linq;

namespace Multiformats.Base
{
    internal class Base32HexLower : Base32
    {
        protected override string Name => "base32hex";
        protected override char Prefix => 'v';
        protected override bool IsValid(string value) => value.All(c => AlphabetRfc4648HexLower.Contains(c));

        public override byte[] Decode(string input) => Decode(input, AlphabetRfc4648HexLower, false, LetterCasing.Lower);

        public override string Encode(byte[] bytes) => Encode(bytes, AlphabetRfc4648HexLower, false);
    }
}