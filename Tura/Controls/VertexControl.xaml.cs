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
using Tura.Models;

namespace Tura.Controls
{
    /// <summary>
    /// Interaction logic for VertexControl.xaml
    /// </summary>
    public partial class VertexControl : UserControl
    {
        public event EventHandler<Vertex> ConnectRequest;
        bool Dragging = false;
        Point MouseLastPosition = new Point();
        public Vertex ContainingVertex;
        public VertexControl(Vertex containingvertex)
        {
            InitializeComponent();
            ContainingVertex = containingvertex;
            NameTextBlock.Text = containingvertex.Name;
            Margin = new Thickness(ContainingVertex.Location.X, ContainingVertex.Location.Y, 0, 0);
            MouseDown += VertexControl_MouseDown;
            MouseUp += VertexControl_MouseUp;
            MouseMove += VertexControl_MouseMove;
        }

        private void VertexControl_MouseMove(object sender, MouseEventArgs e)
        {
            CheckVertexMove();
        }

        private void VertexControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Dragging = false;
        }

        private void VertexControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseLastPosition = Mouse.GetPosition(this);
            Dragging = true;
        }

        private void CheckVertexMove()
        {
            if (Dragging)
            {
                Point p = Mouse.GetPosition(this);
                Margin = new Thickness(Margin.Left + (p.X - MouseLastPosition.X), Margin.Top + (p.Y - MouseLastPosition.Y), Margin.Right, Margin.Bottom);
                ContainingVertex.Location = new Point(Margin.Left, Margin.Top);
            }
        }

        private void ConnectTo_Click(object sender, RoutedEventArgs e)
        {
            ConnectRequest.Invoke(this, ContainingVertex);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
