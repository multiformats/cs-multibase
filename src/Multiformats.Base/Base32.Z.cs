using System.Linq;

namespace Multiformats.Base
{
    internal class Base32Z : Base32
    {
        protected override string Name => "base32z";
        protected override char Prefix => 'h';
        protected override bool IsValid(string value) => value.All(c => AlphabetZBase32.Contains(c));

        public override byte[] Decode(string input) => Decode(input, AlphabetZBase32, false, LetterCasing.Ignore);

        public override string Encode(byte[] bytes) => Encode(bytes, AlphabetZBase32, false);
    }
}