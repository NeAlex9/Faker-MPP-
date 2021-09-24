using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace CharGenerator
{
    public class CharGenerator : Generator
    {
        public string AllowedCharSymbols { get; }

        public CharGenerator()
        {
            this.ElemType = typeof(char);
            this.AllowedCharSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        }

        public override object Generate()
        {
            return (char)this.AllowedCharSymbols[Random.Next(this.AllowedCharSymbols.Length)];
        }
    }
}
