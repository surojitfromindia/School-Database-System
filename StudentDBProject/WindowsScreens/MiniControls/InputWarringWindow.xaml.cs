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

namespace StudentDBProject.WindowsScreens.MiniControls
{
    /// <summary>
    /// Interaction logic for InputWarringWindow.xaml
    /// </summary>
    public partial class AlertDialogCus : Window
    {
        public event RoutedEventHandler onActionClick;
        public object ReturnObject;

        public bool IsCanceldPress { get; private set; }
        public AlertDialogCus()
        {
            InitializeComponent();
            SetTheme();
        }

        public AlertDialogCus(string header, string subtext = "", string actionButtonText = "Ok", string cancleButtonText = "Cancel", bool isActionButtonHiden = false)
        {
            InitializeComponent();
            SetTheme();
            aboutAction.Text = header;
            TextBlock ttSubtext = new TextBlock();
            ttSubtext.TextWrapping = TextWrapping.Wrap;
            ttSubtext.Text = subtext;
            ccPre.Content = ttSubtext;
            btnAction.Content = actionButtonText;
            btnCancel.Content = cancleButtonText;
            if (isActionButtonHiden)
                btnAction.Visibility = Visibility.Hidden;
        }

        public AlertDialogCus(string header, Control miniControl, string actionButtonText = "Ok", string cancleButtonText = "Cancel", bool isActionButtonHiden = false)
        {
            InitializeComponent();
            SetTheme();
            aboutAction.Text = header;
            ccPre.Content = miniControl;
            btnAction.Content = actionButtonText;
            btnCancel.Content = cancleButtonText;
            if (isActionButtonHiden)
                btnAction.Visibility = Visibility.Hidden;
        }

        private void SetTheme()
        {
            Background = new SolidColorBrush((ThemeColor.currentBackgroundColor));
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            btnAction.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            btnAction.Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
            btnCancel.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            btnCancel.Foreground = new SolidColorBrush(ThemeColor.currentFontColor);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsCanceldPress = true;
            this.Close();
        }

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            IsCanceldPress = false;
            onActionClick?.Invoke(sender, e);
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
