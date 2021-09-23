using System;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators
{
    public class ShortGenerator : Generator
    {

        public override object Generate()
        {
            return (short)Random.Next(-32768, 65536);
        }
    }
}
