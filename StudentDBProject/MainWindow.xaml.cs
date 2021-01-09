using StudentDBProject.Models;
using StudentDBProject.WindowsScreens;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace StudentDBProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            b1.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            Background = new SolidColorBrush(ThemeColor.currentBackgroundColor);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginAction();

        }

        private void LoginAction()
        {
            ConnectionClass.ConnectToDB();
            ConnectionClass cc = new ConnectionClass(iU.Text, iP.Password);
            bool isNext = cc.Login();
            HomeScreen g = new HomeScreen(iU.Text);
            g.onThisClose += G_onThisClose;
            if (isNext)
            {
                g.Show();
                Visibility = Visibility.Hidden;
                //iU.Text = ""; iP.Password = "";
            }
        }

        private void G_onThisClose(object sender, EventArgs e)
        {
            Visibility = Visibility.Visible;
            
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            iU.Text = ""; iP.Password = "";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                LoginAction();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Window basic = new Window();
            basic.Title = "Help";
            basic.Width = 750;
            basic.Height = 570;
            basic.Content = new WelcomeHelpScreen();
            basic.ShowDialog();
        }
    }
}
