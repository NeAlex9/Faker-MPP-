using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.FloatingPointTypeGenerators
{
    public class DecimalGenerator : Generator<decimal>
    {
        private static readonly Random Random;

        static DecimalGenerator()
        {
            Random = new Random();
        }

        public override decimal Generate()
        {
            bool sign = Random.Next(2) == 1;
            return new decimal(Random.Next() - -2147483648,
                Random.Next() - -2147483648,
                Random.Next() - -2147483648,
                sign,
                (byte)Random.Next(29));
        }
    }
}
