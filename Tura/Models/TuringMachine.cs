using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tura.Models;
using Tura.Util;

namespace Tura.Models
{
    public class TuringMachine : Machine
    {
        public List<Edge<TuringCondition>> Edges;
        public TuringMachine(string name)
        {
            Name = name;
            Class = MachineClass.Turing;
            Vertices = new List<Vertex>();
            Edges = new List<Edge<TuringCondition>>();
            Location = new Point(20, 20);
            Vertex Start = new Vertex("S", new Point(0, 0));
            Vertex Finish = new Vertex("F", new Point(20, 20));
            Finish.IsFinishState = true;
            Start.IsStartState = true;
            Vertices.Add(Start);
            Vertices.Add(Finish);
        }
        public void PurgeOrphanEdges()
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

        public override string Run(string input)
        {
            TuringMachineBroker broker = new TuringMachineBroker(this);
            broker.InitializeMachine();
            return broker.Run(input);
        }

        public override void RemoveVertex(Vertex V)
        {
            Vertices.Remove(V);
            PurgeOrphanEdges();
        }
    }


}
