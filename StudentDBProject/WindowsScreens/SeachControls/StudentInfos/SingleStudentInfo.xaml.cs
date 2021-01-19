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

namespace StudentDBProject.WindowsScreens.SeachControls.StudentInfos
{
    /// <summary>
    /// Interaction logic for SingleStudentInfo.xaml
    /// </summary>
    public partial class SingleStudentInfo : UserControl
    {
        public SingleStudentInfo(string s)
        {
            InitializeComponent();
            // Show Four Search Output Screen
            StudentEntryScreen StudentDetails = new StudentEntryScreen(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 5),
            };
            BookPerIdScreen bp = new BookPerIdScreen(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 5),
            };
            InstallmentPerId ip = new InstallmentPerId(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 5),
            };
            MiniBusInfo busInfo = new MiniBusInfo(s)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 5),
            };

            //CHECK THIS
            if (StudentDetails != null)
            {
                resultPan.Children.Clear();
                resultPan.Children.Add(StudentDetails);
                resultPan.Children.Add(bp);
                resultPan.Children.Add(ip);
                resultPan.Children.Add(busInfo);
            }
        }
    }
}
