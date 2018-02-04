using System.Linq;

namespace Multiformats.Base
{
    internal class Base64Normal : Base64
    {
        internal static readonly string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        protected override string Name => "base64";
        protected override char Prefix => 'm';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, false, false);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, false, false);
    }
}
