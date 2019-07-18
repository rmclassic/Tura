using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tura.Util;

namespace Tura.Models
{
    public class DFAMachine : Machine
    {
        public int foo = 15;
        public List<Edge<char>> Edges;

        public DFAMachine()
        {
            Class = MachineClass.DFA;
            Vertices = new List<Vertex>();
            Edges = new List<Edge<char>>();
            Location = new Point(20, 20);
        }

        public DFAMachine(string name)
        {
            Name = name;
            Class = MachineClass.DFA;
            Vertices = new List<Vertex>();
            Edges = new List<Edge<char>>();
            Location = new Point(20, 20);
            Vertex Start = new Vertex("S", new Point(0, 0));
            Vertex Finish = new Vertex("F", new Point(20, 20));
            Start.IsStartState = true;
            Finish.IsFinishState = true;
            Vertices.Add(Start);
            Vertices.Add(Finish);
        }

     

        public override string Run(string input)
        {
            DFAMachineBroker broker = new DFAMachineBroker(this);
            broker.InitializeMachine();
            return broker.Run(input);
        }

        public override void RemoveVertex(Vertex V)
        {
            Vertices.Remove(V);
            PurgeOrphanEdges();
        }

        bool InterferingEdgeExist(Vertex source, char condition)
        {
            foreach (Edge<char> e in Edges)
            {
                if (e.Source == source && (e.GetConditions).Contains(condition))
                    return true;
            }
            return false;
        }

        void PurgeOrphanEdges()
        {
            for (int i = 0; i < Edges.Count; i++)
            {
                if (!Vertices.Contains(Edges[i].Source) || !Vertices.Contains(Edges[i].Destination))
                {
                    Edges.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
