using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator
{
    public class BooleanGenerator : Generator
    {
        public override object Generate()
        {
            return Random.Next(2) != 0;
        }
    }
}
