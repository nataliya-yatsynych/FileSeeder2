using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
    public class Grants
    {
        public int cSGP_LI_at_NBD_or_cSGP_PT_at_NBD { get; set; }
        public int cSGP_MI_at_NBD { get; set; }
        public int cSGP_PD_at_NBD { get; set; }
        public int NLAmount { get; set; }  //line 37 in NL eCert
        public int NBLAmount { get; set; } //line 37 in NB ecert
        public int NBBursary { get; set; } //line 51 in NB ecert
        public int NB_Grant { get; set; } //line 52 in NB ecert
        public int cSGP_FTDEP_at_the_NBD_or_cSGP_PTDEP_at_the_NBD { get; set; }
        public int cSGP_PDSE_at_the_NBD { get; set; }
        public int TotalAmount { get; set; } //Total CSL
        public int NL_provintial_grant { get; set; } //for NL ecert
        public int TransitionGrantYT { get; set; } //for YT ecert
        

    }     

    public class GenericRandomGrant
    {
        public int GrantValue()

        {
            Random RandomGrant = StaticRandom.Instance;

            List<int> GrantList = new List<int>();
            GrantList.Add(RandomGrant.Next(500, 1500));
            //GrantList.Add(0);
            //GrantList.Add(0);


            int RandomOption = RandomGrant.Next(GrantList.Count);

            int RandomGrantValue = GrantList[RandomOption];

            return RandomGrantValue;
        }
        
    }
}
