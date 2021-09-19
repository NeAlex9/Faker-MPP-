using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class ShortGenerator : Generator<short>
    {
        private static readonly Random Random;

        static ShortGenerator()
        {
            Random = new Random();
        }

        public override short Generate()
        {
            return (short)Random.Next(-32768, 65536);
        }
    }
}
