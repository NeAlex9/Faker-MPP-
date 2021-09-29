using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker_lab2_
{
    public class DataTransfer
    {
        private DataTransfer(string name)
        {
            //Name = name;
        }

        public HeHe Hehe;

        private int PrivateFieldInt;
        public string Name;
        public List<bool> ListInt{ get; set; }
        public DateTime date{ get; set; }
        public char Char{ get; set; }

        public HaHa Haha;
    }

    public class HeHe
    {
        public string Name { get; private set; }

        public IEnumerable<string> lst;

        private HeHe(string name)
        {
            Name = name;
        }
    }

    public class HaHa
    {
        public int m;

        public DataTransfer dataTransfer;
    }
}
