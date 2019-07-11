﻿using System;
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
        public TuringMachine(string name)
        {
            Name = name;
            Class = MachineClass.Turing;
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
            Class = MachineClass.Turing;
            Location = new Point(20, 20);
            Vertex Start = new Vertex("S", new Point(0, 0));
            Vertex Finish = new Vertex("F", new Point(20, 20));
            Finish.IsFinishState = true;
            Vertices.Add(Start);
            Vertices.Add(Finish);
        }
        public void PurgeOrphanEdges()
        {

        }

        public override void RemoveVertex(Vertex V)
        {

        }
    }


}
