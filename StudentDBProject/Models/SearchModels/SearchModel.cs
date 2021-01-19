using StudentDBProject.WindowsScreens.SeachControls.StudentInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace StudentDBProject.Models.SearchModels
{
    class SearchModel
    {
        /*
         * prefix format for students
         * s:i: search by student id. (single return)
         * s:n: search by student name. (multiple returns)
         * s:c: search by class (multiple returns)
         * s:r: search by roll (multiple returns)
         * s:co: search by contact (multiple returns)
         * 
         * prefix format for books
         * b:n:  search book
         * b:a:  search author (multple returns)
         * b:ac: show currently issued books
         * b:re: show returned books
         * 
         * prefix for windows
         * w: <any window name>

         */



        private static (string, string, string) getCommandFromText(string helpText)
        {
            int NumberOfSeparators = helpText.Where((t) => t.Equals(':')).Count();
            string CommandHead = "";
            string CommandTail = "";
            string CommandParameter = "";
            bool first = Regex.IsMatch(helpText, "^([a-z A-Z]+:){2}[a-z A-Z 0-9]{1,}$");
            bool second = Regex.IsMatch(helpText, "^[a-z A-Z]+:[a-z A-Z 0-9]{1,}$");
            if (first || second)
            {
                if (NumberOfSeparators == 2)
                {
                    string[] b = helpText.Split(':').ToArray();
                    //first element is command head
                    CommandHead = b[0];
                    //last element is command tail
                    CommandTail = b[1];
                    //Command Parameter
                    CommandParameter = b[2];
                }
                else
                {
                    string[] b = helpText.Split(':').ToArray();
                    //first element is command head
                    CommandHead = b[0];
                    CommandParameter = b[1];
                }
            }
            return (CommandHead, CommandTail, CommandParameter);
        }


        public static object ExcuteCommand(string helpText)
        {
            var (f, f2, f3) = getCommandFromText(helpText);
            string fM = f.ToUpper(); string f2M = f2.ToUpper();
            switch (fM)
            {
                case "S":
                    {
                        switch (f2M)
                        {
                            case "I":
                                {
                                    //f3 is id
                                    SingleStudentInfo singleInfoByRoll = new SingleStudentInfo(f3);
                                    return singleInfoByRoll;
                                }
                            case "R":
                                {
                                    ManyStudentInfo manyStudentInfo = new ManyStudentInfo(f3, STUDENTFILLTER.ROLL);
                                    return manyStudentInfo;
                                }
                            case "C":
                                {
                                    ManyStudentInfo manyStudentInfo = new ManyStudentInfo(f3, STUDENTFILLTER.CLASS);
                                    return manyStudentInfo;
                                }
                            case "N":
                                {
                                    ManyStudentInfo manyStudentInfo = new ManyStudentInfo(f3, STUDENTFILLTER.NAME);
                                    return manyStudentInfo;
                                }
                        }
                        break;
                    }
            }
            return new TextBlock() { Text = "Nothing Found, Try Again" };
        }


    }




}
