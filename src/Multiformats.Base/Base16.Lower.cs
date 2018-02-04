using System.Linq;

namespace Multiformats.Base
{
    internal class Base16Lower : Base16
    {
        internal static readonly string ValidChars = "0123456789abcdef";

        protected override string Name => "base16";
        protected override char Prefix => 'f';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, LetterCasing.Lower);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, LetterCasing.Lower);
    }
}