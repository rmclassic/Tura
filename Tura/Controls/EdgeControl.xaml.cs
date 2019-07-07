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
        TextBlock ConditionTextBlock = new TextBlock();
        Edge ContainingEdge;
        Path Path = new Path();
        Polygon Arrow = new Polygon();
        public event EventHandler<Edge> RemoveEdge;

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
            Point CenterPoint = new Point(EdgeLine.X1, EdgeLine.Y1 + 112);
            Path.Data = PathGeometry.Parse("M " + EdgeLine.X1 + "," + EdgeLine.Y1 + " c" + " 150,150 -150,150 0,0");
            double Offx = 5, Offy = 2;
            if (EdgeLine.X2 - EdgeLine.X1 < 0)
                Offx = -Offx;
            if (EdgeLine.Y1 - EdgeLine.Y2 < 0)
                Offy = -Offy;

            Arrow.Points.Clear();
            Arrow.Points.Add(CenterPoint);
            Arrow.Points.Add(new Point(CenterPoint.X - Offx, CenterPoint.Y + Offy));
            Arrow.Points.Add(new Point(CenterPoint.X - Offx, CenterPoint.Y - Offy));

            ConditionTextBlock.Margin = new Thickness(CenterPoint.X, CenterPoint.Y, 0, 0);
            ConditionTextBlock.Text = ContainingEdge.GetCondition.ToString();
            
        }

        private void DrawCurvedEdge()
        {
            Point CenterPoint = new Point((EdgeLine.X1 + EdgeLine.X2) / 2, (EdgeLine.Y1 + EdgeLine.Y2) / 2);
            
            double CurvOffx = 50, CurvOffy = 50;
            if (EdgeLine.X2 - EdgeLine.X1 < 0)
                CurvOffx = -CurvOffx;
            if (EdgeLine.Y1 - EdgeLine.Y2 < 0)
                CurvOffy = -CurvOffy;

            if (EdgeLine.X2 == EdgeLine.X1)
                CurvOffx = 0;
            if (EdgeLine.Y1 == EdgeLine.Y2)
                CurvOffy = 0;



            Point CurveCenterPoint = new Point((EdgeLine.X1 + EdgeLine.X2) / 2, (EdgeLine.Y1 + EdgeLine.Y2) / 2 + CurvOffy);
            Path.Data = PathGeometry.Parse("M " + EdgeLine.X1 + "," + EdgeLine.Y1 + " C " + EdgeLine.X1 + "," + EdgeLine.Y1 + " " + (int)(CenterPoint.X + CurvOffx) + "," + (int)(CenterPoint.Y + CurvOffy) + " " + EdgeLine.X2 + "," + EdgeLine.Y2);

            double Offx = 5, Offy = 2;
            if (EdgeLine.X2 - EdgeLine.X1 < 0)
                Offx = -Offx;
            if (EdgeLine.Y1 - EdgeLine.Y2 < 0)
                Offy = -Offy;

            Arrow.Points.Clear();
            Arrow.Points.Add(new Point(CurveCenterPoint.X - 10, CurveCenterPoint.Y - 10));
            Arrow.Points.Add(new Point(CurveCenterPoint.X - Offx - 10, CurveCenterPoint.Y + Offy - 10));
            Arrow.Points.Add(new Point(CurveCenterPoint.X - Offx - 10, CurveCenterPoint.Y - Offy - 10));
            Arrow.MouseLeftButtonDown += Arrow_MouseLeftButtonDown;
            ConditionTextBlock.Margin = new Thickness(CurveCenterPoint.X, CurveCenterPoint.Y,0,0);
            ConditionTextBlock.Text = ContainingEdge.GetCondition.ToString();
        }

        private void Arrow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            
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
            Arrow.Stroke = Brushes.Black;
            Arrow.StrokeThickness = 2;
            Arrow.Fill = Brushes.Black;
            
            ConfigureEdgeContextMenu();

            ControlGrid.Children.Add(ConditionTextBlock);
            ControlGrid.Children.Add(Arrow);
            ControlGrid.Children.Add(Path);
            UpdateVertexStats();
            if (ContainingEdge.IsSelfConnected())
                DrawSelfEdge();
            else
                DrawCurvedEdge();
        }

        void ConfigureEdgeContextMenu()
        {
            ContextMenu contextmenu = new ContextMenu();
            TextBox conditiontextbox = new TextBox() { Width = contextmenu.Width, Text = ContainingEdge.GetCondition.ToString(), MaxLength = 1 };
            MenuItem removeedgemenuitem = new MenuItem() { Header = "Remove edge" };
            removeedgemenuitem.Click += Removeedgemenuitem_Click;
            conditiontextbox.TextChanged += Conditiontextbox_TextChanged;
            contextmenu.Items.Add(conditiontextbox);
            contextmenu.Items.Add(removeedgemenuitem);
            

            Arrow.ContextMenu = contextmenu;
            Path.ContextMenu = contextmenu;

        }

        private void Removeedgemenuitem_Click(object sender, RoutedEventArgs e)
        {
            if (RemoveEdge != null)
                RemoveEdge.Invoke(this, ContainingEdge);
        }

        private void Conditiontextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t.Text != "")
            ContainingEdge.SetCondition(t.Text[0]);

            if (ContainingEdge.IsSelfConnected())
                DrawSelfEdge();
            else
                DrawCurvedEdge();
        }
    }
}
