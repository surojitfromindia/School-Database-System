using StudentDBProject.Models;
using StudentDBProject.WindowsScreens.MiniControls;
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
        private Color onHoverAndSelectedColor = ThemeColor.currentAcColor;

        public HomeScreen()
        {
            InitializeComponent();
            Foreground =new SolidColorBrush(ThemeColor.currentAcColor);
            Background = new SolidColorBrush(ThemeColor.currentBackgroundColor);
        }

        public HomeScreen(string usr)
        {
            InitializeComponent();
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            Background = new SolidColorBrush(ThemeColor.currentBackgroundColor);
            txtUserName.Text = "Hola "+ConnectionClass.Utype+", "+usr+"!";
            Closed += HomeScreen_Closed;
            ArmTabs();
        }

        private void HomeScreen_Closed(object sender, EventArgs e)
        {
            onThisClose?.Invoke(this, e);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           DragMove();
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
                tt.Foreground = new SolidColorBrush(onHoverAndSelectedColor);
            }
            ((TextBlock)sender).Background = new SolidColorBrush(onHoverAndSelectedColor);
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.White);
            remainSelected();
        }

        private void T_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnMouseDown((TextBlock)sender);
        }


        private void OnMouseDown(TextBlock tName)
        {
            cleanSelection();
            string name = tName.Name;
            curTab = tName;
            remainSelected();
            switch (name)
            {
                case "t1": ccPre.Content = new StudentEntryScreen(); break;
                case "t2": ccPre.Content = new LibraryEntryScreen(); break;
                case "t3": ccPre.Content = new InstallmentEntryScreen(); break;
                case "t4": ccPre.Content = new BusEntryScreen(); break;
                case "t5": ccPre.Content = new SearchWindow(); break;
                case "t6":
                    {
                        AdminSettings adS = new AdminSettings();
                        adS.KD += AdS_KD;
                        ccPre.Content = adS;
                        break;
                    }
                case "t7": ccPre.Content = new WelcomeHelpScreen(); break;
                case "t8": this.Close(); break;
            }
        }

        private void AdS_KD(object sender, MouseEventArgs e)
        {
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            Background = new SolidColorBrush(ThemeColor.currentBackgroundColor);
            onHoverAndSelectedColor = ThemeColor.currentAcColor;
            cleanSelection();
            remainSelected();
        }

        private void cleanSelection() {
            foreach (UIElement t in sPan.Children)
            {
                TextBlock tt = (TextBlock)t;
                tt.Background = new SolidColorBrush(Colors.Transparent);
                tt.Foreground = new SolidColorBrush(onHoverAndSelectedColor);
            }
        }
        private void remainSelected() {
            if (curTab != null)
            {
                ((TextBlock)curTab).Background = new SolidColorBrush(onHoverAndSelectedColor);
                ((TextBlock)curTab).Foreground = new SolidColorBrush(Colors.White);
            }
        }

        

    }
}
