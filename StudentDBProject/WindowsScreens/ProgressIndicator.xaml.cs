using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace StudentDBProject.WindowsScreens
{
    /// <summary>
    /// Interaction logic for ProgressIndicator.xaml
    /// </summary>
    public partial class ProgressIndicator : UserControl
    {
        private List<DataEntryErrorInfoClass> dts;
        private int perUnit = 0;
        public ProgressIndicator(List<DataEntryErrorInfoClass> dts)
        {
            InitializeComponent();
            this.dts = dts;
            perUnit =200 / dts.Count;
            ppProgress.LargeChange = perUnit;
            ppProgress.Maximum = 200;
            UpdateChecking();
        }

        public ProgressIndicator()
        {
            InitializeComponent();
        }

        public bool UpdateChecking()
        {
            p1.Items.Clear();
            bool i = true;
            foreach (var dt in dts)
            {
                if (!dt.action())
                    p1.Items.Add(AddLabel(dt.onErrorSuggestion));
                i = i && dt.action();
            }
            UpdateProgressBar();
            return i;
        }

        TextBlock AddLabel(string text)
        {
            TextBlock lb = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Width = 220,
                Margin = new Thickness(5),
            };
            return lb;
        }

        void UpdateProgressBar()
        {
            int prog = 0;
            foreach (var dt in dts)
            {
                if (dt.action())
                    prog += 1; 
            }
            Ptxt.Text = prog.ToString()+"/"+dts.Count;
            ppProgress.Value = perUnit * prog;

        }


    }
}
