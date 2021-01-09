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
      
        public static Color currentAcColor = ChangeAccentColor(ColorCode.Mint);
        public static Color currentFontColor = ChangeFontColor(ColorCode.White);
        public static Color currentBackgroundColor =ChangeBackgroundColor(ColorCode.Light); //Only: Light, Dark


        public static Color ChangeAccentColor(ColorCode c)
        {
            Color currentAcColor = new Color() ;
            switch (c)
            {
                case ColorCode.Fire: currentAcColor = (Color)ColorConverter.ConvertFromString("#FFC97A2A"); break;
                case ColorCode.Mint: currentAcColor = (Color)ColorConverter.ConvertFromString("#FF05AA75"); break;
                case ColorCode.CoolBlue: currentAcColor = (Color)ColorConverter.ConvertFromString("#FF2F93F7"); break;
                case ColorCode.White: currentAcColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF"); break;
            }
            return currentAcColor;
        }

        public static Color ChangeFontColor(ColorCode c)
        {
            Color currentFontColor = new Color();
            switch (c)
            {
                case ColorCode.Dark: currentFontColor = (Color)ColorConverter.ConvertFromString("#FF343434"); break;
                case ColorCode.White: currentFontColor = (Color)ColorConverter.ConvertFromString("#FFF3F3F3"); break;
            }
            return currentFontColor;
        }

        public static Color ChangeBackgroundColor(ColorCode c)
        {
            Color currentBackColor = new Color();
            switch (c)
            {
                case ColorCode.Dark: currentBackColor = (Color)ColorConverter.ConvertFromString("#FF343434"); break;
                case ColorCode.Light: currentBackColor = (Color)ColorConverter.ConvertFromString("#FFF3F3F3"); break;

            }
            return currentBackColor;
        }
    }

    enum ColorCode
    {
        Fire = 1,
        Mint = 2,
        CoolBlue =3,
        White = 4,
        Dark = 5,
        Light = 6,
    }
}
