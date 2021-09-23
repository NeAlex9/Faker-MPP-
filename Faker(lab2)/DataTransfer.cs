using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker_lab2_
{
    class DataTransfer
    {
        private DataTransfer(string str)
        {
            WithoutSetString = str;
        }

        private int PrivateFieldInt;
        public string WithoutSetString{ get; }
        public long Long{ get; set; }
        public float PrivatePropFloat{ get; private set; }
        public bool Bool{ get; set; }
    }
}
