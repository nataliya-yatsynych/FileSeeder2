using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
   public static class Filler

    {
        public static string AddFiller (int NofSpaces)
        {

            String CustomFiller  = new String(' ', NofSpaces);
            return CustomFiller;

        }
    }
}
