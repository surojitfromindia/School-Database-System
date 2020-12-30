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
        public StudentEntryScreen()
        {
            InitializeComponent();
            txtSname.Focus();
            txtRoll.TextChanged += onTextChange;
            txtClass.TextChanged += onTextChange;
            txtSec.TextChanged += onTextChange;
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
        }

        private void btnTra_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student( id, txtSname.Text, txtFName.Text, txtAdd.Text, txtCon.Text, txtVoter.Text, txtClass.Text, int.Parse(txtRoll.Text), txtSec.Text, libid, busid);
           lblStatus.Text = newStudent.Create().ToString();
        }

        void GenerateIDBUSLIB() {
            id = txtClass.Text + txtRoll.Text+ txtSec.Text;
            busid = id + "B";
            libid = id + "L";
            txtId.Text = id; txtBus.Text = busid; txtLB.Text = libid;
            txtRoll.Text = Student.nextRoll(txtClass.Text).ToString();
        }

        private void btnNewForm_Click(object sender, RoutedEventArgs e)
        {
            NewFrom();
        }

        void NewFrom() {
            txtSname.Clear(); txtId.Clear(); txtFName.Clear();
            txtVoter.Clear(); txtRoll.Clear(); txtSec.Clear();
            txtAdd.Clear(); txtLB.Clear(); txtBus.Clear();
            txtSec.Clear(); txtClass.Clear();txtCon.Clear();
            txtRoll.Text = "";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student(txtId.Text, txtSname.Text, txtFName.Text, txtAdd.Text, txtCon.Text, txtVoter.Text, txtClass.Text, int.Parse(txtRoll.Text), txtSec.Text, txtLB.Text, txtBus.Text);
            MessageBox.Show(Student.update(newStudent) ? "Record Update" : "Unsuccessful");

        }
    }
}
