using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.FloatingPointTypeGenerators
{
    public class DoubleGenerator : Generator
    {
        public DoubleGenerator()
        {
            this.ElemType = typeof(double);
        }

        public override object Generate()
        {
            var buffer = new byte[8];
            Random.NextBytes(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }
    }
}
