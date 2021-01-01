using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StudentDBProject.Models
{
    class ThemeColor
    {
      
        public static Color currentAcColor = (Color)ColorConverter.ConvertFromString("#FF125EAA");        

        public static void ChangeColor(int code)
        {
            switch (code)
            {
                case 1: currentAcColor = Colors.Purple; break;
                case 2: currentAcColor = Colors.RoyalBlue; break;
            }
           
        }
    }
}
