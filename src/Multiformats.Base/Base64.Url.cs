using System.Linq;

namespace Multiformats.Base
{
    internal class Base64Url : Base64
    {
        internal static readonly string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

        protected override string Name => "base64url";
        protected override char Prefix => 'u';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, true, false);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, true, false);
    }
}