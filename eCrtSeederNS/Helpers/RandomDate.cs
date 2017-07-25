using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
    public static class RandomDate
    {
        public static string GenerateRandomDate(int minYear, int maxYear)
           
        {
            
            Random seed = StaticRandom.Instance;
            int year = seed.Next(minYear, maxYear);
            int month = seed.Next(1, 12);
            int day = DateTime.DaysInMonth(year, month);

            int Day = seed.Next(1, day);

            DateTime dt = new DateTime(year, month, Day);
            return dt.ToString("yyyyMMdd");
        }
    }

}
