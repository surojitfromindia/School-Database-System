using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDBProject.Models
{
    class Student
    {
        public string studentName;
        public string studentId;
        public string FatherName;
        public string address;
        public string phone;
        public string VID;
        public string ClassName;
        public int RollNumber;
        public string section;
        public string LibraCard;
        public string BusCard;


        public Student(string sName)
        {
            studentName = sName;
        }

        public Student(string studentId, string studentName,
         string FatherName,
         string address,
         string phone,
         string VID,
         string ClassName,
         int RollNumber,
         string section,
         string LibraCard,
         string BusCard)
        {
            this.studentName = studentName;
            this.studentId = studentId;
            this.FatherName= FatherName;
            this.address=address;
            this.phone = phone;
            this.VID= VID;
            this.ClassName= ClassName;
            this.RollNumber = RollNumber;
            this.section = section;
            this.LibraCard = LibraCard;
            this.BusCard = BusCard;
        }

        public bool Create() {
            string ss = "insert into Student values(@uid, @uname, @ufaname, @uadd," +
                " @uph, @uvid, @uclass, @uRoll, @usec, @ulib, @ubus)";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.publicConnection);           
            cmd.Parameters.AddWithValue("@uid", studentId);
            cmd.Parameters.AddWithValue("@uname", studentName);
            cmd.Parameters.AddWithValue("@ufaname", FatherName);
            cmd.Parameters.AddWithValue("@uadd", address);
            cmd.Parameters.AddWithValue("@uph", phone);
            cmd.Parameters.AddWithValue("@uvid", VID);
            cmd.Parameters.AddWithValue("@uclass", ClassName);
            cmd.Parameters.AddWithValue("@uRoll", RollNumber);
            cmd.Parameters.AddWithValue("@usec", section);
            cmd.Parameters.AddWithValue("@ulib", LibraCard);
            cmd.Parameters.AddWithValue("@ubus", BusCard);
            int i =cmd.ExecuteNonQuery();
            return i==1 ? true : false;
        }

        public static int nextRoll(string className) {

            int p = 1;
            string ss = "select max(RNo) from Student where Cla = @clas";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@clas", className);
            object j = cmd.ExecuteScalar();
            if (j != DBNull.Value)
            {
                p = (int)j+1;
            }
            return p;
        }

        public static bool deleteRecord(string studentID) {
            string commandString = "delete from Student where SId = @uid";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@uid", studentID);
            int i = cmd.ExecuteNonQuery();

            commandString = "delete from Bus where Sid = @sid;";
            cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            cmd.ExecuteNonQuery();

            commandString = "delete from installment where Sid = @sid;";
            cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            cmd.ExecuteNonQuery();

            commandString = "delete from Library where Sid = @sid;";
            cmd = new OleDbCommand(commandString, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            cmd.ExecuteNonQuery();

            return i == 1 ? true : false;

        }

        public static bool update(Student newInfo)
        {
            OleDbCommand cmd = new OleDbCommand("delete from Student where SId = @uid", ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@uid", newInfo.studentId);
            int i = cmd.ExecuteNonQuery();
            bool i2 = newInfo.Create();
            return (i==1 && i2) ? true : false;
        }

        public static Student FindStudent(string studentID)
        {
            Student s = null;
            string ss = "select * from Student where SId = @sid";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.publicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            OleDbDataReader j = cmd.ExecuteReader();
            while (j.Read()) {
                s = new Student(
                    j.GetString(0), j.GetString(1), j.GetString(2), 
                    j.GetString(3), j.GetString(4), j.GetString(5), j.GetString(6)
                    ,j.GetInt32(7), j.GetString(8), j.GetString(9), j.GetString(10)
                    );
            }

            return s;
        }

        public static List<Student> GetAllStudent()
        {
            List<Student> sl = new List<Student>();
            string ss = "select * from Student";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.publicConnection);           
            OleDbDataReader j = cmd.ExecuteReader();
            while (j.Read())
            {
                Student s = new Student(
                    j.GetString(0), j.GetString(1), j.GetString(2),
                    j.GetString(3), j.GetString(4), j.GetString(5), j.GetString(6)
                    , j.GetInt32(7), j.GetString(8), j.GetString(9), j.GetString(10)
                    );
                sl.Add(s);
            }

            return sl;
        }

        public static int GetNoOfStudents()
        {
            string ss = "select count(*) from Student";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.publicConnection);
            int i = (int)cmd.ExecuteScalar();
            return i;
        }
    }
}
