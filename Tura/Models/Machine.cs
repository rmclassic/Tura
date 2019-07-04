using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.Models
{
    public abstract class Machine
    {
        public string Name;
        public List<Vertex> Vertices;
        public List<Edge> Edges;
        public MachineClass Class;
    }

    public enum MachineClass { DFA, Turing }
}
