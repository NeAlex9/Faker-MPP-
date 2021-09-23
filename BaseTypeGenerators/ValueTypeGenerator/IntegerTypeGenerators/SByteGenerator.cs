using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class SByteGenerator : Generator
    {
        public override object Generate()
        {
            return (sbyte)Random.Next(-128, 256);
        }
    }
}
