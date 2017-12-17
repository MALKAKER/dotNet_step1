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
        #region NANNY
        void addNanny(Nanny newNanny);
        Boolean removeNanny(String nannyId);
        void updateNanny(Nanny nannyToUpdate);
#endregion

        //Parent functions:
        #region PARENT
        void addParent(Parent newParent);
        Boolean removeParent(String parentId);
        void updateParent(Parent parentToUpdate);
        #endregion

        //child function
        #region CHILD 
        void addChild(Child newChild);
        Boolean removeChild(String childId);
        void updateChild(Child childToUpdate);
#endregion

        //contract function:
        #region CONTRACT
        void addContract(Contract newContract);
        Boolean removeContract(string contractId);
        void updateContract(Contract contractToUpdate);
        #endregion

        //lists of data
        #region GETT ALL
        List<Nanny> getAllNanny();
        List<Parent> getAllParents();
        //return all the children according to parent
        List<Child> getAllChildren();
        //get lists of parents and return their children
        List<Child> getAllChildren(List<Parent> parents); 
        List<Contract> getAllContracts();
        #endregion

        #region EXISTANCE
        //checks if the parent exist in the system
        bool ParentExist(String id);
        //checks if the Nanny exist in the system
        bool NannyExist(String id);
        //checks if the child exist in the system
        bool ChildExist(String id);
        //checks if the contract exist in the system
        bool ContractExist(String Contractid);
        #endregion

        #region RETRIEVE SPECIFIC OBJECT
        //checks if the parent Entity in the system
        Parent ParentEntity(String id);
        //checks if the Nanny Entity in the system
        Nanny NannyEntity(String id);
        //checks if the child Entity in the system
        Child ChildEntity(String id);
        //checks if the contract Entity in the system
        Contract ContractEntity(String Contractid);
        #endregion
    }
}
