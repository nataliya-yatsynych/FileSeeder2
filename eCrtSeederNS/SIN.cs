using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eCrtSeederNS
{

public static class Luhn
    {
        public static bool LuhnCheck(this string cardNumber)
        {
            return LuhnCheck(cardNumber.Select(c => c - '0').ToArray());
        }

        private static bool LuhnCheck(this int[] digits)
        {
            return GetCheckValue(digits) == 0;
        }

        private static int GetCheckValue(int[] digits)
        {
            return digits.Select((d, i) => i % 2 == digits.Length % 2 ? ((2 * d) % 10) + d / 5 : d).Sum() % 10;
        }
    }

    public static class ValidSIN
    {
        //ValidSIN SIN = new ValidSIN();
        public static int testNumber { get; set; }
        public static bool res { get; set; }

        public static int GenerateValidSIN()
        {
            Random rnd = StaticRandom.Instance;
            do
            {

                testNumber = rnd.Next(100000000, 999999999);
                res = testNumber.ToString().LuhnCheck();
            }
           
            while (!res);

            return testNumber;


        }
    }
      
       

    public static class StaticRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }
}

