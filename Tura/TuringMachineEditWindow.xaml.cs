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
using System.Collections.Concurrent;
using Tura.Util;

namespace Tura
{
    /// <summary>
    /// Interaction logic for TuringMachineEditWindow.xaml
    /// </summary>
    public partial class TuringMachineEditWindow : Window
    {
        TuringMachineTapeControl TapeControl;
        TuringMachine ContainingMachine;
        bool ConnectionRequested = false;
        Vertex ConnectionRequestSource;
        public TuringMachineEditWindow(TuringMachine containingmachine)
        {
            InitializeComponent();
            ContainingMachine = containingmachine;
            
            InvalidateMachineGraph();

            TapeControl = new TuringMachineTapeControl("") { Height = 100 };

            WindowGrid.Children.Add(TapeControl);
            TapeControl.ScrollToItem(0);

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContainingMachine.Vertices.Add(new Vertex("V", new Point(20, 20)));
            InvalidateMachineGraph();
        }

        void InvalidateMachineGraph()
        {
            GraphGrid.Children.Clear();


            foreach (Edge<TuringCondition> E in ContainingMachine.Edges)
            {
                TuringEdgeControl EC = new TuringEdgeControl(E);
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
            ContainingMachine.RemoveVertex(e);
            InvalidateMachineGraph();
        }


        private void EdgeControl_RemoveEdge(object sender, Edge<TuringCondition> e)
        {
            ContainingMachine.Edges.Remove(e);
            InvalidateMachineGraph();
        }

        private void Vc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VertexControl control = sender as VertexControl;
            if (ConnectionRequested)
            {
                if (ContainingMachine.Edges.Contains(new Edge<TuringCondition>(ConnectionRequestSource, control.ContainingVertex, null), new EdgeComparer<TuringCondition>()))
                {
                    MessageBox.Show("Edge is already added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ConnectionRequested = false;
                    return;
                }
                ContainingMachine.Edges.Add(new Edge<TuringCondition>(ConnectionRequestSource, control.ContainingVertex, new TuringCondition()));
                ConnectionRequested = false;
                InvalidateMachineGraph();
            }

        }

        private void Vc_ConnectRequest(object sender, Vertex e)
        {
            ConnectionRequested = true;
            ConnectionRequestSource = e;
        }

        private async void StartMachineButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            int cursor = 0;
            if (InputTextBox.Text == "")
            {
                MessageBox.Show("No input is given to the machine.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            TuringMachineBroker broker = new TuringMachineBroker(ContainingMachine);
            try
            {
                broker.InitializeMachine();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            TuringBrokerStepResult stepresult = new TuringBrokerStepResult(null, '\0', Transition.Right);

            do
            {
                try
                {
                    await TapeControl.ScrollToItem(cursor);
                    stepresult = broker.Step(input[cursor], stepresult.Destination);
                    if (stepresult == null)
                    {
                        return;
                    }

                    await Task.Delay(1000);

                    input = input.Remove(cursor, 1);
                    input = input.Insert(cursor, stepresult.ReplaceBy.ToString());
                    TapeControl.ChangeInput(input);

                    if (stepresult.To == Transition.Right)
                        cursor++;
                    else
                        cursor--;




                }
                catch (Exception ex)
                {
                    //SetNotificationText("Error: " + ex.Message);
                    return;
                }
            } while (stepresult.Destination != null);
            //if (!stepresult.Destination.IsFinishState)

            //    //SetNotificationText("INPUT NOT ACCEPTED");

            //else
            //   // SetNotificationText("INPUT ACCEPTED");
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TapeControl.ChangeInput(InputTextBox.Text);
        }
    }
}
