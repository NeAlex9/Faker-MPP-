using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BaseTypeGenerators;
using BaseTypeGenerators.FloatingPointTypeGenerators;
using BaseTypeGenerators.ReferenceTypeGenerator;
using BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators;
using BaseTypeGenerators.ValueTypeGenerator;
using Generators;

namespace Faker_lab2_
{
    internal class Faker
    {
        public List<Type> SystemTypes { get; private set; }

        public Dictionary<Type, Generator> BaseGenerators { get; private set; }
        public Dictionary<Type, GenericGenerator> GenericGenerators { get; private set; }

        public Faker()
        {
            SystemTypes = typeof(Assembly).Assembly.GetExportedTypes().ToList();
            this.BaseGenerators = new Dictionary<Type, Generator>
            {
                {typeof(byte), new ByteGenerator()},
                {typeof(sbyte), new SByteGenerator()},
                {typeof(short), new ShortGenerator()},
                {typeof(ushort), new UShortGenerator()},
                {typeof(int), new IntGenerator()},
                {typeof(uint), new UIntGenerator()},
                {typeof(long), new LongGenerator()},
                {typeof(ulong), new ULongGenerator()},
                {typeof(decimal), new DecimalGenerator()},
                {typeof(double), new DoubleGenerator()},
                {typeof(float), new FloatGenerator()},
                {typeof(bool), new BooleanGenerator()},
                {typeof(string), new StringGenerator()}
            };
            this.GenericGenerators = new Dictionary<Type, GenericGenerator>
            {
                { typeof(List<>), new ListGenerator(this.BaseGenerators) }
            };
        }

        public T Create<T>()
        {
            var type = typeof(T);
            var resultClass = (T)GetInstance(type);
            SetProperties<T>(ref resultClass);
            return (T)resultClass;
        }

        private Object GetInstance(Type type)
        {
            var ctor = type.GetConstructors()?.FirstOrDefault();
            var constructorParams = ctor.GetParameters();
            var generatedParams = new List<dynamic>();
            foreach (var param in constructorParams)
            {
                if (this.BaseGenerators.TryGetValue(param.ParameterType, out Generator generator))
                {
                    generatedParams.Add(generator.Generate());
                }
                else if (this.GenericGenerators.TryGetValue(param.ParameterType, out GenericGenerator genericGenerator))
                {
                    //generatedParams.Add(genericGenerator.Generate());
                }
            }

            return Activator.CreateInstance(type, generatedParams.ToArray());
        }

        private void SetProperties<T>(ref T instance)
        {
            var props = instance.GetType().GetProperties();
            foreach (var pInfo in props)
            {
                if (!(pInfo?.CanWrite ?? false))
                    continue;

                if (this.BaseGenerators.TryGetValue(pInfo.PropertyType, out Generator generator))
                {
                    pInfo.SetValue(instance, generator.Generate());
                }
                else if (this.GenericGenerators.TryGetValue(pInfo.PropertyType, out GenericGenerator genericGenerator))
                {
                    //generatedParams.Add(genericGenerator.Generate());
                }
            }
        }
    }
}
