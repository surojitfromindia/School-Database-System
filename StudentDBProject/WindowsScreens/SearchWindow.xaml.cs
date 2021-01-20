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
        //Properties and private variables
        private int preIndex = 1;
        private string fillterText = "";
        private STUDENTFILLTER fl = STUDENTFILLTER.NAME;
        private List<UserControl> windows = new List<UserControl>(4);
        private int windowsIndex = 0;


        //Constructors
        public SearchWindow()
        {
            InitializeComponent();
            SetTheme(); // Set theme for this control.
            if (ConnectionClass.UserType != "Admin")
                btnDeleteRecord.Visibility = Visibility.Collapsed; //hide delete record button if user is not admin.

            LoadList(); // Load itemBox with student info.
        }
        public SearchWindow(string fillterText, STUDENTFILLTER fl)
        {
            InitializeComponent();
            SetTheme(); // Set theme for this control.
            if (ConnectionClass.UserType != "Admin")
                btnDeleteRecord.Visibility = Visibility.Collapsed; //hide delete record button if user is not admin.


            this.fl = fl;
            this.fillterText = fillterText;
            ApplyFillterLoadList(fl); // Load itemBox with student info (Fillterd).
        }


        //EventHandelars
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
                    new AlertDialogCus("Information.",
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
        private void btnUpdateRcord_Click(object sender, RoutedEventArgs e)
        {
            if (fillterText != "")
                ApplyFillterLoadList(fl);
            else
                LoadList();
        }
        private void BtnPre_Click(object sender, RoutedEventArgs e)
        {
            PrevPanelOfThisRecord();
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NextPanelOfThisRecord();
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
        private void ApplyFillterLoadList(STUDENTFILLTER fill)
        {
            //Update Count
            lblNoOfStudents.Content = Student.applyFillter(fillterText, fill).Count.ToString() + " Students Found";
            //Add items to the list
            listStudent.Items.Clear();
            List<Student> los = Student.applyFillter(fillterText, fill);
            foreach (Student st in los)
            {
                StudentSmallDetails smD = new StudentSmallDetails(st.studentId, st.studentName,
                    st.section, st.ClassName,
                    st.RollNumber.ToString(), st.phone);
                listStudent.Items.Add(smD);
            }
        }
        private void PrevPanelOfThisRecord()
        {
            if (windows.Count > 0)
            {
                if (windowsIndex <= 0)
                    windowsIndex = windows.Count - 1;
                else
                    windowsIndex--;
                resultPan.Content = windows[windowsIndex];
            }
        }
        private void NextPanelOfThisRecord()
        {
            if (windows.Count > 0)
            {
                if (windowsIndex >= windows.Count - 1)
                    windowsIndex = 0;
                else
                    windowsIndex++;
                resultPan.Content = windows[windowsIndex];
            }
        }
        void SearchAndShow(string s)
        {

            //if user want to stay on same tab.
            //windowsIndex = 0; 

            //Reset Indices
            windows.Clear();

            txtIDNumber.Text = s;
            // Show Four Search Output Screen
            StudentEntryScreen StudentDetails = new StudentEntryScreen(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 500,
                Margin = new Thickness(0, 0, 10, 0),
            };

            BookPerIdScreen bookInfo = new BookPerIdScreen(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 10, 0),
            };

            InstallmentPerId installmentInfo = new InstallmentPerId(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 10, 0),
            };

            MiniBusInfo busInfo = new MiniBusInfo(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 10, 0),
            };

            windows.Add(StudentDetails);
            windows.Add(bookInfo);
            windows.Add(installmentInfo);
            windows.Add(busInfo);
            resultPan.Content = windows[windowsIndex];
        }
        private void SetTheme()
        {
            Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            header.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            lblNoOfStudents.Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            lblNoOfStudents.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            btnDeleteRecord.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            btnUpdateRcord.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            btnSearch.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);

            btnPre.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            btnNext.Background = new SolidColorBrush(ThemeColor.currentAcColor);


        }
    }
}
