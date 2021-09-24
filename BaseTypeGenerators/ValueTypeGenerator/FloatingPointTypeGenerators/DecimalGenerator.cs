using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.FloatingPointTypeGenerators
{
    public class DecimalGenerator : Generator
    {
        public DecimalGenerator()
        {
            this.ElemType = typeof(decimal);
        } 

        public override object Generate()
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
