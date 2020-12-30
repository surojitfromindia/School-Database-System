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
    /// Interaction logic for InstallmentPerId.xaml
    /// </summary>
    public partial class InstallmentPerId : UserControl
    {
        public InstallmentPerId()
        {
            InitializeComponent();
        }

        public InstallmentPerId(string sid)
        {
            InitializeComponent();
            List<Installment> lbs = Installment.FindAllInstallment(sid);
            if (lbs.Count > 0)
            {
                foreach (var ite in lbs)
                {
                    InstallmentRow IRow = new InstallmentRow();
                    IRow.txtAmount.Text = ite.inst.ToString();
                    IRow.txtPDate.Text = ite.payDate.ToString("dd-MM-yyyy");
                    IRow.txtINumber.Text = ite.isnumber.ToString();
                    IRow.txtFine.Text = ite.tFine.ToString();
                    IRow.txtRp.Text = ite.rp;
                    IRow.txtGST.Text = ite.gst.ToString();
                    lstPanel.Children.Add(IRow);
                }
            }
        }
    }
}
