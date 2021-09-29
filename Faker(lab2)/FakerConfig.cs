using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace Faker_lab2_
{ 
    public class FakerConfig
    {
        public Dictionary<MemberInfo, Generator> CustomGenerators{ get; private set; }

        public FakerConfig()
        {
            this.CustomGenerators = new Dictionary<MemberInfo, Generator>();
        }

        public void Add<DTObjectType, MemberType, GeneratorType>(Expression<Func<DTObjectType, MemberType>> expression) 
            where DTObjectType : class
            where GeneratorType : Generator
        {
            Expression expressionBody = expression.Body;
            if (expressionBody.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Invalid expression");
            }

            Generator generator = (Generator)Activator.CreateInstance(typeof(GeneratorType));
            if (generator.ElemType != typeof(MemberType))
            {
                throw new ArgumentException("Invalid generator");
            }

            CustomGenerators.Add(((MemberExpression)expressionBody).Member, generator);
        }
    }
}
