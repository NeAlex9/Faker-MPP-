using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generators;

namespace SystemTypeGenerator
{
    public class DateTimeGenerator : Generator
    {
        public override object Generate()
        {
            int year = Random.Next(DateTime.MinValue.Year, DateTime.MaxValue.Year + 1);
            int month = Random.Next(1, 13);
            int day = Random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            int hour = Random.Next(0, 24);
            int minute = Random.Next(0, 60);
            int second = Random.Next(0, 60);
            int millisecond = Random.Next(0, 1000);
            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }
    }


}
