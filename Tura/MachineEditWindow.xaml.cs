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
using System.Collections.Concurrent;
namespace Tura
{
    /// <summary>
    /// Interaction logic for MachineEditWindow.xaml
    /// </summary>
    public partial class MachineEditWindow : Window
    {
        bool ConnectionRequested = false;
        Vertex ConnectionRequestSource;
        DFAMachine ContainingMachine;
        ConcurrentQueue<string> NotificationsQueue = new ConcurrentQueue<string>();
        public MachineEditWindow(DFAMachine containingmachine)
        {

            InitializeComponent();
            ContainingMachine = containingmachine;
            Task.Run(NotificationChangeLoop);
            InvalidateMachineGraph();
        }

        void SetNotificationText(string text)
        {
            NotificationsQueue.Enqueue(text);
        }

        async void NotificationChangeLoop()
        {
            string currentnotif = "";
            while (true)
            {
                if (NotificationsQueue.TryDequeue(out currentnotif))
                {
                    NotifText.Dispatcher.Invoke(() =>
                    {
                        NotifText.Text = currentnotif;
                    });
                }
                else
                {
                    NotifText.Dispatcher.Invoke(() =>
                    {
                        NotifText.Text = "";
                    });
                }
                await Task.Delay(1000);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContainingMachine.Vertices.Add(new Vertex("V", new Point(20, 20)));
            InvalidateMachineGraph();
        }

        void InvalidateMachineGraph()
        {
            GraphGrid.Children.Clear();


            foreach (Edge<char> E in ContainingMachine.Edges)
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
            ContainingMachine.RemoveVertex(e);
            InvalidateMachineGraph();
        }


        private void EdgeControl_RemoveEdge(object sender, Edge<char> e)
        {
            ContainingMachine.Edges.Remove(e);
            InvalidateMachineGraph();
        }

        private void Vc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VertexControl control = sender as VertexControl;
            if (ConnectionRequested)
            {
                if (ContainingMachine.Edges.Contains(new Edge<char>(ConnectionRequestSource, control.ContainingVertex, 'c'), new EdgeComparer<char>()))
                {
                    MessageBox.Show("Edge is already added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ConnectionRequested = false;
                    return;
                }
                ContainingMachine.Edges.Add(new Edge<char>(ConnectionRequestSource, control.ContainingVertex, 'c'));
                ConnectionRequested = false;
                InvalidateMachineGraph();
            }

        }

        private void Vc_ConnectRequest(object sender, Vertex e)
        {
            ConnectionRequested = true;
            ConnectionRequestSource = e;
        }

        private void StartMachineButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text == "")
            {
                MessageBox.Show("No input is given to the machine.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DFAMachineBroker broker = new DFAMachineBroker(ContainingMachine);
            try
            {
                broker.InitializeMachine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            Vertex TempVertex = null;
            foreach (char c in InputTextBox.Text)
            {
                try
                {
                    TempVertex = broker.Step(c, TempVertex);
                    if (TempVertex == null)
                    {
                        SetNotificationText("Cursor stopped on condition " + c);
                        return;
                    }

                    SetNotificationText("Cursor Went to " + TempVertex.Name);
                }
                catch (Exception ex)
                {
                    SetNotificationText("Error: " + ex.Message);
                    return;
                }
            }
            if (!TempVertex.IsFinishState)
                SetNotificationText("INPUT NOT ACCEPTED");

            else
                SetNotificationText("INPUT ACCEPTED");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.DefaultExt = "dmf";
            dlg.Filter = "DFA machine file | *.dmf";
            try
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    ContainingMachine = FileDAL.LoadDFAMachine(dlg.FileName);

                InvalidateMachineGraph();
            }
            catch
            {
                System.Windows.MessageBox.Show("There was a problem loading the machine. machine file may be corrupted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem item = sender as System.Windows.Controls.MenuItem;

            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.DefaultExt = "dmf";
            dlg.Filter = "DFA machine file | *.dmf";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                FileDAL.SaveDFAMachine(ContainingMachine, dlg.FileName);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}