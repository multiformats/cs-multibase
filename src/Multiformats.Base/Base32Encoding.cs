using System;
using System.Linq;
using SimpleBase;

namespace Multiformats.Base
{
    public class Base32Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { 'V', 'v', 'T', 't', 'B', 'b', 'C', 'c', 'h' };
        public override char DefaultIdentifier => Identifiers[4];

        private readonly Lazy<Base32> _zbase32 = new Lazy<Base32>(() => new Base32(new Base32Alphabet("ybndrfg8ejkmcpqxot1uwisza345h769")));

        public override string Encode(byte[] data) => Encode(data, false);

        /// <summary>
        /// Encode bytes to Base32
        /// </summary>
        /// <param name="data">Input data</param>
        /// <param name="padding">Use padding</param>
        /// <param name="uppercase">Uppercase (default true)</param>
        /// <param name="hex">Extended Base32 hex</param>
        /// <param name="zbase">Z-Base32</param>
        /// <returns>Encoded string</returns>
        public string Encode(byte[] data, bool padding, bool uppercase = true, bool hex = false, bool zbase = false)
        {
            if (zbase)
                return EncodeZBase32(data);

            if (hex)
                return EncodeHex(data, padding, uppercase);

            return EncodeBase32(data, padding, uppercase);
        }

        private string EncodeZBase32(byte[] data)
        {
            return 'h' + _zbase32.Value.Encode(data, false);
        }

        private string EncodeBase32(byte[] data, bool padding, bool uppercase)
        {
            var id = padding ? 'c' : 'b';
            if (uppercase)
                id = char.ToUpper(id);

            var encoded = Base32.Rfc4648.Encode(data, padding);
            return id + (uppercase ? encoded.ToUpper() : encoded.ToLower());
        }

        private string EncodeHex(byte[] data, bool padding, bool uppercase)
        {
            var id = padding ? 't' : 'v';
            if (uppercase)
                id = char.ToUpper(id);

            var encoded = Base32.ExtendedHex.Encode(data, padding);
            return id + (uppercase ? encoded.ToUpper() : encoded.ToLower());
        }

        public override byte[] Decode(string str)
        {
            //TODO: verification
            var id = str[0];
            str = str.Substring(1);

            if (char.IsUpper(id) && str.Any(char.IsLower))
                throw new Exception("Invalid base32, wanted uppercase, got lowercase");

            if (char.IsLower(id) && str.Any(char.IsUpper))
                throw new Exception("Invalid base32, wanted lowercase, got uppercase");

            switch (id)
            {
                case 'V':
                case 'v':
                    return Base32.ExtendedHex.Decode(str);
                case 'T':
                case 't':
                    return Base32.ExtendedHex.Decode(str);
                case 'B':
                case 'b':
                    return Base32.Rfc4648.Decode(str);
                case 'C':
                case 'c':
                    return Base32.Rfc4648.Decode(str);
                case 'h':
                    return _zbase32.Value.Decode(str);
                default:
                    throw new NotSupportedException($"Identifier {id} is not supported.");
            }
        }
    }
}
