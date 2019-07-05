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
using Tura.Util;
namespace Tura
{
    /// <summary>
    /// Interaction logic for MachineEditWindow.xaml
    /// </summary>
    public partial class MachineEditWindow : Window
    {
        bool ConnectionRequested = false;
        Vertex ConnectionRequestSource;
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


            foreach (Edge E in ContainingMachine.Edges)
            {
                GraphGrid.Children.Add(new EdgeControl(E));
            }

            foreach (Vertex v in ContainingMachine.Vertices)
            {
                VertexControl vc = new VertexControl(v);
                vc.ConnectRequest += Vc_ConnectRequest;
                vc.MouseDown += Vc_MouseDown;
                GraphGrid.Children.Add(vc);
            }

           
        }

        private void Vc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VertexControl control = sender as VertexControl;
            if (ConnectionRequested)
            {
                if (ContainingMachine.Edges.Contains(new Edge(ConnectionRequestSource, control.ContainingVertex, 'c'), new EdgeComparer()))
                {
                    MessageBox.Show("Edge is already added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ConnectionRequested = false;
                    return;
                }
                ContainingMachine.Edges.Add(new Edge(ConnectionRequestSource, control.ContainingVertex, 'c'));
                ConnectionRequested = false;
                InvalidateMachineGraph();
            }

        }

        private void Vc_ConnectRequest(object sender, Vertex e)
        {
            ConnectionRequested = true;
            ConnectionRequestSource = e;
        }
    }
}
