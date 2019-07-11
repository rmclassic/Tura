using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.Models;
namespace Tura.Util
{
    class DFAMachineDebugger
    {
        Machine ContainingMachine;
        public DFAMachineDebugger(Machine m)
        {
            ContainingMachine = m;
        }

        public string GetFirstError()
        {
            string error = "";
            error = DebugEdgeInterference();
            if (error != "")
                return error;

            else
                return "";
        }

        string DebugEdgeInterference()
        {
            foreach (Edge e in ContainingMachine.Edges)
            {
                foreach (Edge E in ContainingMachine.Edges)
                {
                    if (AreEdgesInterfering(e, E))
                        return "Two edges from " + e.Source.Name + " are Interfering";
                }
            }
            return "";
        }

        bool AreEdgesInterfering(Edge e1, Edge e2)
        {
            if (e1 != e2 && e1.Source == e2.Source)
            {
                foreach (char c in e1.GetConditions)
                {
                    foreach (char C in e2.GetConditions)
                    {
                        if (c == C)
                            return true;
                    }
                }
                return false;
            }
            else
                return false;
        }
    }
}
