using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
    public static class RandomData
    {

        //private static Random rng = new Random(Environment.TickCount);

        public static string RandomDigits(int length)
        {
            var random = StaticRandom.Instance;
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }


}
