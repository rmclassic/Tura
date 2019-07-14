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
using Tura.Controls;

namespace Tura.Controls
{
    /// <summary>
    /// Interaction logic for TuringMachineTapeControl.xaml
    /// </summary>
    public partial class TuringMachineTapeControl : UserControl
    {
        string InputString;
        public TuringMachineTapeControl(string input)
        {
            
            InitializeComponent();
            InputString = input;
            DrawCells();
        }

        public void ChangeInput(string input)
        {
            InputString = input;
            DrawCells();
        }

        void DrawCells()
        {
            double CellWidth = (this.ActualHeight - 41);
            CellsPanel.Children.Clear();

            for (int i = 0; i < this.ActualWidth / CellWidth / 2; i++)
                CellsPanel.Children.Add(new TuringTapeCellControl('#') { Height = CellWidth, Width = CellWidth });

            foreach (char c in InputString)
            {
                CellsPanel.Children.Add(new TuringTapeCellControl(c) {Height = CellWidth, Width = CellWidth });
                
            }

            for (int i = 0; i < this.ActualWidth / CellWidth / 2; i++)
                CellsPanel.Children.Add(new TuringTapeCellControl('#') { Height = CellWidth, Width = CellWidth });
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawCells();
        }

        public async Task ScrollToItem(int index)
        {
            double CellWidth = (this.ActualHeight - 42);

            if (CellsScrolViewer.HorizontalOffset < index * CellWidth)
            {
                for (double i = CellsScrolViewer.HorizontalOffset; i < index * CellWidth; i += 5)
                {
                    CellsScrolViewer.ScrollToHorizontalOffset(i + CellWidth / 2);
                    await Task.Delay(10);
                }
            }
            else
            {
                for (double i = CellsScrolViewer.HorizontalOffset; i > index * CellWidth; i -= 5)
                {
                    CellsScrolViewer.ScrollToHorizontalOffset(i + CellWidth / 2);
                    await Task.Delay(10);
                }
            }
        }
    }
}
