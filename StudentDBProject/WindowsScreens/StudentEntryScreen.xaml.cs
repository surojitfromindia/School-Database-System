using ErrorInfoWindow;
using StudentDBProject.Models;
using StudentDBProject.WindowsScreens.MiniControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace StudentDBProject.WindowsScreens
{
    public partial class StudentEntryScreen : UserControl
    {
        private string id;
        private string busid;
        private string libid;
        readonly List<DataEntryErrorInfoClass> dts = new List<DataEntryErrorInfoClass>();

        //Default Constructor for this class
        public StudentEntryScreen()
        {
            InitializeComponent();
            mmMini.Content = new BatchInfoMacro()
            {
                Padding = new Thickness(10)
            };
            //Set Theme
            SetTheme();
            //Set ProgressIndicator's properties
            PopulateErrorActionList();
            PI.ErrorDetailsAndAction = dts;
            PI.SetAccColor(ThemeColor.currentAcColor);
            txtSname.Focus();
            txtRoll.TextChanged += onTextChange;
            txtClass.TextChanged += onTextChange;
            txtSec.TextChanged += onTextChange;
            txtSname.TextChanged += onTextChange;
            txtFName.TextChanged += onTextChange;
            txtCon.TextChanged += onTextChange;
            txtAdd.TextChanged += onTextChange;
            txtAdhar.TextChanged += TxtAdhar_TextChanged;
        }

        private void SetTheme()
        {
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            btnNewForm.Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            btnTra.Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            btnUpdate.Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            btnNewForm.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            btnTra.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            btnUpdate.Background = new SolidColorBrush(ThemeColor.currentAcColor);
        }

        //This Constructor used in SearchWindow.cs
        //to show searched data only if matchs are found in database.
        public StudentEntryScreen(string searchID)
        {
            Student privateStudent = Student.FindStudent(searchID);
            if (privateStudent != null)
            {
                InitializeComponent();
                SetTheme();
                int? roll;
                (txtId.Text, txtSname.Text, txtFName.Text, txtAdd.Text,
                    txtCon.Text, txtAdhar.Text, txtClass.Text, roll,
                    txtSec.Text, txtLB.Text, txtBus.Text) = privateStudent;
                txtRoll.Text = roll.ToString();

                //New Form Entry , Transaction, Error Information Controls are disabled
                // so, user can't Entry a new record 
                //but able to update recordrs already exsists.
                btnTra.Visibility = Visibility.Collapsed;
                btnNewForm.Visibility = Visibility.Collapsed;
                lblStatus.Visibility = Visibility.Hidden;
                PI.Visibility = Visibility.Collapsed;
            }
        }

        private void TxtAdhar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtAdhar.Text.EndsWith("-"))
            {
                string te = txtAdhar.Text;
                string nonUnder = te;
                if (te.Contains("-"))
                    nonUnder = te.Replace("-", "");
                if (nonUnder.Length % 4 == 0 && te.Length < 19)
                {
                    txtAdhar.Text = te + "-";
                    txtAdhar.CaretIndex = te.Length + 1;
                }
            }
            PI.UpdateChecking();
        }


        private void onTextChange(object sender, TextChangedEventArgs e)
        {
            GenerateIDBUSLIB();
            PI.UpdateChecking();
        }

        private void btnTra_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student(id, txtSname.Text, txtFName.Text, txtAdd.Text, txtCon.Text, txtAdhar.Text, txtClass.Text, int.Parse(txtRoll.Text), txtSec.Text, libid, busid);
            lblStatus.Text = newStudent.Create().ToString();
        }

        void GenerateIDBUSLIB()
        {

            string _roll = txtRoll.Text;
            if (txtRoll.Text.Length != 0 && int.Parse(txtRoll.Text) < 10)
                _roll = "0" + int.Parse(txtRoll.Text);
            id = txtClass.Text + _roll + txtSec.Text;
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
            txtAdhar.Clear(); txtRoll.Clear(); txtSec.Clear();
            txtAdd.Clear(); txtLB.Clear(); txtBus.Clear();
            txtSec.Clear(); txtClass.Clear(); txtCon.Clear();
            txtRoll.Text = "";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student(
                txtId.Text, txtSname.Text,
                txtFName.Text, txtAdd.Text,
                txtCon.Text, txtAdhar.Text,
                txtClass.Text, int.Parse(txtRoll.Text),
                txtSec.Text, txtLB.Text,
                txtBus.Text);
            MessageBox.Show(Student.update(newStudent) ? "Record Update" : "Unsuccessful");
        }


        //Validity Checking
        bool NameCheck() => StudentInfoVerification.IsStudentNameValid(txtSname.Text);

        bool FatherNameCheck() => StudentInfoVerification.IsGurdianNameValid(txtFName.Text);

        bool AddressCheck() => StudentInfoVerification.IsAddressrValid(txtAdd.Text);

        bool ContactCheck() => StudentInfoVerification.IsContactNumberValid(txtCon.Text);

        bool ClassCheck() => StudentInfoVerification.IsClassIDValid(txtClass.Text);

        bool AadharCheck() => StudentInfoVerification.IsAadharValid(txtAdhar.Text);

        void PopulateErrorActionList()
        {
            DataEntryErrorInfoClass NameError =
                new DataEntryErrorInfoClass("Student name must have atleast one letter preceded by a Space\nUse all capital letters", NameCheck);
            DataEntryErrorInfoClass FatherNameError =
                new DataEntryErrorInfoClass("Guardian name name must have at least one letter preceded by Space\nUse all capital letters", FatherNameCheck);
            DataEntryErrorInfoClass AddressError =
                new DataEntryErrorInfoClass("Address should be valid", AddressCheck);
            DataEntryErrorInfoClass ContactNumberError =
                new DataEntryErrorInfoClass("Contatct must be 10 digits.\nChracters are't allowed", ContactCheck);
            DataEntryErrorInfoClass AadharNumberError =
               new DataEntryErrorInfoClass("Enter Valid Aadhar number", AadharCheck);
            DataEntryErrorInfoClass ClassIDError =
                new DataEntryErrorInfoClass("Class format must macth Pre-defined structure\n(CODE[A-Z])(BATCH[01-99])(YEAR)", ClassCheck);
            dts.Add(NameError);
            dts.Add(FatherNameError);
            dts.Add(AddressError);
            dts.Add(ContactNumberError);
            dts.Add(AadharNumberError);
            dts.Add(ClassIDError);
        }

    }
}
