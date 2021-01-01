using StudentDBProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentDBProject.WindowsScreens
{
    /// <summary>
    /// Interaction logic for StudentEntryScreen.xaml
    /// </summary>
    public partial class StudentEntryScreen : UserControl
    {
        string id;
        string busid;
        string libid;
        readonly ProgressIndicator Pi;

        List<DataEntryErrorInfoClass> dts = new List<DataEntryErrorInfoClass>();
        public StudentEntryScreen()
        {
            InitializeComponent();
            FillWith();
            Pi = new ProgressIndicator(dts) { 
                Height = 350
            };
            ErrorPanel.Children.Add(Pi);
            txtSname.Focus();
            txtRoll.TextChanged += onTextChange;
            txtClass.TextChanged += onTextChange;
            txtSec.TextChanged += onTextChange;
            txtSname.TextChanged += onTextChange;
            txtFName.TextChanged += onTextChange;
            txtCon.TextChanged += onTextChange;
            txtAdd.TextChanged += onTextChange;
        }

        public StudentEntryScreen(string searchID)
        {
            Student privateStudent = Student.FindStudent(searchID);
            if (privateStudent != null)
            {
                InitializeComponent();
                txtFName.Text = privateStudent.FatherName;
                txtId.Text = privateStudent.studentId;
                txtSname.Text = privateStudent.studentName;
                txtAdd.Text = privateStudent.address;
                txtCon.Text = privateStudent.phone;
                txtVoter.Text = privateStudent.VID;
                txtClass.Text = privateStudent.ClassName;
                txtRoll.Text = privateStudent.RollNumber.ToString();
                txtSec.Text = privateStudent.section;
                txtBus.Text = privateStudent.BusCard;
                txtLB.Text = privateStudent.LibraCard;

                btnTra.Visibility = Visibility.Collapsed;
                btnNewForm.Visibility = Visibility.Collapsed;
                lblStatus.Visibility = Visibility.Hidden;
            }
        }



        private void onTextChange(object sender, TextChangedEventArgs e)
        {
            GenerateIDBUSLIB();
            Pi.UpdateChecking();
        }

        private void btnTra_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student(id, txtSname.Text, txtFName.Text, txtAdd.Text, txtCon.Text, txtVoter.Text, txtClass.Text, int.Parse(txtRoll.Text), txtSec.Text, libid, busid);
            lblStatus.Text = newStudent.Create().ToString();
        }

        void GenerateIDBUSLIB()
        {
            id = txtClass.Text + txtRoll.Text + txtSec.Text;
            busid = id + "B";
            libid = id + "L";
            txtId.Text = id; txtBus.Text = busid; txtLB.Text = libid;
            txtRoll.Text = Student.nextRoll(txtClass.Text).ToString();
        }

        private void btnNewForm_Click(object sender, RoutedEventArgs e)
        {
            NewFrom();
        }

        void NewFrom()
        {
            txtSname.Clear(); txtId.Clear(); txtFName.Clear();
            txtVoter.Clear(); txtRoll.Clear(); txtSec.Clear();
            txtAdd.Clear(); txtLB.Clear(); txtBus.Clear();
            txtSec.Clear(); txtClass.Clear(); txtCon.Clear();
            txtRoll.Text = "";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student(txtId.Text, txtSname.Text, txtFName.Text, txtAdd.Text, txtCon.Text, txtVoter.Text, txtClass.Text, int.Parse(txtRoll.Text), txtSec.Text, txtLB.Text, txtBus.Text);
            MessageBox.Show(Student.update(newStudent) ? "Record Update" : "Unsuccessful");
        }


        //Validity Checking
        bool NameCheck()
        {
            bool isAllChar = true;
            if (txtSname.Text.Length > 0)
                foreach (char c in txtSname.Text)
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
            return txtSname.Text.Contains(" ") && isAllChar;
        }
        bool FatherNameCheck()
        {
            bool isAllChar = true;
            if (txtFName.Text.Length > 0)
                foreach (char c in txtFName.Text)
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
            return txtFName.Text.Contains(" ") && isAllChar;
        }
        bool AddressCheck()
        {
            return txtAdd.Text.Contains(" ");
        }
        bool ContactCheck()
        {
            bool isAllChar = true;
            if (txtCon.Text.Length > 0)
                foreach (char c in txtCon.Text)
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
            return txtCon.Text.Length == 10 && isAllChar;
        }



        void FillWith()
        {
            DataEntryErrorInfoClass NameError = new DataEntryErrorInfoClass("Student name must not contain Space or any speacial chracter, use all capital letters", NameCheck);
            DataEntryErrorInfoClass FatherNameError = new DataEntryErrorInfoClass("Father name must no have space or any Speacial, use all capital lettes", FatherNameCheck);
            DataEntryErrorInfoClass AddressError = new DataEntryErrorInfoClass("Address should be valid", AddressCheck);
            DataEntryErrorInfoClass ContactNumberError = new DataEntryErrorInfoClass("Contatct must be 10 digits, no chracter is allowed", ContactCheck);
            dts.Add(NameError);
            dts.Add(FatherNameError);
            dts.Add(AddressError);
            dts.Add(ContactNumberError);
        }

    }
}
