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
            conditiongate = new ConditionList();
            conditiongate.Add(condition);
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

        public bool SetCondition(char c)
        {
            conditiongate.Add(c);

            return true;
        }

        public void ClearConditions()
        {
            conditiongate.Clear();
        }

        bool SelfConnectedVertex;
        Vertex source;
        Vertex destination;
        ConditionList conditiongate;
        public ConditionList GetConditions { get { return conditiongate; } }
        public Vertex Source { set { source = value; SelfConnectedVertex = (source == destination); } get { return source; } }
        public Vertex Destination { set { destination = value; SelfConnectedVertex = (source == destination); } get { return destination; } }

    }
}
