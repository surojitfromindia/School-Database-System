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
        private static Ellipse lastSelectedBackgroundOption;
        private static bool isFirstLaunch = true;
        public event MouseButtonEventHandler onColorSelect;
        private Ellipse[] ColorAccentOptionEllipse;
        private Ellipse[] ColorBackgroundOptionEllipse;

        public ChageColor()
        {
            InitializeComponent();
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);

            //Set Color Circle as default controls.
            if (isFirstLaunch)
            {
                defaultBackAndAccentColor(CoolBlue, Light);
                isFirstLaunch = false;
            }
            //Store All Color Options Control Here
            //When user Select/Click one of the Color Cicle
            //Reset Unselected Option will go back to their unselected dimenssion (45 x 45)
            ColorAccentOptionEllipse = new Ellipse[] { Fire, Mint, CoolBlue };
            ColorBackgroundOptionEllipse = new Ellipse[] { Dark, Light };

            //When User Open The Admin Panel Again,
            //last selected option will show it's selected size (40 x 40)
            if (lastSelectedAccentOption != null)
            {
                Ellipse elps = (Ellipse)FindName(lastSelectedAccentOption.Name);
                elps.Width = 40;
                elps.Height = 40;
            }
            if (lastSelectedBackgroundOption != null)
            {
                Ellipse elps = (Ellipse)FindName(lastSelectedBackgroundOption.Name);
                elps.Width = 40;
                elps.Height = 40;
            }

            //Color Circles Select/Click Events
            Fire.MouseDown += onAccentColorOptionClick;
            Mint.MouseDown += onAccentColorOptionClick;
            CoolBlue.MouseDown += onAccentColorOptionClick;
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
                        ThemeColor.currentBackgroundColor = ThemeColor.ChangeBackgroundColor(ColorCode.Light);
                        break;
                    }
            }
            UnSelectAllBackgroundColorOption();
            var temp = (Ellipse)sender;
            lastSelectedBackgroundOption = temp;
            temp.Width = 40;
            temp.Height = 40;
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
                        break;
                    }
                case "Mint":
                    {
                        ThemeColor.currentAcColor = ThemeColor.ChangeAccentColor(ColorCode.Mint);
                        break;
                    }
                case "CoolBlue":
                    {
                        ThemeColor.currentAcColor = ThemeColor.ChangeAccentColor(ColorCode.CoolBlue);
                        break;
                    }
                case "Dark":
                    {
                        ThemeColor.currentBackgroundColor = ThemeColor.ChangeBackgroundColor(ColorCode.Dark);
                        break;
                    }
                case "Light":
                    {
                        ThemeColor.currentBackgroundColor = ThemeColor.ChangeBackgroundColor(ColorCode.Light);
                        break;
                    }
            }
            UnSelectAllAccentColorOption();
            var temp = (Ellipse)sender;
            lastSelectedAccentOption = temp;
            temp.Width = 40;
            temp.Height = 40;
            onColorSelect?.Invoke(this, e);
            Foreground = new SolidColorBrush(ThemeColor.currentAcColor);
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
            acColor.Width = 40; acColor.Height = 40;
            baColor.Width = 40; baColor.Height = 40;
        }
    }
}
