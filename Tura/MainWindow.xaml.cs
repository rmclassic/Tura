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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Machine> Machines = new List<Machine>();


        public MainWindow()
        {
            InitializeComponent();
            Machines.Add(new DFAMachine("New DFA Machine"));
            InvalidateMachinesGrid();
        }

        public void InvalidateMachinesGrid()
        {
            MachinesGrid.Children.Clear();
            foreach (Machine machine in Machines)
            {
           
                MachinesGrid.Children.Add(new MachineControl(machine));
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;

            if (item.Header.ToString() == "DFA Machine")
                Machines.Add(new DFAMachine("New DFA Machine"));

            InvalidateMachinesGrid();
        }
    }
}
