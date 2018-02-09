using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Multiformats.Base
{
    internal class Base10 : Multibase
    {
        internal static readonly string ValidChars = "0123456789";

        protected override string Name => "base10";
        protected override char Prefix => '9';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        public override byte[] Decode(string input)
        {
            var big = BigInteger.Parse(new string(input.SkipWhile(c => c == '0').ToArray()));
            return LeadingZeros(input).Concat(big.ToByteArray().Reverse()).ToArray();
        }

        private static IEnumerable<byte> LeadingZeros(IEnumerable<char> input)
        {
            return Enumerable.Range(0, input.TakeWhile(b => b == '0').Count()).Select(_ => (byte)0x00);
        }

        private static IEnumerable<char> LeadingNulls(IEnumerable<byte> input)
        {
            return Enumerable.Range(0, input.TakeWhile(b => b == 0x00).Count()).Select(_ => '0');
        }

        public override string Encode(byte[] bytes)
        {
            var big = new BigInteger(bytes.SkipWhile(b => b == 0x00).Reverse().ToArray());
            return new string(LeadingNulls(bytes).ToArray()) + big.ToString();
        }
    }
}
