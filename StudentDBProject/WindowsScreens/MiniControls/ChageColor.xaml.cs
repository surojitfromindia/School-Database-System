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

namespace StudentDBProject.WindowsScreens.MiniControls
{
    /// <summary>
    /// Interaction logic for ChageColor.xaml
    /// </summary>
    public partial class ChageColor : UserControl
    {

        private static Ellipse lastSelectedAccentOption;
        public event MouseButtonEventHandler onColorSelect;
        private Ellipse[] ColorAccentOptionEllipse;
        private Ellipse[] ColorBackgroundOptionEllipse;

        public ChageColor()
        {
            InitializeComponent();
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);

            //Store All Color Options Control Here
            //When user Select/Click one of the Color Cicle
            //Reset Unselected Option will go back to their unselected dimenssion (45 x 45)
            ColorAccentOptionEllipse = new Ellipse[] { Fire, Mint, CoolBlue, White };
            ColorBackgroundOptionEllipse = new Ellipse[] { Dark, Light };


            //Set Color Circle as default controls.
            setColorsInCircle();
            defaultBackAndAccentColor(Mint, Light);

            //Color Circles Select/Click Events
            Fire.MouseDown += onAccentColorOptionClick;
            Mint.MouseDown += onAccentColorOptionClick;
            CoolBlue.MouseDown += onAccentColorOptionClick;
            White.MouseDown += onAccentColorOptionClick;
            Dark.MouseDown += onBackgroundColorOptionClick;
            Light.MouseDown += onBackgroundColorOptionClick;

        }

        private void onBackgroundColorOptionClick(object sender, MouseButtonEventArgs e)
        {
            string colorName = ((Ellipse)sender).Name;
            switch (colorName)
            {
                case "Dark":
                    {
                        ThemeColor.currentBackgroundColor = ThemeColor.ChangeBackgroundColor(ColorCode.Dark);
                        break;
                    }
                case "Light":
                    {
                        ThemeColor.currentAcColor = ThemeColor.ChangeAccentColor(ColorCode.Mint);
                        ThemeColor.currentBackgroundColor = ThemeColor.ChangeBackgroundColor(ColorCode.Light);
                        ThemeColor.currentFontColor = ThemeColor.ChangeAccentColor(ColorCode.White);
                        if (lastSelectedAccentOption!=null && lastSelectedAccentOption.Name == "White")
                        {
                            UnSelectAllAccentColorOption();
                            PointSelectedColorOption(Mint);
                        }
                        break;
                    }
            }
            UnSelectAllBackgroundColorOption();
            PointSelectedColorOption((Ellipse)sender);
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            onColorSelect?.Invoke(this, e);
        }

        private void onAccentColorOptionClick(object sender, MouseButtonEventArgs e)
        {
            string colorName = ((Ellipse)sender).Name;
            switch (colorName)
            {
                case "Fire":
                    {
                        ThemeColor.currentAcColor = ThemeColor.ChangeAccentColor(ColorCode.Fire);
                        ThemeColor.currentFontColor = ThemeColor.ChangeFontColor(ColorCode.White);
                        break;
                    }
                case "Mint":
                    {
                        ThemeColor.currentAcColor = ThemeColor.ChangeAccentColor(ColorCode.Mint);
                        ThemeColor.currentFontColor = ThemeColor.ChangeFontColor(ColorCode.White);
                        break;
                    }
                case "CoolBlue":
                    {
                        ThemeColor.currentAcColor = ThemeColor.ChangeAccentColor(ColorCode.CoolBlue);
                        ThemeColor.currentFontColor = ThemeColor.ChangeFontColor(ColorCode.White);
                        break;
                    }
                case "White":
                    {
                        ThemeColor.currentAcColor = ThemeColor.ChangeAccentColor(ColorCode.White);
                        ThemeColor.currentFontColor = ThemeColor.ChangeFontColor(ColorCode.Dark);
                        ThemeColor.currentBackgroundColor = ThemeColor.ChangeBackgroundColor(ColorCode.Dark);
                        UnSelectAllBackgroundColorOption();
                        PointSelectedColorOption(Dark);
                        break;
                    }

            }
            UnSelectAllAccentColorOption();
            lastSelectedAccentOption =(Ellipse)sender;
            PointSelectedColorOption((Ellipse)sender);
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
            onColorSelect?.Invoke(this, e);  
        }

        void PointSelectedColorOption(Ellipse es)
        {
           
            es.Width = 35;
            es.Height = 35;
        }

        void UnSelectAllAccentColorOption()
        {
            foreach (var elp in ColorAccentOptionEllipse)
            {
                elp.Height = 45;
                elp.Width = 45;
            }
        }

        void UnSelectAllBackgroundColorOption()
        {
            foreach (var elp in ColorBackgroundOptionEllipse)
            {
                elp.Height = 45;
                elp.Width = 45;
            }
        }

        void defaultBackAndAccentColor(Ellipse acColor, Ellipse baColor)
        {
            acColor.Width = 35; acColor.Height = 35;
            baColor.Width = 35; baColor.Height = 35;
        }

        private void setColorsInCircle()
        {
            var c = ColorAccentOptionEllipse.Concat(ColorBackgroundOptionEllipse).ToList();
            c.ForEach(
                (ce) =>
                {
                    ce.Fill = new SolidColorBrush(ThemeColor.GetColorFromString(ce.Name));
                }
            );

        }
    }
}
