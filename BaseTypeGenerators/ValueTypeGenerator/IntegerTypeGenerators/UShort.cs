using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class UShortGenerator : Generator
    {
        public UShortGenerator()
        {
            this.ElemType = typeof(ushort);
        }

        public override object Generate()
        {
            return (ushort)Random.Next(65536);
        }
    }
}
