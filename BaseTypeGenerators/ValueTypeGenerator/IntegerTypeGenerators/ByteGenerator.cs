using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class ByteGenerator : Generator
    {
        public ByteGenerator()
        {
            this.ElemType = typeof(byte);
        }

        public override object Generate()
        {
            return (byte)Random.Next(256);
        }
    }
}
