
using System;
using System.Collections;
using System.Collections.Generic;
using Generators;

namespace BaseTypeGenerators.ReferenceTypeGenerator
{
    public class ListGenerator : GenericGenerator
    {
        public ListGenerator(Dictionary<Type,Generator> generators) : base(generators){}

        public override object Generate(Type baseType)
        {
            var len = Random.Next(10);
            IList result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(baseType));
            for (int i = 0; i < len; i++)
            {
                result.Add(Generators[baseType].Generate());
            }

            return result;
        }
    }
}
