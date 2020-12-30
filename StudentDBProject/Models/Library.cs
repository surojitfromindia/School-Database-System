using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBProject.Models
{
    class Library
    {
        public string sid;
        public string bname;
        public string rp;
        public DateTime iDate;
        public DateTime rDate;
        public double fine;

        //For Search Purpose
        public Library(string studentID)
        {
            sid = studentID;
        }
        //For Record Purpose
        public Library(string studentId, string bookName,
            DateTime iDate, DateTime rDate, double fine, string report)
        {
            sid = studentId;
            bname = bookName;
            rp = report;
            this.iDate = iDate;
            this.rDate = rDate;
            this.fine = fine;
        }


        public bool Create()
        {
            string commandString = "insert into Library values(@sid, @lid, @bname, @idate, @rdate, 0,'Hold')";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", sid);
            cmd.Parameters.AddWithValue("@lid", sid + "L");
            cmd.Parameters.AddWithValue("@bname", bname);
            cmd.Parameters.AddWithValue("@idate", iDate);
            cmd.Parameters.AddWithValue("@rdate", rDate);
            int i = cmd.ExecuteNonQuery();
            return i == 1 ? true : false;
        }

        public static bool Update(Library newLib)
        {
            //First Delete
            OleDbCommand cmd0 = new OleDbCommand("delete from Library where SId = @sid and IDate = (select max(IDate) from Library where SId = @sid)", ConnectionClass.publicConnection);
            cmd0.Parameters.AddWithValue("@sid", newLib.sid);
            int i0 = cmd0.ExecuteNonQuery();

            //Then Add again
            string commandString = "insert into Library values(@sid, @lid, @bname, @idate, @rdate, @fine,'Returned')";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", newLib.sid);
            cmd.Parameters.AddWithValue("@lid", newLib.sid + "L");
            cmd.Parameters.AddWithValue("@bname", newLib.bname);
            cmd.Parameters.AddWithValue("@idate", newLib.iDate);
            cmd.Parameters.AddWithValue("@rdate", newLib.rDate);
            cmd.Parameters.AddWithValue("@fine", newLib.fine);
            int i = cmd.ExecuteNonQuery();
            return (i == 1 && i0 == 1) ? true : false;
        }

        public static Library FindLastEntry(string studenID)
        {
            Library s = null;
            string commandString = "select * from Library where SId = @sid and IDate = (select max(IDate) from Library where SId = @sid)";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", studenID);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                TimeSpan fineCalculation = DateTime.Today.Subtract(rd.GetDateTime(4));
                int t = (int)fineCalculation.TotalDays;
                double fine = 0;
                if (t > -1)
                {
                    fine = 1;
                    fine = 10.25 * t;
                }
                s = new Library(rd.GetString(0), rd.GetString(2),
                    rd.GetDateTime(3), rd.GetDateTime(4), fine, rd.GetString(6));
            }
            rd.Close();
            return s;
        }

        public static List<Library> FindAllBooks(string studenID)
        {

            List<Library> sl = new List<Library>();
            string ss = "select * from Library where SId = @sid ";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", studenID);
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Library s = new Library(rd.GetString(0), rd.GetString(2),
                     rd.GetDateTime(3), rd.GetDateTime(4), rd.GetDouble(5), rd.GetString(6));
                sl.Add(s);
            }
            rd.Close();
            return sl;
        }


    }


}
