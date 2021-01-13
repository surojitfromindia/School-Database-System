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

namespace StudentDBProject.WindowsScreens.MicroControls
{
    /// <summary>
    /// Interaction logic for StudentSeatingVisual.xaml
    /// </summary>
    public partial class StudentSeatingVisual : UserControl
    {

        private int _itemNumber { get; set; }
        private int[] _rolls;
        private Dictionary<int, string> FullRollMap = new Dictionary<int, string>(10);

        public event MouseButtonEventHandler OnSeatItemClick;
        public static string currntValueOnGrid="";

        public StudentSeatingVisual(int itemNumber, IEnumerable<string> rolls)
        {
            InitializeComponent();
            _itemNumber = itemNumber;
            _rolls = rolls
                .ToList()
                .Select
                (
                (t)=> int.Parse(slice(t,-2,2))
                )
                .ToArray();
            rolls
                .ToList()
                .ForEach(
                (r) => FullRollMap.Add(int.Parse(slice(r, -2, 2)), r)
                );
            GenerateGrids();
        }

        static string slice(string s, int startIndex, int length)
        {
            string p = "";
            if (startIndex <= 0) //start from end
            {
                var _startindex = s.Length - (length - startIndex - 1);
                if (length > -1 && _startindex > -1)
                    p = s.Substring(_startindex, length);
            }
            else
                p = s.Substring(startIndex, length);
            return p;
        }

        void GenerateGrids()
        {
            //number of column is 8.
            FillWithRollUptoTop();
            FillWithRollThatArePresent();
        }
        private void FillWithRollUptoTop()
        {
            for(int i =1; i<= _itemNumber; i++)
            {
                if (!_rolls.Contains(i))
                {
                    TextBlock r = new TextBlock()
                    {
                        Text = i.ToString(),
                        Foreground = new SolidColorBrush(ThemeColor.currentFontColor),
                        TextAlignment = TextAlignment.Center,
                        Margin = new Thickness(2),
                        Padding = new Thickness(3),
                        
                    };
                    Grid.SetRow(r, i / 8);
                    Grid.SetColumn(r, (i % 8));
                    seatGrid.Children.Add(r);
                }
            }
        }



        private void FillWithRollThatArePresent()
        {
            foreach (int roll in _rolls)
            {
                TextBlock r = new TextBlock()
                {
                    Text = roll.ToString(),
                    Foreground = new SolidColorBrush(ThemeColor.currentAcColor),
                    Background = new SolidColorBrush(ThemeColor.currentFontColor),
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(2),
                    Padding = new Thickness(3),
                };
                r.MouseDown += R_MouseDown;
                Grid.SetRow(r, roll / 8);
                Grid.SetColumn(r, roll % 8);
                seatGrid.Children.Add(r);
            }
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int _r = int.Parse(((TextBlock)sender).Text);
            currntValueOnGrid = FullRollMap[_r];
            OnSeatItemClick.Invoke(this, e);
        }
    }
}
