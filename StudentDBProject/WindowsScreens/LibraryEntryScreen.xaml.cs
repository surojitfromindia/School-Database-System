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
    /// Interaction logic for LibraryEntryScreen.xaml
    /// </summary>
    public partial class LibraryEntryScreen : UserControl
    {
        public LibraryEntryScreen()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Library lb = new Library(txtSID.Text, txtBookName.Text,
               DateTime.Parse(txtIdate.Text), DateTime.Parse(txtrdate.Text),
               double.Parse(txtFine.Text), txtRp.Text);
            MessageBox.Show(lb.Create().ToString());
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Library lb = new Library(txtSID.Text, txtBookName.Text,
                DateTime.Parse(txtIdate.Text), DateTime.Parse(txtrdate.Text),
                double.Parse(txtFine.Text), txtRp.Text);
            Library.Update(lb);

        }

        private void txtSID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Search();
            }
        }

        void Search()
        {
            listPane.Children.Clear();
            Library foundBook = Library.FindLastEntry(txtSID.Text);
            if (foundBook != null)
            {
                txtBookName.Text = foundBook.bname;
                txtIdate.Text = foundBook.iDate.ToString("dd/MM/yyyy");
                txtrdate.Text = foundBook.rDate.ToString("dd/MM/yyyy");
                txtFine.Text = foundBook.fine.ToString();
                txtRp.Text = foundBook.rp;
                txtLBC.Text = foundBook.sid + "L";
                listPane.Children.Clear();
                BookPerIdScreen bp = new BookPerIdScreen(txtSID.Text);
                listPane.Children.Add(bp);
            }
            else
            {
                txtFine.Text = "0"; txtRp.Text = "HOLD"; txtIdate.Clear();
                txtrdate.Clear(); txtLBC.Clear(); txtBookName.Clear();
                txtSID.Clear();
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFine.Text = "0"; txtRp.Text = "HOLD"; txtIdate.Clear();
            txtrdate.Clear(); txtLBC.Clear(); txtBookName.Clear();
            txtSID.Clear();
        }

        private void txtSID_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtLBC.Text = txtSID.Text + "L";
        }

        private void BookPerIdScreen_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
