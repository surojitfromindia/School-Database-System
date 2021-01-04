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
    /// Interaction logic for BookPerIdScreen.xaml
    /// </summary>
    public partial class BookPerIdScreen : UserControl
    {
        public BookPerIdScreen()
        {
            InitializeComponent();
            b1.Background = new SolidColorBrush(ThemeColor.currentAcColor);
        }

        public BookPerIdScreen(string sid)
        {
            InitializeComponent();
            b1.Background = new SolidColorBrush(ThemeColor.currentAcColor);
            List<Library> lbs = Library.FindAllBooks(sid);
            if (lbs.Count > 0)
            {
                foreach (var ite in lbs)
                {
                    BookRow bRow = new BookRow();
                    bRow.txtBookName.Text = ite.bname;
                    bRow.txtISDate.Text = ite.iDate.ToString("dd-MM-yyyy");
                    bRow.txtRetDate.Text = ite.rDate.ToString("dd-MM-yyyy");
                    bRow.txtFine.Text = ite.fine.ToString();
                    bRow.txtRep.Text = ite.rp;
                    lstPanel.Children.Add(bRow);
                }
            }
        }
    }
}
