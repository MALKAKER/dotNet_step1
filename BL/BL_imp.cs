using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    class BL_imp : IBL
    {
        //defines a component of dal layer
        DAL.Idal dal;
        public void Cass_BlAdapter()
        {
            DAL.DalFactory factory = new DAL.DalFactory();
            dal = factory.getReference();
        }


        public void addChild(Child newChild)
        {
            if (!dal.ChildExist(newChild.ID))
            {
                dal.addChild(newChild);
            }
            else
            {
                throw new Exception("ERROR: Child is already exist!\n");
            }
        }

        public void addContract(Contract newContract)
        {
            TimeSpan age = DateTime.Now - ((dal.getAllChildren()).FirstOrDefault(x => x.ID == newContract.ChildId)).dateOfBirth;
            if (((dal.getAllContracts()).FindAll(x => x.NannyId == newContract.NannyId)).Count < ((dal.getAllNanny()).Find(x => x.ID == newContract.NannyId)).maxChildren)
            {
                throw new Exception("ERROR: Nanny is busy!\n");
            }
            else if(dal.NannyExist(newContract.NannyId) && dal.ParentExist(newContract.ParentId))
            {
                dal.addContract(newContract);
            }
            else if (Convert.ToDateTime(age).Year == 0 && Convert.ToDateTime(age).Month <= 3)
            {
                throw new Exception("ERROR: Babies under 3 months can't register to nanny.\n");
            } 
            else
            {
                throw new Exception("ERROR: One or more stakeholder don't exist in the data base!\n");
            }
            
            
        }

        public void addNanny(Nanny newNanny)
        {

            TimeSpan age = DateTime.Now - newNanny.dateOfBirth;
            if (!dal.NannyExist(newNanny.ID))
            {
                dal.addNanny(newNanny);
            }
            else
            {
                throw new Exception("ERROR: Nanny is already exit!\n");
            }
            switch (newNanny.workField)
            {
                case Specialization.Nanny:
                    if ( (Convert.ToDateTime(age)).Year < 18)
                    {
                        throw new Exception("You are too young to be a nanny.\n");
                    }
                    break;
                case Specialization.Babysitter:
                    if ((Convert.ToDateTime(age)).Year < 14)
                    {
                        throw new Exception("You are too young to be a babysitter.\n");
                    }
                    break;
                default:
                    break;
            }

            dal.addNanny(newNanny);     
        }

        public void addParent(Parent newParent)
        {
            if (!dal.ParentExist(newParent.ID))
            {
                dal.addParent(newParent);
            }
            else
            {
                throw new Exception("ERROR: Parent is already exists!\n");
            }
        }

        public List<Nanny> betterMatchNanny(Address area)
        {
            throw new NotImplementedException();
        }

        public List<Contract> contractDistance(bool isSort = false, delegateSort someSort = null)
        {
            throw new NotImplementedException();
        }

        public List<Child> getAllChildren()
        {
            throw new NotImplementedException();
        }

        public List<Child> getAllChildren(List<Parent> parents)
        {
            throw new NotImplementedException();
        }

        public List<Contract> getAllContracts()
        {
            throw new NotImplementedException();
        }

        public List<Nanny> getAllNanny()
        {
            throw new NotImplementedException();
        }

        public List<Parent> getAllParents()
        {
            throw new NotImplementedException();
        }

        public List<Nanny> matchNanny(Parent parent)
        {
            throw new NotImplementedException();
        }

        public List<Nanny> matchNanny(Address area)
        {
            throw new NotImplementedException();
        }

        public List<Nanny> nannyAddress(bool isSort = false, delegateSort someSort = null)
        {
            if (someSort == null)
            {
                someSort = sortByName;
            }
            return null;
            //return from nanny in dal.getAllNanny()
            //       orderby nanny.lastName
            //       group nanny by nanny.personAddress.city; //?
                   ;
        }

        public List<Nanny> nannyAge(bool isSort = false, delegateSort someSort = null)
        {
            throw new NotImplementedException();
        }

        public List<Child> noNanny()
        {
            throw new NotImplementedException();
        }

        public int numberOfContracts(delegateCondition condition)
        {
            throw new NotImplementedException();
        }

        public bool removeChild(string childId)
        {
            throw new NotImplementedException();
        }

        public bool removeContract(int contractId)
        {
            throw new NotImplementedException();
        }

        public bool removeNanny(string nannyId)
        {
            throw new NotImplementedException();
        }

        public bool removeParent(string parentId)
        {
            throw new NotImplementedException();
        }

        public List<Contract> specificContracts(delegateCondition condition)
        {
            throw new NotImplementedException();
        }

        public List<Nanny> tamatVacation()
        {
            throw new NotImplementedException();
        }

        public void updateChild(Child childToUpdate)
        {
            throw new NotImplementedException();
        }

        public void updateContract(Contract contractToUpdate)
        {
            throw new NotImplementedException();
        }

        public void updateNanny(Nanny nannyToUpdate)
        {
            throw new NotImplementedException();
        }

        public void updateParent(Parent parentToUpdate)
        {
            throw new NotImplementedException();
        }
        //nannies grouping by language
        public List<Nanny> nannyLanguage(Boolean isSort = false, delegateSort someSort = null) { return null; }
        //nannies grouping by lift
        public List<Nanny> nannyLift(Boolean isSort = false, delegateSort someSort = null) { return null; }
        //calculatets the nannies wage
        private decimal CalculateBill(Boolean choice, Contract contract)
        {
            decimal wage = 0;
            switch (choice)
            {
                case true:
                    wage = hours(contract);
                    break;
                case false:
                    wage = months(contract);
                    break;
            }
            return wage;
        }
        private decimal hours(Contract contract)
        {
            if (contract.isAnotherChild)
            {
                return contract.hourWage * contract.totalHours * 0.98m * 4m;
            }
            else
            {
                return contract.hourWage * contract.totalHours * 4m;
            }
        }
        private decimal months(Contract contract)
        {
            //is another child need to update in the dal layer!!!!
            if (contract.isAnotherChild)
            {
                return contract.monthlyWage * 0.98m ;
            }
            else
            {
                return contract.monthlyWage;
            }
        }
        public bool sortByStars(Nanny first, Nanny second)
        {
            return first.currentStars > second.currentStars? true : false;
        }
        public bool sortByName(Person first, Person second)
        {
            return ((first.lastName).CompareTo(second.lastName) >= 0 && (first.firstName).CompareTo(second.firstName) > 0) ? true : false;
        }
        public bool sortByMinAge(Nanny first, Nanny second)
        {
            return first.minAge > second.minAge ? true : false;
        }
        public bool sortByMaxAge(Nanny first, Nanny second)
        {
            return first.maxAge > second.maxAge ? true : false;
        }
        public bool sortByPricePerHour(Nanny first, Nanny second)
        {
            return first.costPerHour < second.costPerHour ? true : false;
        }
        public bool sortByPricePerMonth(Nanny first, Nanny second)
        {
            return first.costPerMonth < second.costPerMonth ? true : false;
        }
    }
}
