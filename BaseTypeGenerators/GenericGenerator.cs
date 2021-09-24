using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace BaseTypeGenerators
{
    public abstract class GenericGenerator
    {
        public static readonly Random Random;
        public Dictionary<Type, Generator> Generators{ get; private set; }

        public Type ElemType { get; protected set; }

        static GenericGenerator()
        {
            Random = new Random();
        }

        protected GenericGenerator(Dictionary<Type, Generator> generators)
        {
            this.Generators = generators;
        }

        public abstract object Generate(Type baseType);
    }
}
