using System.Linq;

namespace Multiformats.Base
{
    internal class Base64UrlPadded : Base64
    {
        internal static readonly string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_=";

        protected override string Name => "base64urlpad";
        protected override char Prefix => 'U';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, true, true);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, true, true);
    }
}