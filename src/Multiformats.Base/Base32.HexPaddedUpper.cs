using System.Linq;

namespace Multiformats.Base
{
    internal class Base32HexPaddedUpper : Base32
    {
        protected override string Name => "BASE32HEXPAD";
        protected override char Prefix => 'T';
        protected override bool IsValid(string value) => value.All(c => c == '=' || AlphabetRfc4648HexUpper.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, AlphabetRfc4648HexUpper, true, LetterCasing.Upper);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, AlphabetRfc4648HexUpper, true);
    }
}