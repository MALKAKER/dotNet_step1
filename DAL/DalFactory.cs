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
        private static readonly DalFactory instance = new DalFactory();

        public static DalFactory Instance
        {
            get { return DalFactory.instance; }
        }

        static DalFactory() { }
        //private DalFactory() { }

        public Idal getReference()
        {
            return new Dal_imp();//here we change after using xml
        }
    }
}
