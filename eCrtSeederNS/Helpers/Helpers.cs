using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eCrtSeederNS
{

     
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
    public static class SpecialCharacters
    {
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
    public static class MidPoint
    {
        public static int ValueAtMidPoint(int GrantValue)
        {
            if (GrantValue != 0)
            {
                return (GrantValue / 2);
            }
            return GrantValue;
        }
    }
    public static class CurrentDate
    {
        public static string GenerateTodayDate()
        {
            string CreationDate = DateTime.Now.ToString("yyyyMMdd");
            return CreationDate;
        }

        public static string GenerateTodayDateJulian()
        {
            JulianCalendar calendar = new JulianCalendar();
            var today = DateTime.Today;
            var dateInJulian = calendar.ToDateTime(today.Year, today.Month, today.Day, today.Hour, today.Minute, today.Second, today.Millisecond);
           // var TodayDateJulian = string.Format("{0}{1}", dateInJulian.ToString("yyyy"), dateInJulian.DayOfYear);
            var TodayDateJulian = string.Format("{0}{1}", today.ToString("yyyy"), today.DayOfYear);
            return TodayDateJulian;

            
        }
        }


    
}