using System;
using System.Globalization;
using System.Numerics;

namespace Multiformats.Base
{
    internal class Base10 : Multibase
    {
        private static readonly char[] _alphabet = "0123456789".ToCharArray();

        protected override string Name => "base10";
        protected override char Prefix => '9';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan()).ToArray();

        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input)
        {
            Span<char> zeroLeaded = stackalloc char[input.Length + 2];
            zeroLeaded[0] = '0';
            zeroLeaded[1] = '0';
            input.CopyTo(zeroLeaded.Slice(2));
            var big = BigInteger.Parse(zeroLeaded.ToString(), NumberStyles.None);

            var leadingZeros = LeadingZeros(input);
            var bigBytes = big.ToByteArray().AsSpan();
            bigBytes.Reverse();
            var bigLeadingZeros = LeadingZeros(bigBytes);

            Span<byte> result = stackalloc byte[leadingZeros + (bigBytes.Length - bigLeadingZeros)];
            result.Slice(0, leadingZeros).Fill(0);
            bigBytes.Slice(bigLeadingZeros).CopyTo(result.Slice(leadingZeros));

            return result.ToArray();
        }

        private static int LeadingZeros(ReadOnlySpan<byte> input)
        {
            var i = 0;
            for (i = 0; i < input.Length; ++i)
            {
                if (input[i] != 0)
                    break;
            }
            return i;
        }

        private static int LeadingZeros(ReadOnlySpan<char> input)
        {
            var i = 0;
            for (i = 0; i < input.Length; ++i)
            {
                if (input[i] != '0')
                    break;
            }
            return i;
        }

        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan()).ToString();

        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes)
        {
            Span<byte> bigBytes = stackalloc byte[bytes.Length + 1];
            bytes.CopyTo(bigBytes.Slice(1));
            bigBytes[0] = 0x00;
            bigBytes.Reverse();
            var big = new BigInteger(bigBytes.ToArray()).ToString().AsSpan();
            
            var leadingZeros = LeadingZeros(bytes);
            Span<char> result = stackalloc char[leadingZeros + big.Length];
            result.Slice(0, leadingZeros).Fill('0');
            big.CopyTo(result.Slice(leadingZeros));

            return result.ToArray();

        }
    }
}
