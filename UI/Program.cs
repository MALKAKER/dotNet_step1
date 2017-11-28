using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL mybl = FactoryBL.Instance;
            mybl.specificContracts(c => c.ChildId == "5");
            mybl.specificContracts(c => c.ChildId == "5");
        }
    }
}
