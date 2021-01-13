using StudentDBProject.Models;
using StudentDBProject.WindowsScreens.MiniControls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentDBProject.WindowsScreens
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : UserControl
    {
        private int preIndex = 1;

        public SearchWindow()
        {
            InitializeComponent();
            SetTheme(); // Set theme for this control.
            if (ConnectionClass.Utype != "Admin")
                btnDeleteRecord.Visibility = Visibility.Collapsed; //hide delete record button if user is not admin.
            LoadList(); // Load itemBox with student info.
        }

        private void SetTheme()
        {
            Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            header.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            lblNoOfStudents.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            btnDeleteRecord.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            btnUpdateRcord.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            btnSearch.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchAndShow(txtIDNumber.Text);
        }

        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            AlertDialogCus ExitDialog = new AlertDialogCus("Delete This Record.",
                "Do you want to delete this record, this will permanently remove this record from database ?");
            ExitDialog.ShowDialog();
            bool isCancel = ExitDialog.IsCanceldPress;
            if (!isCancel)
            {
                bool isdeleted = Student.deleteRecord(txtIDNumber.Text);
                if (isdeleted)
                {
                    new AlertDialogCus ("Information.",
                        "Record successfully removed from database!", cancleButtonText: "Ok", isActionButtonHiden: true)
                        .ShowDialog();
                    listStudent.Items.RemoveAt(preIndex + 1);
                    listStudent.SelectedIndex = (preIndex == -1) ? 0 : preIndex;
                }
            }
        }

        private void listStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((StudentSmallDetails)listStudent.SelectedItem != null) //must let the list load.
            {
                string idOfSelectedItem = ((StudentSmallDetails)listStudent.SelectedItem).txtSID.Text;
                SearchAndShow(idOfSelectedItem);
                preIndex = listStudent.SelectedIndex - 1;
            }
        }

        void SearchAndShow(string s)
        {
            txtIDNumber.Text = s;
            // Show Four Search Output Screen
            StudentEntryScreen StudentDetails = new StudentEntryScreen(s);
            BookPerIdScreen bp = new BookPerIdScreen(s);
            InstallmentPerId ip = new InstallmentPerId(s);
            MiniBusInfo busInfo = new MiniBusInfo(s);
            resultPan.Children.Clear();
            resultPan.Children.Add(StudentDetails);
            resultPan.Children.Add(bp);
            resultPan.Children.Add(ip);
            resultPan.Children.Add(busInfo);
        }

        void LoadList()
        {
            //Update Count
            lblNoOfStudents.Content = Student.GetNoOfStudents() + " Students Found";
            //Add items to the list
            listStudent.Items.Clear();
            List<Student> los = Student.GetAllStudent();
            foreach (Student st in los)
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
