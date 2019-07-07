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
                EdgeControl EC = new EdgeControl(E);
                EC.RemoveEdge += EdgeControl_RemoveEdge;
                GraphGrid.Children.Add(EC);
            }

            foreach (Vertex v in ContainingMachine.Vertices)
            {
                VertexControl vc = new VertexControl(v);
                vc.ConnectRequest += Vc_ConnectRequest;
                vc.RemoveVertex += Vc_RemoveVertex;
                vc.MouseDown += Vc_MouseDown;
                GraphGrid.Children.Add(vc);
            }

           
        }

        private void Vc_RemoveVertex(object sender, Vertex e)
        {
            ContainingMachine.Vertices.Remove(e);
            PurgeOrphanEdges();
            InvalidateMachineGraph();
        }

        public void PurgeOrphanEdges()
        {
            for (int i = 0; i < ContainingMachine.Edges.Count; i++)
            {
                if (!ContainingMachine.Vertices.Contains(ContainingMachine.Edges[i].Source) || !ContainingMachine.Vertices.Contains(ContainingMachine.Edges[i].Destination))
                {
                    ContainingMachine.Edges.RemoveAt(i);
                    i--;
                }
            }
        }

        private void EdgeControl_RemoveEdge(object sender, Edge e)
        {
            ContainingMachine.Edges.Remove(e);
            InvalidateMachineGraph();
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
