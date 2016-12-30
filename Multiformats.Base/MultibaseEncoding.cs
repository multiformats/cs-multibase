namespace Multiformats.Base
{
    public abstract class MultibaseEncoding
    {
        /// <summary>
        /// Identifying chars of this encoding
        /// </summary>
        public abstract char[] Identifiers { get; }

        /// <summary>
        /// The default identifier (defaults to the first of Identifiers)
        /// </summary>
        public virtual char DefaultIdentifier => Identifiers[0];

        protected MultibaseEncoding() { }

        /// <summary>
        /// Encode bytes to Multibase encoded string
        /// </summary>
        /// <param name="data">Input data</param>
        /// <returns>Multibase encoded string</returns>
        public abstract string Encode(byte[] data);

        /// <summary>
        /// Decode a Multibase encoded string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Decoded bytes</returns>
        public abstract byte[] Decode(string str);
    }
}