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
            d = str;
        }

        private int PrivateFieldInt;
        public string WithoutSetString{ get; }
        private List<bool> ListInt{ get; set; }
        public System.Object PropFloat{ get; set; }
        public bool Bool{ get; set; }

        private HeHe d;

        public HaHa Haha;
    }

    public class HeHe
    {
        private int Number;

        public List<string> lst;

        private HeHe(int n)
        {
            Number = n;
        }
    }

    public class HaHa
    {
        public int m;
    }
}
