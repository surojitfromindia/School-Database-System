using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ErrorInfoWindow
{
    public class DataEntryErrorInfoClass
    {
       
        public string ErrorSuggestionText { get; private set; }
        public Func<bool> PredicateFunction { get; private set; }
        public DataEntryErrorInfoClass(string errorSuggestionText, Func<bool> predicateFunction)
        {

            ErrorSuggestionText = errorSuggestionText;
            PredicateFunction = predicateFunction;
        }
    }
}
