using System;

namespace Multiformats.Base
{
    internal class Base8 : Multibase
    {
        private static readonly char[] _alphabet = {'0', '1', '2', '3', '4', '5', '6', '7'};

        protected override string Name => "base8";
        protected override char Prefix => '7';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input) => Decode(input.AsSpan()).ToArray();

        public override ReadOnlyMemory<byte> Decode(ReadOnlySpan<char> input)
        {
            Span<char> bin = stackalloc char[input.Length * 3];
            for (var i = 0; i < bin.Length; i += 3)
            {
                var b = Convert.ToByte($"{input[i / 3]}", 8);
                bin[i] = ((byte)(b >> 2)) == 0 ? '0' : '1';
                bin[i + 1] = ((byte)((b >> 1) & 1)) == 0 ? '0' : '1';
                bin[i + 2] = ((byte)(b & 1)) == 0 ? '0' : '1';
            }

            var modlen = input.Length % 8;

            return _bases[MultibaseEncoding.Base2].Decode(bin.Slice(modlen == 6 ? 2 : modlen == 3 ? 1 : 0));
        }

        public override string Encode(byte[] bytes) => Encode(bytes.AsSpan()).ToString();

        public override ReadOnlyMemory<char> Encode(ReadOnlySpan<byte> bytes)
        {
            var encoded = _bases[MultibaseEncoding.Base2].Encode(bytes);
            var modlen = encoded.Length % 3;
            var prepad = modlen == 0 ? 0 : 3 - modlen;

            Span<char> result = stackalloc char[(prepad + encoded.Length) / 3];
            for (var i = 0; i < prepad + encoded.Length; i += 3)
            {
                result[i / 3] = Convert.ToString((byte)(
                    ((byte)(i < prepad || encoded.Span[i - prepad] == '0' ? 0 : 1) << 2) |
                    ((byte)(i + 1 < prepad || encoded.Span[(i - prepad) + 1] == '0' ? 0 : 1) << 1) |
                    (byte)(i + 2 < prepad || encoded.Span[(i - prepad) + 2] == '0' ? 0 : 1)
                ), 8)[0];
            }

            return result.ToArray();
        }
    }
}