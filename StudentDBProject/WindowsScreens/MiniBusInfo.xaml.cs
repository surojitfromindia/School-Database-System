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

namespace StudentDBProject.WindowsScreens
{
    /// <summary>
    /// Interaction logic for MiniBusInfo.xaml
    /// </summary>
    /// 

    public partial class MiniBusInfo : UserControl
    {
        private string id;
        public MiniBusInfo()
        {
            InitializeComponent();
            b1.Background = new SolidColorBrush(ThemeColor.currentAcColor);
        }

        public MiniBusInfo(string id)
        {
            InitializeComponent();
            b1.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            this.id = id;
            LoadInfo();
           
        }

        private void LoadInfo()
        {
            Bus b = Bus.FindBusI(id);
            if (b != null)
            {
                (_, lblBusID.Content, lblAmount.Content,
                      lblReport.Content, lblPDate.Content, lblFine.Content) = b;

            }
        }
    }
}
