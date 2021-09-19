using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class ByteGenerator : Generator<byte>
    {
        private static readonly Random Random;

        static ByteGenerator()
        {
            Random = new Random();
        }

        public override byte Generate()
        {
            return (byte)Random.Next(256);
        }
    }
}
