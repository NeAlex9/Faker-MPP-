using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class LongGenerator : Generator
    {
        public LongGenerator()
        {
            this.ElemType = typeof(long);
        }

        public override object Generate()
        {
            var buffer = new byte[8];
            Random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
