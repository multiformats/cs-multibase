using System.Linq;

namespace Multiformats.Base
{
    internal class Base16Upper : Base16
    {
        internal static readonly string ValidChars = "0123456789ABCDEF";

        protected override string Name => "BASE16";
        protected override char Prefix => 'F';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, LetterCasing.Upper);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, LetterCasing.Upper);
    }
}