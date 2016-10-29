using System.Linq;

namespace Multiformats.Base
{
    public class Base10Encoding : MultibaseEncoding
    {
        public override char[] Identifiers => new[] { '9' };

        public override string Encode(byte[] data) => Identifiers[0] + string.Join(" ", data.Select(b => $"{b:D}"));
        public override byte[] Decode(string str) => str.Substring(1).Split(' ').Select(byte.Parse).ToArray();
    }
}