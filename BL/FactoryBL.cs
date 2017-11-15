using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public sealed class FactoryBL
    {
        private static readonly FactoryBL instance = new FactoryBL();

        public static FactoryBL Instance
        {
            get { return FactoryBL.instance; }
        }

        static FactoryBL() { }
        private FactoryBL() { }

        public IBL getReference()
        {
            return new BL_imp();
        }
    }
}
