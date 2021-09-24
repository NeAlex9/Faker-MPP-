using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.ReferenceTypeGenerator
{
    public class StringGenerator : Generator
    {
        public string AllowedStringSymbols{ get; }

        public StringGenerator() 
        {
            this.ElemType = typeof(string);
            this.AllowedStringSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        }

        public override object Generate()
        {
            return new string(Enumerable.Repeat(this.AllowedStringSymbols, this.AllowedStringSymbols.Length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
