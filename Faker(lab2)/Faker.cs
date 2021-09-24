using System;
using System.Collections;
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
        public List<Type> SystemTypes { get; }

        public Dictionary<Type, Generator> BaseGenerators { get; private set; }
        public Dictionary<Type, GenericGenerator> GenericGenerators { get; private set; }
        private Stack<Type> _nestedTypes; 

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
            this._nestedTypes = new Stack<Type>();
        }

        public T Create<T>()
        {
            var type = typeof(T);
            var resultClass = Create(type);
            return (T)resultClass;
        }

        public object Create(Type objectType)
        {
            var resultClass = GetInstance(objectType);
            SetProperties(ref resultClass);
            SetFields(ref resultClass);
            return resultClass;
        }

        private Object GetInstance(Type type)
        {
            var ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.FirstOrDefault();
            var constructorParams = ctor.GetParameters();
            var generatedParams = new List<dynamic>();
            foreach (var param in constructorParams)
            {
                if (this.BaseGenerators.TryGetValue(param.ParameterType, out Generator generator))
                {
                    generatedParams.Add(generator.Generate());
                }
                else if (param.ParameterType.IsGenericType)
                {
                    if (this.GenericGenerators.TryGetValue(param.ParameterType.GetGenericTypeDefinition(),
                        out GenericGenerator genericGenerator))
                    {
                        Type itemType = param.ParameterType.GetGenericArguments()[0];
                        generatedParams.Add(genericGenerator.Generate(itemType));
                    }
                }
                else if (param.ParameterType.IsClass && !this.SystemTypes.Contains(param.ParameterType))
                {
                    var elem = Create(param.ParameterType);
                    generatedParams.Add(elem);
                }
            }

            return ctor.Invoke(generatedParams.ToArray());
        }

        private void SetProperties(ref dynamic instance)
        {
            var props = instance.GetType().GetProperties();
            foreach (var pInfo in props)
            {
                if (!(pInfo?.CanWrite ?? false) || (pInfo?.SetMethod.IsPrivate ?? false))
                    continue;

                if (this.BaseGenerators.TryGetValue(pInfo.PropertyType, out Generator generator))
                {
                    pInfo.SetValue(instance, generator.Generate());
                }
                else if (pInfo.PropertyType.IsGenericType)
                {
                    if (this.GenericGenerators.TryGetValue(pInfo.PropertyType.GetGenericTypeDefinition(),
                        out GenericGenerator genericGenerator))
                    {
                        Type itemType = pInfo.PropertyType.GetGenericArguments()[0];
                        pInfo.SetValue(instance, genericGenerator.Generate(itemType));
                    }
                }
                else if (pInfo.PropertyType.IsClass && !this.SystemTypes.Contains(pInfo.PropertyType))
                {
                    var elem = Create(pInfo.PropertyType);
                    pInfo.SetValue(instance, elem);
                }
            }
        }

        private void SetFields(ref dynamic instance)
        {
            var fields = instance.GetType().GetFields();
            foreach (var fieldInfo in fields)
            {
                if (!fieldInfo.IsPublic)
                    continue;

                if (this.BaseGenerators.TryGetValue(fieldInfo.FieldType, out Generator generator))
                {
                    fieldInfo.SetValue(instance, generator.Generate());
                }
                else if (fieldInfo.FieldType.IsGenericType)
                {
                    if (this.GenericGenerators.TryGetValue(fieldInfo.FieldType.GetGenericTypeDefinition(),
                        out GenericGenerator genericGenerator))
                    {
                        Type itemType = fieldInfo.FieldType.GetGenericArguments()[0];
                        fieldInfo.SetValue(instance, genericGenerator.Generate(itemType));
                    }
                }
                else if (fieldInfo.FieldType.IsClass && !this.SystemTypes.Contains(fieldInfo.FieldType))
                {
                    var elem = Create(fieldInfo.FieldType);
                    fieldInfo.SetValue(instance, elem);
                }
            }
        }
    }
}
