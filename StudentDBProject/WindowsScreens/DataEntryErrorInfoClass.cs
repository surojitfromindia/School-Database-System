using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StudentDBProject.WindowsScreens
{
    public class DataEntryErrorInfoClass
    {
       
        public string onErrorSuggestion;
        public Func<bool> action;
        public DataEntryErrorInfoClass(string onErrorSuggestion, Func<bool> action)
        {
            
            this.onErrorSuggestion = onErrorSuggestion;
            this.action = action;
        }
    }
}
