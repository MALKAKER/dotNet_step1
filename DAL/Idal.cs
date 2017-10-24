using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    // manage the nanny software
    interface Idal
    {
        //Nanny functions:
        // ask the lecturer about the blah
        Boolean addNanny(Nanny newNanny);
        Boolean removeNanny(String nannyId);
        Boolean updateNanny(Nanny nannyToUpdate);


        //Parent functions:
        Boolean addParent(Parent newParent);
        Boolean removeParent(String parentId);
        Boolean updateParent(Parent parentToUpdate);

        //child function
        Boolean addChild(Child newChild);
        Boolean removeChild(String childId);
        Boolean updateChild(Child childToUpdate);

        //contract function:
        Boolean addContract(Contract newContract);
        Boolean removeContract(int contractId);
        Boolean updateContract(Contract contractToUpdate);

        //lists of data

        List<Nanny> getAllNanny();
        //
        List<Parent> getAllParents();
        //________________________________
        //return all the children according to parent
        List<Child> getAllChildren();
        //get lists of parents and return their children
        List<Child> getAllChildren(List<Parent> parents); 
        //________________________________
        List<Contract> getAllContracts();
        //list of banck branches
        //List<Nanny> getAllNanny(); todo
    }
}
