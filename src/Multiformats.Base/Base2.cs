﻿using System.Linq;

namespace Multiformats.Base
{
    internal class Base2 : Multibase
    {
        internal static readonly string ValidChars = "01";

        protected override string Name => "base2";
        protected override char Prefix => '0';
        protected override bool IsValid(string value) => value.All(c => ValidChars.Contains(c));

        internal override byte[] DecodeCore(string input)
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

        internal override string EncodeCore(byte[] bytes) => new string(bytes.Select(b => Enumerable.Range(0, 8).Select(i => (b & (1 << i)) != 0 ? '1' : '0').Reverse()).SelectMany(b => b).ToArray());
    }
}
