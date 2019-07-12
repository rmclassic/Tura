using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tura.Models
{
    public abstract class Machine<T>
    {
        public string Name;
        public Point Location;
        public List<Vertex> Vertices;
        public List<Edge<T>> Edges;
        public MachineClass Class;
        public abstract void RemoveVertex(Vertex V);
    }

    public enum MachineClass { DFA, Turing }
}
