using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.IntegerTypeGenerators
{
    public class UShortGenerator : Generator<ushort>
    {
        private static readonly Random Random;

        static UShortGenerator()
        {
            Random = new Random();
        }

        public override ushort Generate()
        {
            return (ushort)Random.Next(65536);
        }
    }
}
