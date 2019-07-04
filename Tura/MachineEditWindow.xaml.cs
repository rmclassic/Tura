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
using System.Windows.Shapes;
using Tura.Models;
using Tura.Controls;

namespace Tura
{
    /// <summary>
    /// Interaction logic for MachineEditWindow.xaml
    /// </summary>
    public partial class MachineEditWindow : Window
    {
        Machine ContainingMachine;
        public MachineEditWindow(Machine containingmachine)
        {
            
            InitializeComponent();
            ContainingMachine = containingmachine;
            InvalidateMachineGraph();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContainingMachine.Vertices.Add(new Vertex("V", new Point(20, 20)));
            InvalidateMachineGraph();
        }

        void InvalidateMachineGraph()
        {
            GraphGrid.Children.Clear();
            foreach (Vertex v in ContainingMachine.Vertices)
            {
                GraphGrid.Children.Add(new VertexControl(v));
            }
        }
    }
}
