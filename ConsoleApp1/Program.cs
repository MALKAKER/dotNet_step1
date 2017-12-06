using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IBL mybl = FactoryBL.Instance;
                Address adress = new Address
                {
                    house = "22a",
                    city = "Lud",
                    country = "Israel",
                    addressLine2 = "what is it??",
                    flat = "7",
                    floor = "-3.5",
                    postCode = "12345",
                    street = "vered"
                };
         //       Address adress1 = new Address("Israel", "Lud", "12345", "vered", "22a", "what is it??", "7", "-3.5");
                Console.WriteLine(adress.ToString());
                mybl.specificContracts(c => c.ChildId == "5");
                mybl.specificContracts(c => c.ChildId == "5");
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "/n");
            }
            Console.ReadKey();
        }
    }
}
