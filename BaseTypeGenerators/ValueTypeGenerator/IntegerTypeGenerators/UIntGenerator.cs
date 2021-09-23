using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class UIntGenerator : Generator
    {
        public override object Generate()
        {
            var buffer = new byte[4];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
