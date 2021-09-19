using System;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class SByteGenerator : Generator<sbyte>
    {
        private static readonly Random Random;

        static SByteGenerator()
        {
            Random = new Random();
        }

        public override sbyte Generate()
        {
            return (sbyte)Random.Next(-128, 256);
        }
    }
}
