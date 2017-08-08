using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCrtSeederNS
{
    class DBconnection
    {

        public static string server { get; set; }
        SqlConnection connection;
        SqlCommand command;

        public DBconnection()
        {
            connection = new SqlConnection();
            
            if (Program.EnvironmentIndicator == "S")
            {
                server = "vc01wsqlQA128.devservices.dh.com,1555";
            }
            else if (Program.EnvironmentIndicator== "D")
            {
                server = "vc01wsqldv128.devservices.dh.com,1555";
            }
            connection.ConnectionString = "Data Source="+server+";Initial Catalog=STLE_CSL_DES_ST;Integrated Security=True";
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
        } // constructor
        public int GetMCFAASqnInHeader()
        {
            connection.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select TOP 1 [dbo].[DesFileList].SeqInFilename, [dbo].[DesFileList].SeqInFileheader from [dbo].[DesFileList] inner join[dbo].[ValFileResult] on[dbo].[DesFileList].FileID = [dbo].[ValFileResult].FileID where ProvinceCode = '" + Program.Originator + "' and FileSource = 'MSFAA_SENT' and IsValidFlag = 1 and LastLoadStatus = 'C' order by SeqInHdr desc", connection);
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                Program.MSFAASequenceNumberInHeader = Convert.ToInt32(myReader["SeqInFileheader"]);
              
            }
            connection.Close();
            return Program.MSFAASequenceNumberInHeader+1;
        }

        public int GetMCFAASqnInFileName()
        {
            connection.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select TOP 1 [dbo].[DesFileList].SeqInFilename, [dbo].[DesFileList].SeqInFileheader from [dbo].[DesFileList] inner join[dbo].[ValFileResult] on[dbo].[DesFileList].FileID = [dbo].[ValFileResult].FileID where ProvinceCode = '" + Program.Originator + "' and FileSource = 'MSFAA_SENT' and LastLoadStatus = 'C' order by SeqInHdr desc", connection);
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                if(myReader["SeqInFilename"] == DBNull.Value)
                {
                    Program.MSFAASequenceNumberInFileName = 0;
                } else
                {
                    Program.MSFAASequenceNumberInFileName = Convert.ToInt32(myReader["SeqInFilename"]);

                }
            }
            connection.Close();
            return Program.MSFAASequenceNumberInFileName+1;
        }

        public int GetEcertSqnInHeader()
        {
            connection.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select TOP 1 SeqInFilename, SeqInFileheader  from [dbo].[DesFileList] where FileSource = 'ECERT_" + Program.Originator + "' and ProvinceCode = '" + Program.Originator + "' and LastLoadStatus = 'C' order by SysRecordCreateDt desc", connection);
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                if (myReader["SeqInFileheader"] == DBNull.Value)
                {
                    Program.SequenceNumbereCertInHeader = 0;
                }
                else
                {
                    Program.SequenceNumbereCertInHeader = Convert.ToInt32(myReader["SeqInFileheader"]);
                }

            }
            connection.Close();
            return Program.SequenceNumbereCertInHeader + 1;
        }

        public int GetEcertSqnInFileName()
        {
            connection.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select TOP 1 SeqInFilename, SeqInFileheader  from [dbo].[DesFileList] where FileSource = 'ECERT_" + Program.Originator + "' and ProvinceCode = '" + Program.Originator + "' and LastLoadStatus = 'C' order by SysRecordCreateDt desc", connection);
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                if (myReader["SeqInFilename"] == DBNull.Value)
                {
                    Program.SequenceNumbereCertInHeader = 0;
                } else
                {
                    Program.SequenceNumbereCertInFileName = Convert.ToInt32(myReader["SeqInFilename"]);
                }
                
            }
            connection.Close();
            return Program.SequenceNumbereCertInFileName + 1;
        }

        public int GetEcertDateInFileName()
        {
            connection.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select top 1 FileName from DesFileList where ProvinceCode = 'AB' and FileSource = 'ECERT_AB' and LastLoadStatus = 'C' order by FileName desc", connection);
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                if (myReader["FileName"] == DBNull.Value)
                {
                    Program.DateeCertInFileName = 0;
                } else
                {
                    Program.DateeCertInFileName = Convert.ToInt32($"{myReader["FileName"]}".Substring(14));
                }
            }
            connection.Close();
            return Program.DateeCertInFileName + 1;
        }

    }
}
