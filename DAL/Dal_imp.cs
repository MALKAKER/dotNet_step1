using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
namespace DAL
{
    
    public class Dal_imp : Idal
    {
        #region Contract ID Constans
        //to use for IDs:
        private static List<String> recycledIDs = new List<String>();
        //8 digits serial number
        //variable that change every new variable
        private static int lastUsedID = 0;////////////change to store value and restore at beginning of run.
        #endregion

        #region Add Functions
        //add

        //add child to the DS
        public void addChild(Child newChild)
        {
            if (DataSource.childList.Exists(x => x.ID == newChild.ID))
                throw new Exception("Child already exists!\n");
            DataSource.childList.Add(newChild);
            
        }
        
        //add contract to the DS
        public void addContract(Contract newContract)
        {
            //If there are no recycled IDs, assign the next number up
            if (recycledIDs.Count() == 0)//no recycled IDs
            {
                //if all possible 8-digit codes are already in use, throw exception.
                if (lastUsedID.ToString().Length > 8)
                {
                    throw new Exception("No available IDs.\nCannot create a new contract.\n");
                }
                ++lastUsedID;
                newContract.contractID = String.Format("{0:00000000}", lastUsedID);
            }
            //There is a recycled ID to reuse, so use it and then remove it.
            else
            {
                newContract.contractID = String.Format("{0:00000000}", recycledIDs[0]);
                recycledIDs.RemoveAt(0);
            }
            //If a contract with this ID already exists, throw exception.
            //////////////////////Just in case? sort out.....
            if (DataSource.contractList.Exists(x => x.contractID == newContract.contractID))
                throw new Exception("Contract already exists!\n");
            DataSource.contractList.Add(newContract);
            
        }

        public void addNanny(Nanny newNanny)
        {
            DataSource.nannyList.Add(newNanny);
            
        }

        public void addParent(Parent newParent)
        {
            DataSource.parentList.Add(newParent);
        }
        #endregion

        #region Get All Function
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
        #endregion

        #region Remove Function
        //remove 

        public bool removeChild(string childId)
        {
            return DataSource.childList.Remove(DataSource.childList.Find(x => x.ID == childId));
        }

        public bool removeContract(int contractId)
        {
            return DataSource.contractList.Remove(DataSource.contractList.Find(x => x.contractID == contractId.ToString()));////need to initalize the contract id
        }

        public bool removeNanny(string nannyId)
        {
            return DataSource.nannyList.Remove(DataSource.nannyList.Find(x => x.ID == nannyId));
        }

        public bool removeParent(string parentId)
        {
            return DataSource.parentList.Remove(DataSource.parentList.Find(x => x.ID == parentId));
        }
        #endregion

        #region Update Functions
        //update
        public void updateChild(Child childToUpdate)
        {
            DataSource.childList.Remove(DataSource.childList.Find(x => x.ID ==childToUpdate.ID));
            DataSource.childList.Add(childToUpdate);
        }
        public void updateContract(Contract contractToUpdate)
        {
            DataSource.contractList.Remove(DataSource.contractList.Find(x => x.contractID == contractToUpdate.contractID));
            DataSource.contractList.Add(contractToUpdate);    
        }

        public void updateNanny(Nanny nannyToUpdate)
        {
            DataSource.nannyList.Remove(DataSource.nannyList.Find(x => x.ID == nannyToUpdate.ID));
            DataSource.nannyList.Add(nannyToUpdate);
        }

        public void updateParent(Parent parentToUpdate)
        {
            DataSource.parentList.Remove(DataSource.parentList.Find(x => x.ID == parentToUpdate.ID));
            DataSource.parentList.Add(parentToUpdate);
        }
        #endregion

        #region Exsistance Functions
        //exist
        //checks if the parent exist in the system
        public bool ParentExist(String id)
        {
            return DataSource.parentList.Exists(x => x.ID == id);
        }
        //checks if the Nanny exist in the system
        public bool NannyExist(String id)
        {
            return DataSource.nannyList.Exists(x => x.ID == id);
        }
        //checks if the Nanny exist in the system
        public bool ChildExist(String id)
        {
            return DataSource.childList.Exists(x => x.ID == id);
        }
        //checks if the Contract exist in the system
        
        public bool ContractExist(string Contractid)
        {
            return DataSource.contractList.Exists(x => x.contractID == Contractid);
        }
        #endregion

        #region Get Entity Functions
        //returns the parent Entity in the system
        public Parent ParentEntity(String id)
        {
            return DataSource.parentList.First(x => x.ID ==id);
        }
        //returns the Nanny Entity in the system
        public Nanny NannyEntity(String id)
        {
            return DataSource.nannyList.First(x => x.ID == id);
        }
        //returns the child Entity in the system
        public Child ChildEntity(String id)
        {
            return DataSource.childList.First(x => x.ID == id);
        }
        //returns the contract Entity in the system
        public Contract ContractEntity(String Contractid)
        {
            return DataSource.contractList.First(x => x.contractID == Contractid);
        }
        #endregion
    }
}
