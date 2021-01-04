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

        public static Color currentBackgroundColor =ChangeBackgroundColor(ColorCode.Light);


        public static Color ChangeAccentColor(ColorCode c)
        {
            Color currentAcColor = new Color() ;
            switch (c)
            {
                case ColorCode.Fire: currentAcColor = (Color)ColorConverter.ConvertFromString("#FFC97A2A"); break;
                case ColorCode.Mint: currentAcColor = (Color)ColorConverter.ConvertFromString("#FF05AA75"); break;
                case ColorCode.CoolBlue: currentAcColor = (Color)ColorConverter.ConvertFromString("#FF125EAA"); break;
            }
            return currentAcColor;
        }
        public static Color ChangeBackgroundColor(ColorCode c)
        {
            Color currentBackColor = new Color();
            switch (c)
            {
                case ColorCode.Dark: currentBackColor = (Color)ColorConverter.ConvertFromString("#FF343434"); break;
                case ColorCode.Light: currentBackColor = Colors.White; break;
            }
            return currentBackColor;
        }
    }

    enum ColorCode
    {
        Fire = 1,
        Mint = 2,
        CoolBlue =3,
        Dark = 4,
        Light = 5,
    }
}
