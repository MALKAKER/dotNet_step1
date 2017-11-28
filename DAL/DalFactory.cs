using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public sealed class DalFactory
    {
        private static Idal dal = null;
 
        private DalFactory() { }

        public  static Idal getReference()
        {
            if(dal == null)
            {
                dal = new Dal_imp();//here we change after using xml
            }

            return dal;
        }
    }
}
