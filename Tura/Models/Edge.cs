using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.Models
{
    public class Edge
    {

        public Edge(Vertex start, Vertex finish, char condition)
        {
            conditiongate = condition;
            Source = start;
            Destination = finish;
            if (Source == Destination)
                SelfConnectedVertex = true;
            else SelfConnectedVertex = false;
        }

        public bool IsSelfConnected()
        {
            return SelfConnectedVertex;
        }
        bool SelfConnectedVertex;
        Vertex source;
        Vertex destination;
        char conditiongate;
        public char GetCondition { get { return conditiongate; } }
        public Vertex Source { set { source = value; SelfConnectedVertex = (source == destination); } get { return source; } }
        public Vertex Destination { set { destination = value; SelfConnectedVertex = (source == destination); } get { return destination; } }

    }
}
