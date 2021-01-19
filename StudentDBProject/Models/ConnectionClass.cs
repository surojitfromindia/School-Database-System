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
        string USER_PASSWORD;
        public static OleDbConnection PublicConnection;
        public static string UserType;
        private static readonly string DB_PATH = "..\\..\\Resources\\Database\\school.mdb";



        public ConnectionClass(string user, string password)
        {
            USER_NAME = user;
            USER_PASSWORD = password;
        }

        public bool Login() {
            bool isLoginSuccesful = false;
            isAdmin();
            string u="", p ="";
            OleDbCommand cmd = new OleDbCommand("select UID, UPW from UserInfo where UID = @uid and UPW = @upw", PublicConnection);
            cmd.Parameters.AddWithValue("@uid", USER_NAME);
            cmd.Parameters.AddWithValue("@upw", USER_PASSWORD);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) {
                u = rd.GetString(0);
                p = rd.GetString(1);
            }
            if (USER_NAME == u && USER_PASSWORD == p && USER_NAME != "" && USER_PASSWORD!= "") {
                isLoginSuccesful = true;
            }
            return isLoginSuccesful;
        }

        private bool isAdmin()
        {
            OleDbCommand cmd = new OleDbCommand("select UTP from UserInfo where UID = @uid", PublicConnection);
            cmd.Parameters.AddWithValue("@uid", USER_NAME);
            string au = (string)cmd.ExecuteScalar();
            UserType = au; 
            return au == "Admin" ? true : false;
        }

        public static void ConnectToDB()
        {
            PublicConnection =
                new OleDbConnection($"provider=Microsoft.Jet.oledb.4.0; data source= {DB_PATH}");
            PublicConnection.Open();
        }

    }
}
