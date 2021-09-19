using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.ReferenceTypeGenerator
{
    public class StringGenerator : Generator<string>
    {
        private static readonly Random Random;
        public string AllowedStringSymbols{ get; }

        static StringGenerator()
        {
            Random = new Random();
        }

        public StringGenerator()
        {
            this.AllowedStringSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        }

        public override string Generate()
        {
            return new string(Enumerable.Repeat(this.AllowedStringSymbols, this.AllowedStringSymbols.Length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
