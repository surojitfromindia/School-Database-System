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
            txtIdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Library lb = new Library(txtSID.Text, txtBookName.Text,
               DateTime.Parse(txtIdate.Text), DateTime.Parse(txtrdate.Text),
               double.Parse(txtFine.Text), lblRp.Content.ToString());
            if (lb.Create())
                lblRest.Text = "Record Saved!";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Library lb = new Library(txtSID.Text, txtBookName.Text,
                DateTime.Parse(txtIdate.Text), DateTime.Parse(txtrdate.Text),
                double.Parse(txtFine.Text), lblRp.Content.ToString());
            if (Library.Update(lb))
            {
                MessageBox.Show("Updated");
                lblRest.Text = "Update Successful!";
                Search();
            }
            else
                MessageBox.Show("Update Faild!");
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
                FillInfoFieldWith(foundBook);
            else
                ResetInputFields();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetInputFields();
        }

        private void txtSID_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtLBC.Text = txtSID.Text + "L";
        }

        void ResetInputFields()
        {
            txtFine.Text = "0"; lblRp.Content = "HOLD"; txtIdate.Clear();
            txtIdate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtLBC.Clear(); txtBookName.Clear();
        }

        void FillInfoFieldWith(Library foundBook)
        {
            txtBookName.Text = foundBook.bname;
            txtIdate.Text = foundBook.iDate.ToString("dd/MM/yyyy");
            txtrdate.Text = foundBook.rDate.ToString("dd/MM/yyyy");
            txtFine.Text = foundBook.fine.ToString();
            lblRp.Content = foundBook.rp;
            txtLBC.Text = foundBook.sid + "L";
            listPane.Children.Clear();
            BookPerIdScreen bp = new BookPerIdScreen(txtSID.Text);
            listPane.Children.Add(bp);
        }
    }
}
