using System.Linq;

namespace Multiformats.Base
{
    internal class Base64UrlPadded : Base64
    {
        internal static readonly string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_=";

        protected override string Name => "base64urlpad";
        protected override char Prefix => 'U';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        public override byte[] Decode(string input) => Decode(input, true, true);

        public override string Encode(byte[] bytes) => Encode(bytes, true, true);
    }
}