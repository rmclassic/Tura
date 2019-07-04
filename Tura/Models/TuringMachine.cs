using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using Tura.Models;

namespace Tura.Models
{
    public class TuringMachine : Machine
    {
        public TuringMachine()
        {
            Class = MachineClass.Turing;
            Vertex Start = new Vertex("S", new Point(0, 0));
            Vertex Finish = new Vertex("F", new Point(20, 20));
            Vertices.Add(Start);
            Vertices.Add(Finish);
            Edges.Add(new Edge(Start, Finish));
        }
    }


}
