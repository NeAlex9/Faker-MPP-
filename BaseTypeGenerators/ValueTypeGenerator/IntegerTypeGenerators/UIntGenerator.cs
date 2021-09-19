using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class UIntGenerator : Generator<uint>
    {
        private static readonly Random Random;

        static UIntGenerator()
        {
            Random = new Random();
        }

        public override uint Generate()
        {
            var buffer = new byte[4];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
