using System.Linq;

namespace Multiformats.Base
{
    internal class Base2 : Multibase
    {
        private static readonly char[] _alphabet = { '0', '1' };

        protected override string Name => "base2";
        protected override char Prefix => '0';
        protected override char[] Alphabet => _alphabet;

        public override byte[] Decode(string input)
        {
            var bytes = new byte[input.Length / 8];
            for (var index = 0; index < input.Length / 8; index++)
            {
                for (var i = 0; i < 8; i++)
                    if (input[(index * 8) + i] == '1')
                        bytes[index] |= (byte)(1 << (7 - i));
            }

            return bytes;
        }

        public override string Encode(byte[] bytes) => new string(bytes.Select(b => Enumerable.Range(0, 8).Select(i => (b & (1 << i)) != 0 ? '1' : '0').Reverse()).SelectMany(b => b).ToArray());
    }
}
