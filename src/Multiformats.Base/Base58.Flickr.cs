using System.Linq;

namespace Multiformats.Base
{
    internal class Base58Flickr : Base58
    {
        internal static readonly string ValidChars = "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";

        protected override string Name => "base58flickr";
        protected override char Prefix => 'Z';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input) => Decode(input, ValidChars);

        internal override string EncodeCore(byte[] bytes) => Encode(bytes, ValidChars);
    }
}