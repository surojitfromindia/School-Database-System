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
    /// Interaction logic for ManyStudentInfo.xaml
    /// </summary>
    public partial class ManyStudentInfo : UserControl
    {
        public ManyStudentInfo(string t, STUDENTFILLTER F)
        {
            InitializeComponent();
            SearchWindow SS = new SearchWindow(t, F);
            resultPan.Children.Clear();
            resultPan.Children.Add(SS);

        }
    }
}
