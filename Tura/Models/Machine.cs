using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tura.Models
{
    public abstract class Machine
    {
        public string Name;
        public Point Location;
        public List<Vertex> Vertices;
        public MachineClass Class;
        public abstract void RemoveVertex(Vertex V);
        public abstract string Run(string input);
        public abstract void ReconstructRefs();
    }



    public enum MachineClass { DFA, Turing }
}
