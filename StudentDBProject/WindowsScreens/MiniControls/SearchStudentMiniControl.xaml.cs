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

namespace StudentDBProject.WindowsScreens.MiniControls
{
    /// <summary>
    /// Interaction logic for SearchStudentMiniControl.xaml
    /// </summary>
    public partial class SearchStudentMiniControl : UserControl
    {
        public SearchStudentMiniControl()
        {
            InitializeComponent();
            btnSearch.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            btnSearch.Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clicked");
        }
    }
}
