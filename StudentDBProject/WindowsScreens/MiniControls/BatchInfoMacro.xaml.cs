using StudentDBProject.Models;
using StudentDBProject.WindowsScreens.MicroControls;
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

namespace StudentDBProject.WindowsScreens.MiniControls
{
  
    public partial class BatchInfoMacro : UserControl
    {
        readonly List<string> k = new List<string>();
        StudentSeatingVisual ssv;

        public BatchInfoMacro()
        {
            InitializeComponent();
           // updateSeatRecord();
            SetTheme();
        }
        public void updateSeatRecord()
        {
            string selectedBatch = comBatch.Text;
            k.Clear();
            Student.GetAllStudent(selectedBatch).ForEach((t) => k.Add(t.studentId));
            ssv = new StudentSeatingVisual(30, k);
            ssv.OnSeatItemClick += Ssv_OnSeatItemClick;
            seatVisual.Content = ssv;
        }
        private void SetTheme()
        {
            card.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
        }
        private void Ssv_OnSeatItemClick(object sender, MouseButtonEventArgs e)
        {
            SetInfo();
        }
        private void SetInfo()
        {
            ccPreBodyIfno.Content = new StudentEntryScreen(StudentSeatingVisual.currntValueOnGrid)
            {
                Foreground = new SolidColorBrush(ThemeColor.currentFontColor),
            };
        }
        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            updateSeatRecord();
        }
        private void ComBatch_DropDownClosed(object sender, EventArgs e)
        {
            updateSeatRecord();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ccPreBodyIfno.Content = null;
        }
    }
}
