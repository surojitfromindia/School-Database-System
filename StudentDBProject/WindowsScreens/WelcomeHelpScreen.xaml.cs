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
    /// Interaction logic for WelcomeHelpScreen.xaml
    /// </summary>
    public partial class WelcomeHelpScreen : UserControl
    {
        Dictionary<string, string> helpDescription = new Dictionary<string, string>(20);

        public WelcomeHelpScreen()
        {
            InitializeComponent();
            DescriptionAbout();
        }



        void DescriptionAbout()
        {
            helpDescription.Add("Student Entry", "Insert student info\ninto database");
            helpDescription.Add("Bus Entry", "Insert Bus info into database");
            foreach(string optionText in helpDescription.Keys)
            {
                Label lb = new Label();
                lb.Content = optionText;
                lb.Padding = new Thickness(1);
                lb.Foreground = new SolidColorBrush(Color.FromRgb(18, 94, 170));
                listHelpOpt.Items.Add(lb);
            }
        }

        void Search()
        {
            txtDes.Text = helpDescription[((Label)listHelpOpt.SelectedItem).Content.ToString()];
        }

        private void listHelpOpt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }
    }
}
