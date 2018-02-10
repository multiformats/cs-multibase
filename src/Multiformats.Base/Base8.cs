using System;
using System.Collections.Generic;
using System.Linq;

namespace Multiformats.Base
{
    internal class Base8 : Multibase
    {
        private static readonly char[] _alphabet = {'0', '1', '2', '3', '4', '5', '6', '7'};

        protected override string Name => "base8";
        protected override char Prefix => '7';
        protected override char[] Alphabet => _alphabet;

        private static byte[] FromOct(byte o) => new[] { (byte)(o >> 2), (byte)((o >> 1) & 1), (byte)(o & 1) };
        private static byte ToNum8(char c) => Convert.ToByte($"{c}", 8);
        private static char FromBit(byte b) => b == 0 ? '0' : '1';

        public override byte[] Decode(string input)
        {
            var base2 = _bases[MultibaseEncoding.Base2];

            var bin = input.Select(ToNum8).SelectMany(FromOct).Select(FromBit);

            var modlen = input.Length % 8;

            var binstr = "";
            if (modlen == 0)
                binstr = new string(bin.ToArray());
            else if (modlen == 3)
                binstr = new string(bin.Skip(1).ToArray());
            else if (modlen == 6)
                binstr = new string(bin.Skip(2).ToArray());

            return base2.Decode(binstr);
        }

        private static byte ToBit(char c) => c == '0' ? (byte)0 : (byte)1;
        private static byte ToOct(IEnumerable<byte> b)
        {
            var bin = b.ToArray();

            return (byte)((bin[0] << 2) | (bin[1] << 1) | bin[2]);
        }
        private static char FromNum8(byte b) => Convert.ToString(b, 8).First();

        private static IEnumerable<byte> BinToOct(IEnumerable<byte> b)
        {
            var result = new List<byte>();
            var batch = new List<byte>();
            foreach (var x in b)
            {
                batch.Add(x);

                if (batch.Count == 3)
                {
                    result.Add(ToOct(batch));
                    batch.Clear();
                }
            }

            return result;
        }

        public override string Encode(byte[] bytes)
        {
            var base2 = _bases[MultibaseEncoding.Base2];
            var encoded = base2.Encode(bytes);
            var modlen = encoded.Length % 3;
            var prepad = new string('0', modlen == 0 ? 0 : 3 - modlen);

            return new string(BinToOct((prepad + encoded).Select(ToBit)).Select(FromNum8).ToArray());
        }
    }
}