using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public Student(
            string studentId,
            string studentName,
            string FatherName,
            string address,
            string phone,
            string VID,
            string ClassName,
            int RollNumber,
            string section,
            string LibraCard,
            string BusCard
            )
        {
            this.studentId = studentId;
            this.studentName = studentName;
            this.FatherName = FatherName;
            this.address = address;
            this.phone = phone;
            this.VID = VID;
            this.ClassName = ClassName;
            this.RollNumber = RollNumber;
            this.section = section;
            this.LibraCard = LibraCard;
            this.BusCard = BusCard;
        }

        public void Deconstruct(
            out string studentId,
            out string studentName,
            out string FatherName,
            out string address,
            out string phone,
            out string VID,
            out string ClassName,
            out int RollNumber,
            out string section,
            out string LibraCard,
            out string BusCard

            )
        {
            studentId = this.studentId;
            studentName = this.studentName;
            FatherName = this.FatherName;
            address = this.address;
            phone = this.phone;
            VID = this.VID;
            ClassName = this.ClassName;
            RollNumber = this.RollNumber;
            section = this.section;
            LibraCard = this.LibraCard;
            BusCard = this.BusCard;
        }


        public bool Create()
        {
            string ss = "insert into Student values(@uid, @uname, @ufaname, @uadd," +
                " @uph, @uvid, @uclass, @uRoll, @usec, @ulib, @ubus)";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
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
            int i = cmd.ExecuteNonQuery();
            return i == 1 ? true : false;
        }

        public static int nextRoll(string className)
        {

            int p = 1;
            string ss = "select max(RNo) from Student where Cla = @clas";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@clas", className);
            object j = cmd.ExecuteScalar();
            if (j != DBNull.Value)
            {
                p = (int)j + 1;
            }
            return p;
        }

        public static bool deleteRecord(string studentID)
        {
            string commandString = "delete from Student where SId = @uid";
            OleDbCommand cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@uid", studentID);
            int i = cmd.ExecuteNonQuery();

            commandString = "delete from Bus where Sid = @sid;";
            cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            cmd.ExecuteNonQuery();

            commandString = "delete from installment where Sid = @sid;";
            cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            cmd.ExecuteNonQuery();

            commandString = "delete from Library where Sid = @sid;";
            cmd = new OleDbCommand(commandString, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            cmd.ExecuteNonQuery();

            return i == 1 ? true : false;

        }

        public static bool update(Student newInfo)
        {
            OleDbCommand cmd = new OleDbCommand("delete from Student where SId = @uid", ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@uid", newInfo.studentId);
            int i = cmd.ExecuteNonQuery();
            bool i2 = newInfo.Create();
            return (i == 1 && i2) ? true : false;
        }

        public static Student FindStudent(string studentID)
        {
            Student s = null;
            string ss = "select * from Student where SId = @sid";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
            cmd.Parameters.AddWithValue("@sid", studentID);
            OleDbDataReader j = cmd.ExecuteReader();
            while (j.Read())
            {
                s = new Student(
                    j.GetString(0), j.GetString(1), j.GetString(2),
                    j.GetString(3), j.GetString(4), j.GetString(5), j.GetString(6)
                    , j.GetInt32(7), j.GetString(8), j.GetString(9), j.GetString(10)
                    );
            }

            return s;
        }

        public static List<Student> GetAllStudent()
        {
            List<Student> sl = new List<Student>();
            string ss = "select * from Student order by RNo ";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
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
        public static List<Student> GetAllStudent(string batch)
        {
            List<Student> sl = new List<Student>();
            string ss = $"select * from Student where Cla='{batch}' order by RNo ";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
            OleDbDataReader j = cmd.ExecuteReader();
            while (j.Read())
            {
                Student s = new Student(
                    j.GetString(0), j.GetString(1), j.GetString(2),
                    j.GetString(3), j.GetString(4), j.GetString(5), j.GetString(6),
                    j.GetInt32(7), j.GetString(8), j.GetString(9), j.GetString(10)
                    );
                sl.Add(s);
            }
            return sl;
        }
        public static int GetNoOfStudents()
        {
            string ss = "select count(*) from Student";
            OleDbCommand cmd = new OleDbCommand(ss, ConnectionClass.PublicConnection);
            int i = (int)cmd.ExecuteScalar();
            return i;
        }

        public static List<Student> applyFillter(string text, STUDENTFILLTER st)
        {
            var studentList = new List<Student>();
            switch (st)
            {
                case STUDENTFILLTER.CLASS:
                    {
                        studentList = GetAllStudent().Where(t => (t.ClassName == text)).ToList();
                        break;
                    }
                case STUDENTFILLTER.NAME:
                    {
                        studentList = GetAllStudent().Where(t => (t.studentName == text)).ToList();
                        break;
                    }
                case STUDENTFILLTER.ROLL:
                    {
                        studentList = GetAllStudent().Where(t => (t.RollNumber.ToString() == text)).ToList();
                        break;
                    }
                case STUDENTFILLTER.NONE:
                    {
                        studentList = GetAllStudent();
                        break;
                    }
            }

            return studentList;
        }

    }
    public enum STUDENTFILLTER
    {
        ROLL, NAME, CLASS, NONE
    }


    public static class StudentInfoVerification
    {
        public static bool IsStudentNameValid(string studentName)
        {
            studentName = studentName.Trim();
            bool isAllChar = true;
            if (studentName.Length > 0)
                foreach (char c in studentName)
                {
                    int p = c;
                    Console.Write(c);
                    if ((c >= 65 && c <= 90) || c == 32)
                        isAllChar = true;
                    else
                    {
                        isAllChar = false;
                        break;
                    }
                }
            return studentName.Contains(" ") && isAllChar;
        }
        public static bool IsGurdianNameValid(string gurdianName)
        {
            gurdianName = gurdianName.Trim();
            bool isAllChar = true;
            if (gurdianName.Length > 0)
                foreach (char c in gurdianName)
                {
                    int p = c;
                    Console.Write(c);
                    if ((c >= 65 && c <= 90) || c == 32)
                        isAllChar = true;
                    else
                    {
                        isAllChar = false;
                        break;
                    }
                }
            return gurdianName.Contains(" ") && isAllChar;
        }
        public static bool IsAddressrValid(string address)
        {
            address = address.Trim();
            return address.Contains(" ");
        }
        public static bool IsContactNumberValid(string contactNumber)
        {
            contactNumber = contactNumber.Trim();
            bool isAllChar = true;
            if (contactNumber.Length > 0)
                foreach (char c in contactNumber)
                {
                    int p = c;
                    Console.Write(c);
                    if ((c >= 48 && c <= 57))
                        isAllChar = true;
                    else
                    {
                        isAllChar = false;
                        break;
                    }
                }
            return contactNumber.Length == 10 && isAllChar;
        }
        public static bool IsAadharValid(string Aadhar)
        {
            return Regex.IsMatch(Aadhar, "^([0-9]{4}-){3}[0-9]{4}$");
        }
        public static bool IsClassIDValid(string classID)
        {
            classID = classID.Trim();
            return classID.Length == 7;
        }
    }
}
