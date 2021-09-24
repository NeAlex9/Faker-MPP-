using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker_lab2_
{
    public class DataTransfer
    {
        private DataTransfer(HeHe str)
        {
            Hehe = str;
        }

        public int PrivateFieldInt;
        public string WithoutSetString{ get; set; }
        public List<bool[]> ListInt{ get; set; }
        public DateTime date{ get; set; }
        public char Char{ get; set; }

        private HeHe Hehe;

        public HaHa Haha;
    }

    public class HeHe
    {
        private int Number;

        public IEnumerable<string> lst;

        private HeHe(int n, object e)
        {
            Number = n;
        }
    }

    public class HaHa
    {
        public int m;

        public DataTransfer dataTransfer;
    }
}
