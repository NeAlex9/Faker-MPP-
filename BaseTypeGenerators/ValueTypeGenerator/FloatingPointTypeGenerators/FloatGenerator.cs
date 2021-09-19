using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.FloatingPointTypeGenerators
{
    public class FloatGenerator : Generator<float>
    {
        private static readonly Random Random;

        static FloatGenerator()
        {
            Random = new Random();
        }

        public override float Generate()
        {
            var buffer = new byte[4];
            Random.NextBytes(buffer);
            return BitConverter.ToSingle(buffer, 0);
        }
    }
}
