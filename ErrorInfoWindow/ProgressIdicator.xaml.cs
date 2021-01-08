using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ErrorInfoWindow
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ProgressIndicator : UserControl
    {
        private List<DataEntryErrorInfoClass> errorDetailsAndAction 
            = new List<DataEntryErrorInfoClass>();
        private double progressChangePerValue = 0;
        public List<DataEntryErrorInfoClass> ErrorDetailsAndAction
        {
            set
            {
                //Set Error List, 
                //Compute ProgressBar Unit Change
                //Update the List according to test
                errorDetailsAndAction = value;
                ComputePLength();
                UpdateChecking();
            }
        }

        public ProgressIndicator(List<DataEntryErrorInfoClass> dts)
        {
            InitializeComponent();
            ErrorDetailsAndAction = dts;
        }

        public ProgressIndicator()
        {
            InitializeComponent();
        }


        public bool UpdateChecking()
        {
            p1.Items.Clear();
            bool i = true;
            foreach (var dt in errorDetailsAndAction)
            {
                if (!dt.PredicateFunction())
                    p1.Items.Add(AddLabel(dt.ErrorSuggestionText));
                i = i && dt.PredicateFunction();
            }
            if(i)
            {
                p1.Items.Clear();
                p1.Items.Add(AddLabel("No Problem Found!\nProced"));
            }
            UpdateProgressBar();
            return i;
        }

        private TextBlock AddLabel(string text)
        {
            TextBlock lb = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
            };
            return lb;
        }

        private void UpdateProgressBar()
        {
            int prog = 0;
            foreach (var dt in errorDetailsAndAction)
            {
                if (dt.PredicateFunction())
                    prog += 1;
            }
            Ptxt.Text = prog.ToString() + "/" + errorDetailsAndAction.Count;
            ppProgress.Value = progressChangePerValue * prog;
        }

        public void ComputePLength()
        {
            double cLength = C1.Width.Value;
            int itermCount = errorDetailsAndAction.Count;
            if (itermCount == 0)
                progressChangePerValue = cLength / 1;
            else
                progressChangePerValue = cLength / itermCount;
            ppProgress.LargeChange = progressChangePerValue;
            ppProgress.Maximum = cLength;
        }

        public void SetAccColor(Color c)
        {
            p1.Foreground = new SolidColorBrush(c);
            ppT.Foreground = new SolidColorBrush(c);
            ppProgress.Foreground = new SolidColorBrush(c);
        }

    }
}

