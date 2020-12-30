using StudentDBProject.Models;
using StudentDBProject.WindowsScreens;
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

namespace StudentDBProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                iU.Text = ""; iP.Password = "";
            }
        }

        private void G_onThisClose(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
            
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
