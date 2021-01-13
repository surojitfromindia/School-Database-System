using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StudentDBProject.Models
{
    static class ThemeColor
    {

        public static Color currentAcColor = ChangeAccentColor(ColorCode.CoolBlue);
        public static Color currentFontColor = ChangeFontColor(ColorCode.Light);
        public static Color currentBackgroundColor = ChangeBackgroundColor(ColorCode.Light); //Only: Light, Dark


        public static Color ChangeAccentColor(ColorCode c)
        {
            Color currentAcColor = GetColorFromColorCode(c);
            return currentAcColor;
        }

        public static Color ChangeFontColor(ColorCode c)
        {
            Color currentFontColor = GetColorFromColorCode(c);
            return currentFontColor;
        }

        public static Color ChangeBackgroundColor(ColorCode c)
        {
            Color currentBackgroundColor = GetColorFromColorCode(c);
            return currentBackgroundColor;
        }

        public static Color GetColorFromString(string colorname)
        {
            Color p = Colors.White;
            switch (colorname)
            {
                //Add Color Here.
                case "Fire": p = (Color)ColorConverter.ConvertFromString("#FFC97A2A"); break;
                case "Mint": p = (Color)ColorConverter.ConvertFromString("#FF00A871"); break;
                case "CoolBlue": p = (Color)ColorConverter.ConvertFromString("#FF2F93F7"); break;
                case "White": p = (Color)ColorConverter.ConvertFromString("#FFFFFFFF"); break;
                case "Dark": p = (Color)ColorConverter.ConvertFromString("#FF343434"); break;
                case "Light": p = (Color)ColorConverter.ConvertFromString("#FFF3F3F3"); break;
            }
            return p;
        }

        public static Color GetColorFromColorCode(ColorCode c)
        {
            Color p = Colors.White;
            switch(c)
            {
                //Add Color Here.
                case ColorCode.Fire: p = (Color)ColorConverter.ConvertFromString("#FFC97A2A"); break;
                case ColorCode.Mint: p = (Color)ColorConverter.ConvertFromString("#FF00A871"); break;
                case ColorCode.CoolBlue: p = (Color)ColorConverter.ConvertFromString("#FF2F93F7"); break;
                case ColorCode.White: p = (Color)ColorConverter.ConvertFromString("#FFFFFFFF"); break;
                case ColorCode.Dark: p = (Color)ColorConverter.ConvertFromString("#FF343434"); break;
                case ColorCode.Light: p = (Color)ColorConverter.ConvertFromString("#FFF3F3F3"); break;
            }
            return p;
        }
    }

    public enum ColorCode
    {
        //Add Color Codes Here.
        Fire = 1,
        Mint = 2,
        CoolBlue = 3,
        White = 4,
        Dark = 5,
        Light = 6,
    }
}
