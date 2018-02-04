using System;
using System.Linq;

namespace Multiformats.Base
{
    internal class Identity : Multibase
    {
        protected override string Name => "identity";
        protected override char Prefix => '\0';
        protected override bool IsValid(string value) => true;

        internal override byte[] DecodeCore(string input) => input.Select(Convert.ToByte).ToArray();
        internal override string EncodeCore(byte[] bytes) => new string(bytes.Select(Convert.ToChar).ToArray());
    }
}