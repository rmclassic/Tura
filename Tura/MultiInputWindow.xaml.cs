using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Tura;

namespace Tura
{
    /// <summary>
    /// Interaction logic for MultiInputWindow.xaml
    /// </summary>
    public partial class MultiInputWindow : Window
    {
        List<string> InputList = new List<string>();
        
        public MultiInputWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveInput();
        }

        public List<string> GetInput()
        {
            return InputList;
        }

        TextBox CreateItemtextBox()
        {
            TextBox t = new TextBox();
            t.Width = Width - 20;
            t.HorizontalAlignment = HorizontalAlignment.Center;
            t.Margin = new Thickness(0, 2, 0, 2);
            return t;
        }


        void SaveInput()
        {
            InputList.Clear();

            foreach (TextBox t in ConditionsPanel.Children)
            {
                if (t.Text != "")
                    InputList.Add(t.Text);
            }
        }

        private void AddItemButtonClick_Click(object sender, RoutedEventArgs e)
        {
            TextBox t = CreateItemtextBox();
            ConditionsPanel.Children.Add(t);
        }
    }

}
