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
    /// Interaction logic for StudentSmallDetails.xaml
    /// </summary>
    public partial class StudentSmallDetails : UserControl
    {
        public StudentSmallDetails()
        {
            InitializeComponent();
        }
        public StudentSmallDetails(string id, string name, string sec, string clas, string roll, string contact)
        {
            InitializeComponent();
            txtName.Text = name;
            txtSID.Text = id;
            txtSec.Text = sec;
            txtRoll.Text = roll ;
            txtClass.Text = clas;
            txtCon.Text = contact ;
        }
       
    }
}
