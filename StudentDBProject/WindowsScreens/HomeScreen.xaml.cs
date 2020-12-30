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
using System.Windows.Shapes;

namespace StudentDBProject.WindowsScreens
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        public event EventHandler onThisClose;
        private object curTab;
        public HomeScreen()
        {
            InitializeComponent();
           
        }

        public HomeScreen(string usr)
        {
            InitializeComponent();
            txtUserName.Text = "Hola "+ConnectionClass.Utype+", "+usr+" !";
            Closed += HomeScreen_Closed;
            ArmTabs();
        }

        private void HomeScreen_Closed(object sender, EventArgs e)
        {
            onThisClose?.Invoke(this, e);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // DragMove();
        }

        void ArmTabs() {
            foreach (UIElement t in sPan.Children){
                TextBlock tt = (TextBlock)t;
                t.MouseDown += T_MouseDown;
                t.MouseEnter += T_MouseEnter;
                t.MouseLeave += T_MouseLeave;
            }
        }

        private void T_MouseLeave(object sender, MouseEventArgs e)
        {
            cleanSelection();
            remainSelected();
        }

        private void T_MouseEnter(object sender, MouseEventArgs e)
        {
            
            foreach (UIElement t in sPan.Children)
            {
                TextBlock tt = (TextBlock)t;
                tt.Background = new SolidColorBrush(Colors.Transparent);
                tt.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF125EAA"));
            }
            ((TextBlock)sender).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF125EAA"));
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.White);
            remainSelected();
        }

        private void T_MouseDown(object sender, MouseButtonEventArgs e)
        {

            cleanSelection();
            string name = ((TextBlock)sender).Name;
            curTab = sender;
            remainSelected();
            switch (name) {
                case "t1": ccPre.Content = new StudentEntryScreen(); break;
                case "t2": ccPre.Content = new LibraryEntryScreen(); break;
                case "t3": ccPre.Content = new InstallmentEntryScreen(); break;
                case "t4": ccPre.Content = new BusEntryScreen(); break;
                case "t5": ccPre.Content = new SearchWindow(); break;
                case "t7": ccPre.Content = new WelcomeHelpScreen(); break;

            }
        }

        private void cleanSelection() {
            foreach (UIElement t in sPan.Children)
            {

                TextBlock tt = (TextBlock)t;
                tt.Background = new SolidColorBrush(Colors.Transparent);
                tt.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF125EAA"));
            }
        }
        private void remainSelected() {
            if (curTab != null)
            {
                ((TextBlock)curTab).Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF125EAA"));
                ((TextBlock)curTab).Foreground = new SolidColorBrush(Colors.White);
            }
        }

        

    }
}
