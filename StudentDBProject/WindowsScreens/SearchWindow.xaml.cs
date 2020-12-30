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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : UserControl
    {

        StudentEntryScreen StudentDetails;
        int preIndex =  1;
        public SearchWindow()
        {
            InitializeComponent();
            if (ConnectionClass.Utype != "Admin") {
                btnDeleteRecord.Visibility = Visibility.Hidden;

            }//hide delete record button if user is not admin.
            LoadList(); // Load itemBox with student info.
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchAndShow(txtIDNumber.Text);
        }

        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDetails != null)
            {
                bool isdeleted = Student.deleteRecord(txtIDNumber.Text);
                if (isdeleted)
                    MessageBox.Show("Record Deleted");

                listStudent.Items.RemoveAt(preIndex + 1);
                listStudent.SelectedIndex =  (preIndex == -1) ? 0 : preIndex;
            }
        }

        private void listStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((StudentSmallDetails)listStudent.SelectedItem != null) //must let the list load.
            {
                string p = ((StudentSmallDetails)listStudent.SelectedItem).txtSID.Text;
                txtIDNumber.Text = p;
                SearchAndShow(p);
                preIndex = listStudent.SelectedIndex - 1;
            }           
        }

        void SearchAndShow(string s)
        {
            StudentDetails = new StudentEntryScreen(s);
            BookPerIdScreen bp = new BookPerIdScreen(txtIDNumber.Text);
            InstallmentPerId ip = new InstallmentPerId(txtIDNumber.Text);
            MiniBusInfo busInfo = new MiniBusInfo(txtIDNumber.Text);
            resultPan.Children.Clear();
            resultPan.Children.Add(StudentDetails);
            resultPan.Children.Add(bp);
            resultPan.Children.Add(ip);
            resultPan.Children.Add(busInfo);

        }
        void LoadList()
        {
            listStudent.Items.Clear();
            List<Student> los = Student.GetAllStudent();
            foreach(Student st in los)
            {
                StudentSmallDetails smD = new StudentSmallDetails(st.studentId, st.studentName,
                    st.section, st.ClassName,
                    st.RollNumber.ToString(), st.phone);
                listStudent.Items.Add(smD);
            }
        }

        private void btnUpdateRcord_Click(object sender, RoutedEventArgs e)
        {
            LoadList();
        }
    }
}
