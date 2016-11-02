using System;
using SimpleBase;

namespace Multiformats.Base
{
    public class Base58Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { 'Z', 'z' };
        public override char DefaultIdentifier => Identifiers[1];

        public override string Encode(byte[] data) => Encode(data, Base58Alphabet.Bitcoin);

        public string Encode(byte[] data, Base58Alphabet alphabet) => (alphabet == Base58Alphabet.Bitcoin ? 'z' : 'Z') + (alphabet == Base58Alphabet.Bitcoin ? Base58.Bitcoin.Encode(data) : Base58.Flickr.Encode(data));

        public override byte[] Decode(string str)
        {
            var id = str[0];
            str = str.Substring(1);

            switch (id)
            {
                case 'Z':
                    return Base58.Flickr.Decode(str);
                case 'z':
                    return Base58.Bitcoin.Decode(str);
                default:
                    throw new NotSupportedException($"Unsupported identifier: {id}");
            }
        }
    }

    public enum Base58Alphabet
    {
        Bitcoin,
        Flickr
    }
}