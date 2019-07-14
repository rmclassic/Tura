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
        public event EventHandler InvokeActivation; //IMPLEMENT THIS
        public event EventHandler VertexMoved;
        public Vertex(string name, Point location)
        {
            
            Rename(name);
            Location = location;
            isstartstate = false;
            isfinishstate = false;
        }

        public void Rename(string name)
        {
            Name = name;
        }
        public bool IsFinishState { set { if (isstartstate) isstartstate = !isstartstate; isfinishstate = value; } get { return isfinishstate; } }
        public bool IsStartState { set { if (isfinishstate) isfinishstate = !isfinishstate; isstartstate = value; } get { return isstartstate; } }
        bool isstartstate;
        bool isfinishstate;

        public string Name;
        Point location;
        public Point Location { set { location = value; if (VertexMoved != null) VertexMoved.Invoke(this, null); } get { return location; } }
    }
}
