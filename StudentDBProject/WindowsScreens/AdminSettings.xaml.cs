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
    /// Interaction logic for AdminSettings.xaml
    /// </summary>
    public partial class AdminSettings : UserControl
    {


        public event MouseEventHandler KD;

        public AdminSettings()
        {
            InitializeComponent();
            changeColor.onColorSelect += ChangeColor_onColorSelect;
            
        }

        private void ChangeColor_onColorSelect(object sender, MouseButtonEventArgs e)
        {
            KD?.Invoke(this, e);
        }
    }
}
