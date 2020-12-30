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
    /// Interaction logic for InstallmentEntryScreen.xaml
    /// </summary>
    public partial class InstallmentEntryScreen : UserControl
    {
        public InstallmentEntryScreen()
        {
            InitializeComponent();
            txtPdate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Installment i = new Installment(txtSID.Text, int.Parse(txtINumber.Text), int.Parse(txtAmount.Text),
                double.Parse(txtGST.Text), DateTime.Parse(txtPdate.Text), double.Parse(txtFine.Text), txtReport.Text
                );
            MessageBox.Show(i.Create() ? "Save Successful" : "Not Succesful");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            NewForm();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Installment lastInstallment = Installment.GetLastInstallment(txtSID.Text);
            if (lastInstallment != null)
            {
                txtINumber.Text = lastInstallment.isnumber.ToString();
                txtAmount.Text = lastInstallment.inst.ToString();
                txtGST.Text = lastInstallment.gst.ToString();
                txtPdate.Text = lastInstallment.payDate.ToString("dd/MM/yyyy");
                txtFine.Text = lastInstallment.tFine.ToString();
                txtReport.Text = lastInstallment.rp;
            }
        }

        void NewForm()
        {
            txtINumber.Clear();
            txtAmount.Text = "0";
            txtPdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtSID.Clear();
        }

        private void txtSID_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtINumber.Text = Installment.getLastInstallmentNumberOf(txtSID.Text).ToString();
        }

        private void txtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtGST != null) //let it load
                txtGST.Text = (int.Parse(txtAmount.Text) * 0.18).ToString();
        }
    }
}
