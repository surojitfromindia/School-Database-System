using StudentDBProject.Models;
using StudentDBProject.WindowsScreens.MiniControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace StudentDBProject.WindowsScreens
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        private int MDKC = 0;
        public event EventHandler onThisClose;
        private object curTab;
        private AdminSettings adS;
        public HomeScreen()
        {
            InitializeComponent();
        }

        public HomeScreen(string usr)
        {
            InitializeComponent();

            accHandler.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            Background = new SolidColorBrush(ThemeColor.currentBackgroundColor);
            foreach (UIElement t in sPan.Children)
            {
                TextBlock tt = (TextBlock)t;
                tt.Background = new SolidColorBrush(Colors.Transparent);
                tt.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            }
            txtUserName.Text = "Hola " + ConnectionClass.UserType + ", " + usr + "!";
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

        void ArmTabs()
        {
            foreach (UIElement t in sPan.Children)
            {
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
                tt.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            }
            ((TextBlock)sender).Background = new SolidColorBrush(ThemeColor.currentAcColor);
            ((TextBlock)sender).Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
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
            MDKC = int.Parse(name.Substring(1));
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
                        if (adS == null)
                            adS = new AdminSettings();
                        adS.KD += AdS_KD;
                        ccPre.Content = adS;
                        break;
                    }
                case "t7": ccPre.Content = new WelcomeHelpScreen(); break;
                case "t8":
                    {
                        AlertDialogCus iw = new AlertDialogCus("Exit Application", "This will close this application\nAre your sure ?", "Yes"
                            );
                        iw.ShowDialog();
                        if (!iw.IsCanceldPress)
                            Close();
                        break;
                    }
            }
        }

        private void AdS_KD(object sender, MouseEventArgs e)
        {
            Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            accHandler.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            Background = new SolidColorBrush(ThemeColor.currentBackgroundColor);
            cleanSelection();
            remainSelected();
        }

        private void cleanSelection()
        {
            foreach (UIElement t in sPan.Children)
            {
                TextBlock tt = (TextBlock)t;
                tt.Background = new SolidColorBrush(Colors.Transparent);
                tt.Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            }
        }
        private void remainSelected()
        {
            if (curTab != null)
            {
                ((TextBlock)curTab).Background = new SolidColorBrush(ThemeColor.currentAcColor);
                ((TextBlock)curTab).Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.OemTilde:
                    {
                        AlertDialogCus i = new AlertDialogCus(
                            "Search", new SearchStudentMiniControl(),
                            cancleButtonText: "Close", isActionButtonHiden: true)
                        {
                            Width = 820,
                            Height = 600
                        }
                        ;
                        i.ShowDialog();
                        break;
                    }
            }
            if (e.Key == Key.Down)
            {
                MDKC++;
                string oName = "t" + MDKC;
                if (MDKC <= 7)
                {
                    OnMouseDown((TextBlock)FindName(oName));
                    if (MDKC == 7)
                        MDKC = 0;
                }
            }
        }
    }
}
