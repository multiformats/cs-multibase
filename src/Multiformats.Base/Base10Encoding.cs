using System.Linq;

namespace Multiformats.Base
{
    public class Base10Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { '9' };

        public const char DefaultSeparator = ' ';

        public string Encode(byte[] data, char separator) => Identifiers[0] + string.Join(new string(separator, 1), data.Select(b => $"{b:D}"));
        public override string Encode(byte[] data) => Encode(data, DefaultSeparator);

        public byte[] Decode(string str, char separator) => str.Substring(1).Split(separator).Select(byte.Parse).ToArray();
        public override byte[] Decode(string str) => Decode(str, DefaultSeparator);
    }
}