using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators.ValueTypeGenerator
{
    public class CharGenerator : Generator<char>
    {
        private static readonly Random Random;
        public string AllowedCharSymbols { get; }

        static CharGenerator()
        {
            Random = new Random();
        }

        public CharGenerator()
        {
            this.AllowedCharSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        }

        public override char Generate()
        {
            return (char)this.AllowedCharSymbols[Random.Next(this.AllowedCharSymbols.Length)];
        }
    }
}
