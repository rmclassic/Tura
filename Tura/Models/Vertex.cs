using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Tura.Models
{
    public class Vertex
    {
        public event EventHandler VertexMoved;
        public Vertex(string name, Point location)
        {
            Rename(name);
            Location = location;
            IsFinishState = false;
        }

        public void Rename(string name)
        {
            Name = name;
        }
        public bool IsFinishState;
        public string Name;
        Point location;
        public Point Location { set { location = value; if (VertexMoved != null) VertexMoved.Invoke(this, null); } get { return location; } }
    }
}
