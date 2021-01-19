using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBProject.Models
{
    public class Bus
    {
        public string StudentID { get; private set; }
        public string BusId { get; private set; }
        public int Installment { get; private set; }
        public string Report { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public double FineAmount { get; private set; }


        public Bus()
        {
        }


        //For Record Purpose
        public Bus(string studentId, string bid, int install,
            DateTime pDate, double fine, string report)
        {
            StudentID = studentId;
            BusId = bid;
            Installment = install;
            PaymentDate = pDate;
            FineAmount = fine;
            Report = report;
        }

        public void Deconstruct(out string studentId, out string busId, out int install,
           out DateTime pDate, out double fine, out string report)
        {
            studentId = StudentID;
            busId = BusId;
            install = Installment;
            pDate = PaymentDate;
            fine = FineAmount;
            report = Report;
        }

        public bool Create()
        {
            string commandString = "insert into Bus values(@sid, @bid, @ins, @pdate, @fine, 'Paid')";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", StudentID);
            cmd.Parameters.AddWithValue("@bid", StudentID + "B");
            cmd.Parameters.AddWithValue("@ins", Installment);
            cmd.Parameters.AddWithValue("@pdate", PaymentDate);
            cmd.Parameters.AddWithValue("@fine", FineAmount);
            int i = cmd.ExecuteNonQuery();
            return i == 1 ? true : false;
        }

        public static Bus FindBusI(string studenID)
        {
            Bus bs = null;
            string ss = "select * from Bus where Sid = @sid ";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", studenID);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                bs = new Bus(rd.GetString(0), rd.GetString(1), rd.GetInt32(2), rd.GetDateTime(3), rd.GetDouble(4), rd.GetString(5));
            }
            rd.Close();
            return bs;
        }

    }
}
