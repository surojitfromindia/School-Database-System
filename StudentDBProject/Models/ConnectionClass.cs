using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBProject.Models
{
    class ConnectionClass
    {
        string USER_NAME;
        string PasswordT;
        public static OleDbConnection publicConnection;
        public static string Utype;
        public ConnectionClass(string u, string p)
        {
            USER_NAME = u;
            PasswordT = p;
        }

        public bool Login() {
            bool isLoginSuccesful = false;
            isAdmin();
            string u="", p ="";
            OleDbCommand cmd = new OleDbCommand("select UID, UPW from UserInfo where UID = @uid and UPW = @upw", publicConnection);
            cmd.Parameters.AddWithValue("@uid", USER_NAME);
            cmd.Parameters.AddWithValue("@upw", PasswordT);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) {
                u = rd.GetString(0);
                p = rd.GetString(1);
            }
            if (USER_NAME == u && PasswordT == p && USER_NAME != "" && PasswordT!= "") {
                isLoginSuccesful = true;
            }
            return isLoginSuccesful;
        }

        private bool isAdmin()
        {
            OleDbCommand cmd = new OleDbCommand("select UTP from UserInfo where UID = @uid", publicConnection);
            cmd.Parameters.AddWithValue("@uid", USER_NAME);
            string au = (string)cmd.ExecuteScalar();
            Utype = au; 
            return au == "Admin" ? true : false;
        }

        public static void ConnectToDB()
        {
            string dbPath ="..\\..\\Resources\\Database\\school.mdb";
            Console.WriteLine(dbPath);
            publicConnection = new OleDbConnection($"provider=Microsoft.Jet.oledb.4.0; data source= {dbPath}");
            publicConnection.Open();
        }

    }
}
