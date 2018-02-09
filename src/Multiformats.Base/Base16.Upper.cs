using System.Linq;

namespace Multiformats.Base
{
    internal class Base16Upper : Base16
    {
        internal static readonly string ValidChars = "0123456789ABCDEF";

        protected override string Name => "BASE16";
        protected override char Prefix => 'F';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        public override byte[] Decode(string input) => Decode(input, LetterCasing.Upper);

        public override string Encode(byte[] bytes) => Encode(bytes, LetterCasing.Upper);
    }
}