using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tura.Models
{
    public class ConditionList : List<char>
    {
        public override string ToString()
        {
            string St = "";

            foreach (char c in this)
            {
                St += c + ", ";
            }

            St = St.Remove(St.Length - 2, 2);
            return St;
        }
    }
}
