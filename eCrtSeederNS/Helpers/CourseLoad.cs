using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
    public static class CourseLoad
    {
        public static int GenerateCourseLoad(int min, int max)
            {
            Random seed = StaticRandom.Instance;
                       
            int CourseLoad = seed.Next(min, max);
        
            return CourseLoad;

            }
    }
}
