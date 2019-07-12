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
using Tura.Controls;

namespace Tura
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TuringMachine> TuringMachines = new List<TuringMachine>();
        private List<DFAMachine> DFAMachines = new List<DFAMachine>();

        public MainWindow()
        {
            InitializeComponent();
            DFAMachines.Add(new DFAMachine("New DFA Machine"));
            InvalidateMachinesGrid();
        }

        public void InvalidateMachinesGrid()
        {
            MachinesGrid.Children.Clear();
            foreach (DFAMachine machine in DFAMachines)
            {
           
                MachinesGrid.Children.Add(new MachineControl(machine));
            }

            foreach (TuringMachine machine in TuringMachines)
            {

                MachinesGrid.Children.Add(new TuringMachineControl(machine));
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;

            if (item.Header.ToString() == "DFA Machine")
                DFAMachines.Add(new DFAMachine("New DFA Machine"));
            else if (item.Header.ToString() == "Turing Machine")
                TuringMachines.Add(new TuringMachine("New Turing Machine"));

            InvalidateMachinesGrid();
        }
    }
}
