using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    // manage the nanny software
    public interface Idal
    {
        //Nanny functions:
        // ask the lecturer about the blah
        void addNanny(Nanny newNanny);
        Boolean removeNanny(String nannyId);
        void updateNanny(Nanny nannyToUpdate);


        //Parent functions:
        void addParent(Parent newParent);
        Boolean removeParent(String parentId);
        void updateParent(Parent parentToUpdate);

        //child function
        void addChild(Child newChild);
        Boolean removeChild(String childId);
        void updateChild(Child childToUpdate);

        //contract function:
        void addContract(Contract newContract);
        Boolean removeContract(int contractId);
        void updateContract(Contract contractToUpdate);

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
        //return
        //checks if the parent exist in the system
        bool ParentExist(String id);
        //checks if the Nanny exist in the system
        bool NannyExist(String id);
        //checks if the child exist in the system
        bool ChildExist(String id);
    }
}
