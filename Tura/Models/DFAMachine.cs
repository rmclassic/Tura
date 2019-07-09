using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Tura.Models
{
    class DFAMachine : Machine
    {
        public DFAMachine(string name)
        {
            Name = name;
            Class = MachineClass.DFA;
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
            Location = new Point(20, 20);
            Vertex Start = new Vertex("S", new Point(0, 0));
            Vertex Finish = new Vertex("F", new Point(20, 20));
            Vertices.Add(Start);
            Vertices.Add(Finish);
        }
    }
}
