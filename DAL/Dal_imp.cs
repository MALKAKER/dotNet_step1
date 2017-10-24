using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    using DS;
    class Dal_imp : Idal
    {
        //add

        //add child to the DS
        public bool addChild(Child newChild)
        {
            if (DataSource.childList.Exists(x => x.ID == newChild.ID))
                throw new Exception("Child already exists!\n");
            DataSource.childList.Add(newChild);
            return true;//!
        }

        //add contract to the DS
        public bool addContract(Contract newContract)
        {
            //if (DataSource.contractList.Exists(x => x. == newContract.ID))//////////////////////////////!
            //    throw new Exception("Child already exists!\n");
            //DataSource.contractList.Add(newContract);
            return true;//!
        }

        public bool addNanny(Nanny newNanny)
        {
            DataSource.nannyList.Add(newNanny);
            return true;//!
        }

        public bool addParent(Parent newParent)
        {
            DataSource.parentList.Add(newParent);
            return true;//!
        }

        //get


        public List<Child> getAllChildren()
        {
            return DataSource.childList;
        }

        public List<Child> getAllChildren(List<Parent> parents)
        {
            var children = (from parent in parents
                       from child in DataSource.childList
                       where child.parentId == parent.ID
                       select child).ToList<Child>();
            return children;
        }

        public List<Contract> getAllContracts()
        {
            return DataSource.contractList;
        }

        public List<Nanny> getAllNanny()
        {
            return DataSource.nannyList;
        }

        public List<Parent> getAllParents()
        {
            return DataSource.parentList;
        }

        //remove 

        public bool removeChild(string childId)
        {
            return DataSource.childList.Remove(DataSource.childList.Find(x => x.ID == childId));
        }

        public bool removeContract(int contractId)
        {
            return DataSource.contractList.Remove(DataSource.contractList.Find(x => x.contractId == contractId));
        }

        public bool removeNanny(string nannyId)
        {
            return DataSource.nannyList.Remove(DataSource.nannyList.Find(x => x.ID == nannyId));
        }

        public bool removeParent(string parentId)
        {
            return DataSource.parentList.Remove(DataSource.parentList.Find(x => x.ID == parentId));
        }

        //update
        public bool updateChild(Child childToUpdate)
        {
            DataSource.childList.Remove(DataSource.childList.Find(x => x.ID ==childToUpdate.ID));
            DataSource.childList.Add(childToUpdate);
            return true; //?
        }

        public bool updateContract(Contract contractToUpdate)
        {
            DataSource.contractList.Remove(DataSource.contractList.Find(x => x.contractId == contractToUpdate.contractId));
            DataSource.contractList.Add(contractToUpdate);
            return true;//!
        }

        public bool updateNanny(Nanny nannyToUpdate)
        {
            DataSource.nannyList.Remove(DataSource.nannyList.Find(x => x.ID == nannyToUpdate.ID));
            DataSource.nannyList.Add(nannyToUpdate);
            return true;//!
        }

        public bool updateParent(Parent parentToUpdate)
        {
            DataSource.parentList.Remove(DataSource.parentList.Find(x => x.ID == parentToUpdate.ID));
            DataSource.parentList.Add(parentToUpdate);
            return true;//! to ask!!!!!! if we need the bool
        }
    }
}
