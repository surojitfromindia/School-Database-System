using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBProject.Models
{
    class Installment
    {
        public string sid; public int isnumber;
        public int inst; public double gst; public DateTime payDate; public double tFine; public string rp;
        public Installment(string sid, int isnumber,
            int inst, double gst, DateTime payDate, double tFine, string rp)
        {
            this.sid = sid;
            this.isnumber = isnumber;
            this.inst = inst;
            this.gst = gst;
            this.payDate = payDate;
            this.tFine = tFine;
            this.rp = rp;
        }


        public bool Create()
        {
            string commandString = "insert into installment values(@sid, @inum ,@ins, @gst, @pdate, @fin, 'Paid')";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", sid);
            cmd.Parameters.AddWithValue("@inum", isnumber);
            cmd.Parameters.AddWithValue("@ins", inst);
            cmd.Parameters.AddWithValue("@gst", gst);
            cmd.Parameters.AddWithValue("@padte", payDate);
            cmd.Parameters.AddWithValue("@fin", tFine);
            int i = cmd.ExecuteNonQuery();
            return i == 1 ? true : false;
        }

        public static int getLastInstallmentNumberOf(string id)
        {
            string commandString = "select max(inumber) from installment where sid = @sid";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", id);

            object b = cmd.ExecuteScalar();
            if (b != DBNull.Value)
                return (int)b;
            return 0;
        }

        public static Installment GetLastInstallment(string sid)
        {
            Installment lastInstallment = null;
            string commandString = "select * from installment where sid = @sid and " +
                "pDate =(select max(pDate) from installment where sid =@sid)";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", sid);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    lastInstallment = new Installment(rd.GetString(0), rd.GetInt32(1) ,rd.GetInt32(2),
                        rd.GetDouble(3), rd.GetDateTime(4), rd.GetDouble(5), rd.GetString(6));
                }
            }
            return lastInstallment;
        }

        public static List<Installment> FindAllInstallment(string studenID)
        {

            List<Installment> sl = new List<Installment>();
            string ss = "select * from Installment where SId = @sid ";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", studenID);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Installment s = new Installment(rd.GetString(0), rd.GetInt32(1), rd.GetInt32(2),
                        rd.GetDouble(3), rd.GetDateTime(4), rd.GetDouble(5), rd.GetString(6));
                sl.Add(s);
            }
            rd.Close();
            return sl;
        }
    }
}
