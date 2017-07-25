using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
   
        public class RandomValueFromList
        {
            public string SelectRandomValueFromList(string[] array)
            {
                List<string> PersonalDataList = new List<string>();

                for (int i = 0; i < array.Length; i++)
                {
                    PersonalDataList.Add(array[i].ToString());
                }

                Random rnd = StaticRandom.Instance;

                int randomItemIndex = rnd.Next(PersonalDataList.Count);

                return PersonalDataList[randomItemIndex];
            }


        }
    }


