using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tura.Models;
namespace Tura.Util
{
    class TuringMachineBroker
    {
        TuringMachine ContainingMachine;
        bool Initialized = true;

        public TuringMachineBroker(TuringMachine machine)
        {
            ContainingMachine = machine;
        }

        public void InitializeMachine()
        {
            //DFAMachineDebugger dbg = new DFAMachineDebugger(ContainingMachine);
            //string error = dbg.GetFirstError();
            //if (error != "")
            //    throw new InvalidOperationException(error);

            //Initialized = true;
        }

        public Vertex GetStartState()
        {
            foreach (Vertex v in ContainingMachine.Vertices)
            {
                if (v.IsStartState)
                    return v;
            }
            return null;
        }

        public TuringBrokerStepResult Step(char condition, Vertex sourcevertex)
        {
            if (!Initialized)
                throw new InvalidOperationException("Broker should be initialized first");

            if (sourcevertex == null)
                return new TuringBrokerStepResult(GetStartState(), condition, Transition.None);

            foreach (Edge<TuringCondition> e in ContainingMachine.Edges)
            {
                TuringBrokerStepResult ConditionResult = ConditionCheckOutput(e, condition);
                if (e.Source == sourcevertex && (ConditionResult.Destination != null))
                {
                    return ConditionResult;
                }
            }
            return null;
        }


        public string Run(string input)
        {

            int cursor = 0;

            TuringMachineBroker broker = new TuringMachineBroker(ContainingMachine);

            TuringBrokerStepResult stepresult = new TuringBrokerStepResult(null, '\0', Transition.Right);

            do
            {
                try
                {
                    stepresult = broker.Step(input[cursor], stepresult.Destination);
                    if (stepresult == null)
                    {
                        return input;
                    }

                    input = input.Remove(cursor, 1);
                    input = input.Insert(cursor, stepresult.ReplaceBy.ToString());

                    if (stepresult.To == Transition.Right)
                    {
                        cursor++;
                        if (cursor >= input.Length)
                            input = input + "#";
                    }
                    else
                    {
                        cursor--;
                        if (cursor < 0)
                        {
                            input = "#" + input;
                            cursor = 0;
                        }
                    }



                }
                catch (Exception ex)
                {
                    //SetNotificationText("Error: " + ex.Message);
                    return "ERROR";
                }
            } while (stepresult.Destination != null);

            input = input.TrimEnd('#');
            return input;
        }

        TuringBrokerStepResult ConditionCheckOutput(Edge<TuringCondition> e, char val)
        {
            foreach (TuringCondition c in e.GetConditions)
            {
                if (c.Condition == val)
                    return new TuringBrokerStepResult(e.Destination, c.ReplaceBy, c.To);
            }
            return new TuringBrokerStepResult(null, '\0', Transition.Right);
        }
    }
}
