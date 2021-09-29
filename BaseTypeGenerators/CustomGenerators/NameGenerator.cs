using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.CustomGenerators
{
    public class NameGenerator : Generator
    {
        public List<string> AllowedStringSymbols { get; }

        public NameGenerator()
        {
            this.ElemType = typeof(string);
            this.AllowedStringSymbols = new List<string>
            {
                "Alex", "Natali", "Yan", "Marko", "Peter"
            };
        }

        public override object Generate()
        {
            return this.AllowedStringSymbols[Random.Next(5)].Clone();
        }
    }
}
