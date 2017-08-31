using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
    public static class Header
    {
        
        public static string AddEcertHeaderNS()
        {
            DBconnection obj2 = new DBconnection();
            string ServiceProvider = RandomData.RandomDigits(8);
            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "H" + obj2.GetEcertSqnInHeader().ToString().PadLeft(9, '0') + Program.Originator.ToString() + ServiceProvider + FileCreationDate + Filler.AddFiller(825);
            return eCertHeader;
        }
        public static string AddEcertHeaderNL()
        {
            DBconnection obj2 = new DBconnection();
            string ServiceProvider = "222";  //the only valid value as per datamap file
            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "H" + obj2.GetEcertSqnInHeader().ToString().PadLeft(9, '0') + Program.Originator.ToString() + ServiceProvider + FileCreationDate + Filler.AddFiller(642);
            return eCertHeader;
        }

        public static string AddEcertHeaderAB()
        {

            string FileCreationDate = CurrentDate.GenerateTodayDate();
            string CreationTime = string.Format("{0:HHmmss}", DateTime.Now);
            string FileTitle = "CSL DOCUMENTS";

            //Result String
            string eCertHeader = "01" + Program.Originator.ToString() + FileTitle.PadRight(40)+ FileCreationDate + CreationTime+ Filler.AddFiller(5) /*for not used sequence number*/ + Filler.AddFiller(207);
            return eCertHeader;
        }

        public static string AddEcertHeaderYT()
        {

            DBconnection obj2 = new DBconnection();
            string eCertHeader = "H" + obj2.GetEcertSqnInHeader().ToString().PadLeft(9, '0') + Program.Originator.ToString() + CurrentDate.GenerateTodayDate() + Filler.AddFiller(592);
            return eCertHeader;
        }

        public static string AddEcertHeaderPE()
        {

            string ServiceProvider = RandomData.RandomDigits(8);
            string FileCreationDate = CurrentDate.GenerateTodayDate();
            DBconnection obj2 = new DBconnection();
            //Result String
            string eCertHeader = "H" + obj2.GetEcertSqnInHeader().ToString().PadLeft(9, '0') + Program.Originator.ToString() + ServiceProvider + FileCreationDate + Filler.AddFiller(825);
            return eCertHeader;
        }

        public static string AddEcertHeaderNB()
        {

            string FileCreationDate = CurrentDate.GenerateTodayDate();
            DBconnection obj2 = new DBconnection();
            //Result String
            string eCertHeader = "H" + obj2.GetEcertSqnInHeader().ToString().PadLeft(9, '0') + Program.Originator.ToString() + "22" + FileCreationDate + Filler.AddFiller(886);
            return eCertHeader;
        }

        public static string AddEcertHeaderMB()
        {

            string FileCreationDate = CurrentDate.GenerateTodayDate();
            DBconnection obj2 = new DBconnection();
            //Result String
            string eCertHeader = "H" + obj2.GetEcertSqnInHeader().ToString().PadLeft(9, '0') + Program.Originator.ToString() + "222" + FileCreationDate + Filler.AddFiller(580);
            return eCertHeader;
        }

        public static string AddEcertHeaderSK()
        {

            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "00SK 15 " + FileCreationDate + Filler.AddFiller(364);
            return eCertHeader;
        }
        public static string AddEcertHeaderON()
        {

            string FileCreationDate = CurrentDate.GenerateTodayDate();
            DBconnection obj2 = new DBconnection();

            //Result String
            string eCertHeader = "H" + obj2.GetEcertSqnInHeader().ToString().PadLeft(9, '0')+ Program.Originator.ToString()+FileCreationDate + Filler.AddFiller(480);
            return eCertHeader;
        }
        public static string AddMSFAAHeader()
        {
            string RecordTypeHeader = "100";
            string Title = "MSFAA SENT";
            string CreationDate = CurrentDate.GenerateTodayDate();
            string CreationTime = string.Format("{0:HHmm}", DateTime.Now);
            DBconnection obj2 = new DBconnection();

            // string Sequence = "000002";

            string MSFAAHeader = RecordTypeHeader + Program.Originator.ToString().PadRight(4) + Title.PadRight(40) + CreationDate + CreationTime + obj2.GetMCFAASqnInHeader().ToString().PadLeft(6,'0') + Filler.AddFiller(535);
            return MSFAAHeader;
        }
    }
}