using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker_lab2_
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            var dto = faker.Create<DataTransfer>();
        }
    }
}
