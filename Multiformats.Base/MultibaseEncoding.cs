namespace Multiformats.Base
{
    public abstract class MultibaseEncoding
    {
        public abstract char[] Identifiers { get; }
        public virtual char DefaultIdentifier => Identifiers[0];

        protected MultibaseEncoding() { }

        public abstract string Encode(byte[] data);
        public abstract byte[] Decode(string str);
    }
}