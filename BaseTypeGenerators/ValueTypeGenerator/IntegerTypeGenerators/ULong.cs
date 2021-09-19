using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class ULongGenerator : Generator<ulong>
    {
        private static readonly Random Random;

        static ULongGenerator()
        {
            Random = new Random();
        }

        public override ulong Generate()
        {
            var buffer = new byte[8];
            Random.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }
    }
}
