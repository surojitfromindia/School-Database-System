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
    /// Interaction logic for BusEntryScreen.xaml
    /// </summary>
    public partial class BusEntryScreen : UserControl
    {
        public BusEntryScreen()
        {
            InitializeComponent();
            txtPayDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        

        private void chkIsFineApplicable_Checked(object sender, RoutedEventArgs e)
        {
            txtFine.Text = (int.Parse(txtInst.Text) * (0.1)).ToString();
        }

        private void chkIsFineApplicable_Unchecked(object sender, RoutedEventArgs e)
        {
            txtFine.Text = "0";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Bus bb = new Bus(txtSid.Text, txtBId.Text, int.Parse(txtInst.Text),
                DateTime.Parse(txtPayDate.Text), double.Parse(txtFine.Text), txtRp.Text);
            if (bb.Create())
                MessageBox.Show("Transaction Complete");
            else
                MessageBox.Show("Faild");

        }

        private void txtSid_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBId.Text = txtSid.Text + "B";
        }

       

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtPayDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtSid.Clear(); txtRp.Clear(); txtBId.Clear(); chkIsFineApplicable.IsChecked = false;
            txtRp.Text = "Paid";txtInst.Text="0";
        }

       

        
    }
}
