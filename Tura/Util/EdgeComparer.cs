using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.Models;
namespace Tura.Util
{
    class EdgeComparer : IEqualityComparer<Edge>
    {
        public bool Equals(Edge x, Edge y)
        {
            return (x.Source == y.Source && x.Destination == y.Destination);
        }

        public int GetHashCode(Edge obj)
        {
            throw new NotImplementedException();
        }
    }
}
