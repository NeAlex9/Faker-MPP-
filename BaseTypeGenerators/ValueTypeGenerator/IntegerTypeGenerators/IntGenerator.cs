using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class IntGenerator : Generator
    {
        public override object Generate()
        {
            var buffer = new byte[4];
            Random.NextBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
