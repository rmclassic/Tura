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
    /// Interaction logic for EdgeControl.xaml
    /// </summary>
    public partial class EdgeControl : UserControl
    {
        Line EdgeLine;
        Edge ContainingEdge;
        Path Path = new Path();
        public EdgeControl(Edge containingedge)
        {
            InitializeComponent();
            ContainingEdge = containingedge;
            InitializeEdge();
            ContainingEdge.Destination.VertexMoved += Destination_VertexMoved;
            ContainingEdge.Source.VertexMoved += Source_VertexMoved;
        }

        private void Destination_VertexMoved(object sender, EventArgs e)
        {

            UpdateVertexStats();
            if (ContainingEdge.IsSelfConnected())
                DrawSelfEdge();
            else
                DrawCurvedEdge();

        }

        private void Source_VertexMoved(object sender, EventArgs e)
        {

            UpdateVertexStats();

            if (ContainingEdge.IsSelfConnected())
                DrawSelfEdge();
            else
                DrawCurvedEdge();

        }

        private void DrawSelfEdge()
        {
            Path.Data = PathGeometry.Parse("M " + EdgeLine.X1 + "," + EdgeLine.Y1 + " c" + " 150,150 -150,150 0,0");


        }

        private void DrawCurvedEdge()
        {
            Point p = new Point((EdgeLine.X1 + EdgeLine.X2) / 2, (EdgeLine.Y1 + EdgeLine.Y2) / 2);
            double CurvOffx = 50, CurvOffy = 50;
            if (EdgeLine.X2 - EdgeLine.X1 < 0)
                CurvOffx = -CurvOffx;
            if (EdgeLine.Y1 - EdgeLine.Y2 < 0)
                CurvOffy = -CurvOffy;

            Path.Data = PathGeometry.Parse("M " + EdgeLine.X1 + "," + EdgeLine.Y1 + " C " + EdgeLine.X1 + "," + EdgeLine.Y1 + " " + (int)(p.X + CurvOffx) + "," + (int)(p.Y + CurvOffy) + " " + EdgeLine.X2 + "," + EdgeLine.Y2);
        }

        private void UpdateVertexStats()
        {
            EdgeLine.X1 = ContainingEdge.Source.Location.X + 50;
            EdgeLine.Y1 = ContainingEdge.Source.Location.Y + 50;
            EdgeLine.X2 = ContainingEdge.Destination.Location.X + 50;
            EdgeLine.Y2 = ContainingEdge.Destination.Location.Y + 50;
        }

        private void InitializeEdge()
        {
            EdgeLine = new Line();
            Path.Stroke = System.Windows.Media.Brushes.Black;
            Path.StrokeThickness = 2;
            Path.HorizontalAlignment = HorizontalAlignment.Left;
            Path.VerticalAlignment = VerticalAlignment.Top;

            Point p = new Point((EdgeLine.X1 + EdgeLine.X2) / 2, (EdgeLine.Y1 + EdgeLine.Y2) / 2);
            double CurvOffx, CurvOffy;

            CurvOffx = EdgeLine.X2 - EdgeLine.X1;
            CurvOffy = EdgeLine.Y1 - EdgeLine.Y2;

            Path.Data = PathGeometry.Parse("M " + EdgeLine.X1 + "," + EdgeLine.Y1 + " C " + EdgeLine.X1 + "," + EdgeLine.Y1 + " " + (int)(p.X + CurvOffx) + "," + (int)(p.Y + CurvOffy) + " " + EdgeLine.X2 + "," + EdgeLine.Y2);

            ControlGrid.Children.Add(Path);
            EdgeLine.HorizontalAlignment = HorizontalAlignment.Left;
            EdgeLine.VerticalAlignment = VerticalAlignment.Top;
            
            
            
            
            EdgeLine.Stroke = Brushes.Black;
            EdgeLine.StrokeThickness = 2;
            //ControlGrid.Children.Add(EdgeLine);
            
        }
    }
}
