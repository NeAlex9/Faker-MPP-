using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public abstract class Generator
    {
        protected static readonly Random Random;

        public Type  ElemType { get; protected set; }

        static Generator()
        {
            Random = new Random();
        }

        public abstract object Generate();
    }
}
