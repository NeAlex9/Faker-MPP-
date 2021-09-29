using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseTypeGenerators.ReferenceTypeGenerator;

namespace Faker_lab2_
{
    class Program
    {
        static void Main(string[] args)
        {
            var fakerConfig = new FakerConfig();
            fakerConfig.Add<DataTransfer, string, StringGenerator>(elem => elem.WithSetString);
            var faker = new Faker();
            var dto = faker.Create<DataTransfer>();
            Console.ReadLine();
        }
    }
}
