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

            string ServiceProvider = RandomData.RandomDigits(8);
            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "H" + Program.SequenceNumbereCertInHeader.ToString().PadLeft(9, '0') + Program.Originator.ToString() + ServiceProvider + FileCreationDate + Filler.AddFiller(825);
            return eCertHeader;
        }
        public static string AddEcertHeaderNL()
        {

            string ServiceProvider = "222";  //the only valid value as per datamap file
            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "H" + Program.SequenceNumbereCertInHeader.ToString().PadLeft(9, '0') + Program.Originator.ToString() + ServiceProvider + FileCreationDate + Filler.AddFiller(642);
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

            string eCertHeader = "H" + Program.SequenceNumbereCertInHeader.ToString().PadLeft(9, '0') + Program.Originator.ToString() + CurrentDate.GenerateTodayDate() + Filler.AddFiller(592);
            return eCertHeader;
        }

        public static string AddEcertHeaderPE()
        {

            string ServiceProvider = RandomData.RandomDigits(8);
            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "H" + Program.SequenceNumbereCertInHeader.ToString().PadLeft(9, '0') + Program.Originator.ToString() + ServiceProvider + FileCreationDate + Filler.AddFiller(825);
            return eCertHeader;
        }

        public static string AddEcertHeaderNB()
        {

            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "H" + Program.SequenceNumbereCertInHeader.ToString().PadLeft(9, '0') + Program.Originator.ToString() + "22" + FileCreationDate + Filler.AddFiller(886);
            return eCertHeader;
        }

        public static string AddEcertHeaderMB()
        {

            string FileCreationDate = CurrentDate.GenerateTodayDate();

            //Result String
            string eCertHeader = "H" + Program.SequenceNumber.ToString().PadLeft(9, '0') + Program.Originator.ToString() + "222" + FileCreationDate + Filler.AddFiller(580);
            return eCertHeader;
        }
        public static string AddMSFAAHeader()
        {
            string RecordTypeHeader = "100";
            string Title = "MSFAA SENT";
            string CreationDate = CurrentDate.GenerateTodayDate();
            string CreationTime = string.Format("{0:HHmm}", DateTime.Now);
           // string Sequence = "000002";

            string MSFAAHeader = RecordTypeHeader + Program.Originator.ToString().PadRight(4) + Title.PadRight(40) + CreationDate + CreationTime + Program.MSFAASequenceNumberInHeader.ToString().PadLeft(6,'0') + Filler.AddFiller(535);
            return MSFAAHeader;
        }
    }
}