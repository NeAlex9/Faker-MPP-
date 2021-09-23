using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BaseTypeGenerators.FloatingPointTypeGenerators;
using BaseTypeGenerators.ReferenceTypeGenerator;
using BaseTypeGenerators.ValueTypeGenerator.IntegerTypeGenerators;
using Generators;

namespace Faker_lab2_
{
    internal class Faker
    {
        public List<Type> SystemTypes{ get; private set; }

        public Faker()
        {
            SystemTypes = typeof(Assembly).Assembly.GetExportedTypes().ToList();
        }


        public T Create<T>()
        {
            var type = typeof(T);
            var ctor = type.GetConstructors()?.FirstOrDefault();
            var constructorParams = ctor.GetParameters(); 
            var generatedParams = new List<dynamic>();
            foreach (var param in constructorParams)
            {
                generatedParams.Add(GetGenerator(param.ParameterType));
            }

            var resultClass = Activator.CreateInstance(type, generatedParams.ToArray());
            var props = resultClass.GetType().GetProperties();
            foreach (var pInfo in props)
            {
                if (!(pInfo?.CanWrite ?? false))
                    continue;

                pInfo.SetValue(resultClass, GetGenerator(pInfo.PropertyType).Generate());
            }

            // foreach (PropertyInfo pInfo in props)
            //     Console.WriteLine($"{pInfo.Name} : {pInfo.GetValue(resultClass)}");

            Console.WriteLine();
            Console.WriteLine();

            return (T)resultClass;
        }

        private bool SetIntegerTypeGenerator(Type type, ref Generator gen)
        {
            bool res = true;
            if (type == typeof(byte))
            {
                gen = new ByteGenerator();
            }
            else if (type == typeof(sbyte))
            {
                gen = new SByteGenerator();
            }
            else if(type == typeof(short))
            {
                gen = new ShortGenerator();
            }
            else if (type == typeof(ushort))
            {
                gen = new UShortGenerator();
            }
            else if(type == typeof(int))
            {
                gen = new IntGenerator();
            }
            else if (type == typeof(uint))
            {
                gen = new UIntGenerator();
            }
            else if(type == typeof(long))
            {
                gen = new LongGenerator();
            }
            else if (type == typeof(ulong))
            {
                gen = new ULongGenerator();
            }
            else
            {
                res = false;
            }

            return res;
        }

        private bool SetFloatingPointTypeGenerator(Type type, ref Generator gen)
        {
            bool res = true;
            if (type == typeof(decimal))
            {
                gen = new DecimalGenerator();
            }
            else if (type == typeof(double))
            {
                gen = new DoubleGenerator();
            }
            else if (type == typeof(float))
            {
                gen = new FloatGenerator();
            }
            else
            {
                res = false;
            }

            return res;
        }

        private bool SetReferenceTypeGenerator(Type type, ref Generator gen)
        {
            bool res = true;
            if (type == typeof(List<>))
            {
                gen = new ListGenerator<int>();
            }
            else if (type == typeof(string))
            {
                gen = new StringGenerator();
            }
            else
            {
                res = false;
            }

            return res;
        }

        private Generator GetGenerator(Type type)
        {
            Generator gen = null;
            if (SetIntegerTypeGenerator(type, ref gen))
            {
                return gen;
            }
            else if (SetFloatingPointTypeGenerator(type, ref gen))
            {
                return gen;
            }
            else if (SetReferenceTypeGenerator(type, ref gen))
            {
                return gen;
            }

            return null;
        }
    }
}
