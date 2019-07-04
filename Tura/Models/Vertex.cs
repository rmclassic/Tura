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
        public Vertex(string name, Point location)
        {
            Rename(name);
            Location = location;
        }

        public void Rename(string name)
        {
            Name = name;
        }
        public string Name;
        public Point Location;
    }
}
