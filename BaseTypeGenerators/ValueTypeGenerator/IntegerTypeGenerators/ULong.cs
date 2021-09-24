using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class ULongGenerator : Generator
    {
        public ULongGenerator()
        {
            this.ElemType = typeof(ulong);
        }


        public override object Generate()
        {
            var buffer = new byte[8];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
