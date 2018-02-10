using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Multiformats.Base
{
    internal class Base10 : Multibase
    {
        private static readonly char[] _alphabet = "0123456789".ToCharArray();

        protected override string Name => "base10";
        protected override char Prefix => '9';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input)
        {
            var big = BigInteger.Parse("00" + input, NumberStyles.None);
            return LeadingZeros(input).Concat(big.ToByteArray().Reverse().SkipWhile(b => b == 0)).ToArray();
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
            var big = new BigInteger(bytes.Reverse().Concat(new byte[]{0x00}).ToArray());
            return new string(LeadingNulls(bytes).ToArray()) + big.ToString();
        }
    }
}
