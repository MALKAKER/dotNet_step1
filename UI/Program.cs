using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL mybl = FactoryBL.Instance;
            Address adreess = new Address("Israel", "Lud", "12345", "vered", "22a", "what is it??","7","-3.5");
            Console.WriteLine(adreess.ToString());
            mybl.specificContracts(c => c.ChildId == "5");
            mybl.specificContracts(c => c.ChildId == "5");
        }
    }
}
