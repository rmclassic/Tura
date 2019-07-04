using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.Models
{
    public class Edge
    {
        public Edge(Vertex start, Vertex finish)
        {
            Source = start;
            Destination = finish;
        }
        public Vertex Source;
        public Vertex Destination;

    }
}
