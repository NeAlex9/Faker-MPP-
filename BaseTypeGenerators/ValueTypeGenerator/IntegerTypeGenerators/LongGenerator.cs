using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class LongGenerator : Generator<long>
    {
        private static readonly Random Random;

        static LongGenerator()
        {
            Random = new Random();
        }

        public override long Generate()
        {
            var buffer = new byte[8];
            Random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
