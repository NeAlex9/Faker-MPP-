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
        private readonly Stack<Type> _nestedTypes;

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
                {typeof(string), new StringGenerator()},
                {typeof(char), LoadPlugin(@"B:\BSUIR\3 course\5 sem\СПП\lab\Faker(lab2)\CharGenerator\bin\Debug\CharGenerator.dll", typeof(char))},
                {typeof(DateTime), LoadPlugin(@"B:\BSUIR\3 course\5 sem\СПП\lab\Faker(lab2)\SystemTypeGenerator\bin\Debug\SystemTypeGenerator.dll", typeof(DateTime))
                }
            };
            this.GenericGenerators = new Dictionary<Type, GenericGenerator>
            {
                { typeof(List<>), new ListGenerator(this.BaseGenerators) }
            };
            this._nestedTypes = new Stack<Type>();
        }

        public T Create<T>() where T : class
        {
            var type = typeof(T);
            this._nestedTypes.Push(type);
            var resultClass = Create(type);
            return resultClass as T;
        }

        public Generator LoadPlugin(string path, Type generatorType)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var inst = (Generator)Activator.CreateInstance(type);
                if (inst.ElemType == generatorType)
                {
                    return inst;
                }
            }

            return null;
        }

        private bool IsCorrectBaseValue(Type type, out Generator generator)
        {
            if (this.BaseGenerators.TryGetValue(type, out generator))
            {
                return true;
            }

            return false;
        }

        private bool IsCorrectGenericType(Type type, out GenericGenerator generator)
        {
            generator = null;
            if (type.IsGenericType)
            {
                if (this.GenericGenerators.TryGetValue(type.GetGenericTypeDefinition(), out generator))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsCustomClassTypeWithoutObsession(Type type)
        {
            if (type.IsClass && !type.IsArray && !this.SystemTypes.Contains(type) && !this._nestedTypes.Contains(type))
            {
                return true;
            }

            return false;
        }

        public object Create(Type objectType)
        {
            var resultClass = GetInstance(objectType);
            if (resultClass != null)
            {
                SetProperties(ref resultClass);
                SetFields(ref resultClass);
            }

            return resultClass;
        }

        private object GetInstance(Type type)
        {
            var ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.FirstOrDefault();
            var constructorParams = ctor.GetParameters();
            var generatedParams = new List<dynamic>();
            foreach (var param in constructorParams)
            {
                Type typeObj = param.ParameterType;
                if (IsCorrectBaseValue(typeObj, out Generator generator))
                {
                    generatedParams.Add(generator.Generate());
                }
                else if (IsCorrectGenericType(typeObj, out GenericGenerator genericGenerator))
                {
                    Type itemType = param.ParameterType.GetGenericArguments()[0];
                    generatedParams.Add(genericGenerator.Generate(itemType));
                }
                else if (IsCustomClassTypeWithoutObsession(typeObj))
                {
                    this._nestedTypes.Push(param.ParameterType);
                    var elem = Create(param.ParameterType);
                    generatedParams.Add(elem);
                    this._nestedTypes.Pop();
                }
            }

            try
            {
                return ctor.Invoke(generatedParams.ToArray());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void SetProperties(ref dynamic instance)
        {
            var props = instance.GetType().GetProperties();
            foreach (var pInfo in props)
            {
                Type typeObj = pInfo.PropertyType;
                if (!(pInfo?.CanWrite ?? false) || (pInfo?.SetMethod.IsPrivate ?? false))
                    continue;

                if (IsCorrectBaseValue(typeObj, out Generator generator))
                {
                    pInfo.SetValue(instance, generator.Generate());
                }
                else if (IsCorrectGenericType(typeObj, out GenericGenerator genericGenerator))
                {
                    Type itemType = pInfo.PropertyType.GetGenericArguments()[0];
                    pInfo.SetValue(instance, genericGenerator.Generate(itemType));
                }
                else if (IsCustomClassTypeWithoutObsession(typeObj))
                {
                    this._nestedTypes.Push(pInfo.PropertyType);
                    var elem = Create(pInfo.PropertyType);
                    pInfo.SetValue(instance, elem);
                    this._nestedTypes.Pop();
                }
            }
        }

        private void SetFields(ref dynamic instance)
        {
            var fields = instance.GetType().GetFields();
            foreach (var fieldInfo in fields)
            {
                Type typeObj = fieldInfo.FieldType;
                if (!fieldInfo.IsPublic)
                    continue;

                if (IsCorrectBaseValue(typeObj, out Generator generator))
                {
                    fieldInfo.SetValue(instance, generator.Generate());
                }
                else if (IsCorrectGenericType(typeObj, out GenericGenerator genericGenerator))
                {
                    Type itemType = fieldInfo.FieldType.GetGenericArguments()[0];
                    fieldInfo.SetValue(instance, genericGenerator.Generate(itemType));
                }
                else if (IsCustomClassTypeWithoutObsession(typeObj))
                {
                    this._nestedTypes.Push(fieldInfo.FieldType);
                    var elem = Create(fieldInfo.FieldType);
                    fieldInfo.SetValue(instance, elem);
                    this._nestedTypes.Pop();
                }
            }
        }
    }

    internal class GeneratorByPlugin
    {
        public Type ElementType { get; private set; }

        public Generator Generator{ get; private set;  }
    }
}
