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
namespace Tura
{
    /// <summary>
    /// Interaction logic for MachineControl.xaml
    /// </summary>
    public partial class MachineControl : UserControl
    {
        public Machine ContainingMachine;
        bool Dragging = false;
        Point MouseLastPosition;
        public MachineControl(Machine containingmachine)
        {
            ContainingMachine = containingmachine;
            MouseDoubleClick += MachineControl_MouseDoubleClick;
            MouseDown += MachineControl_MouseDown;
            MouseMove += MachineControl_MouseMove;
            MouseUp += MachineControl_MouseUp;
            InitializeComponent();
        }

        private void MachineControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Dragging = false;
        }

        private void MachineControl_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (Dragging)
            {
                Point p = Mouse.GetPosition(this);
                Margin = new Thickness(Margin.Left + (p.X - MouseLastPosition.X), Margin.Top + (p.Y - MouseLastPosition.Y), Margin.Right, Margin.Bottom);
               // RenderTransform = new TranslateTransform(p.X, p.Y);
            }
            
        }

        private void MachineControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseLastPosition = Mouse.GetPosition(this);
            Dragging = true;
        }

        private void MachineControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
