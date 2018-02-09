using System.Linq;

namespace Multiformats.Base
{
    internal class Base64Normal : Base64
    {
        internal static readonly string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        protected override string Name => "base64";
        protected override char Prefix => 'm';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        public override byte[] Decode(string input) => Decode(input, false, false);

        public override string Encode(byte[] bytes) => Encode(bytes, false, false);
    }
}
