using System.Linq;

namespace Multiformats.Base
{
    internal class Base64Padded : Base64
    {
        internal static readonly string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

        protected override string Name => "base64pad";
        protected override char Prefix => 'M';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, false, true);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, false, true);
    }
}