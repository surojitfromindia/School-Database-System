using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBProject.Models
{
    class Bus
    {
        public string sid;
        public string bid;
        public int inst;
        public string rp;
        public DateTime pDate;
        public double fine;
        public int install;

        //For Search Purpose
        public Bus(string studentID)
        {
            sid = studentID;
        }
        //For Record Purpose
        public Bus(string studentId, string bid, int install,
            DateTime pDate, double fine, string report)
        {
            sid = studentId;
            this.bid = bid;
            inst = install;
            this.pDate = pDate;
            this.fine = fine;
            rp = report;
        }

        public bool Create()
        {
            string commandString = "insert into Bus values(@sid, @bid, @ins, @pdate, @fine, 'Paid')";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", sid);
            cmd.Parameters.AddWithValue("@bid", sid + "B");
            cmd.Parameters.AddWithValue("@ins", inst);
            cmd.Parameters.AddWithValue("@pdate", pDate);
            cmd.Parameters.AddWithValue("@fine", fine);
            int i = cmd.ExecuteNonQuery();
            return i == 1 ? true : false;
        }

        public static Bus FindBusI(string studenID)
        {
            Bus bs = null;
            string ss = "select * from Bus where Sid = @sid ";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", studenID);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                bs = new Bus(rd.GetString(0), rd.GetString(1), rd.GetInt32(2),
                    rd.GetDateTime(3), rd.GetDouble(4), rd.GetString(5));
            }
            rd.Close();
            return bs;
        }

    }
}
