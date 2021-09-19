using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class IntGenerator : Generator<int>
    {
        private static readonly Random Random;

        static IntGenerator()
        {
            Random = new Random();
        }

        public override int Generate()
        {
            var buffer = new byte[4];
            Random.NextBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
