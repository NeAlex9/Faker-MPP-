using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseTypeGenerators.CustomGenerators;
using BaseTypeGenerators.ReferenceTypeGenerator;

namespace Faker_lab2_
{
    class Program
    {
        static void Main(string[] args)
        {
            var fakerConfig = new FakerConfig();
            fakerConfig.Add<HeHe, string, NameGenerator>(elem => elem.Name);
            var faker = new Faker(fakerConfig);
            var dto = faker.Create<DataTransfer>();
            Console.ReadLine();
        }
    }
}
