using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Elevation.Request;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;
using System.Reflection;

namespace BL
{
    class BL_imp : IBL
    {
        //defines a component of dal layer
        DAL.Idal dal;
        public void Cass_BlAdapter()
        {
            
            dal = DAL.DalFactory.getReference();
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

        public List<Nanny> betterMatchNanny(Address area, Parent parent)
        {
            throw new NotImplementedException();
        }

        public List<Contract> contractDistance(bool isSort = false, delegateSort someSort = null)
        {
            throw new NotImplementedException();
        }

        public List<Child> getAllChildren()
        {
            return dal.getAllChildren();
        }
        /// <summary>
        /// getAllChildren(List<Parent> parents) - return all the children 
        /// belongs to specific parent
        /// </summary>
        /// <param name="parents"></param>
        /// <returns></returns>
        public List<Child> getAllChildren(List<Parent> parents)
        {
            return dal.getAllChildren(parents);
        }
        /// <summary>
        /// getAllContracts - return all contacts
        /// </summary>
        /// <returns></returns>
        public List<Contract> getAllContracts()
        {
            return dal.getAllContracts();
        }
        /// <summary>
        /// getAllNanny - return all nanny
        /// </summary>
        /// <returns></returns>
        public List<Nanny> getAllNanny()
        {
            return dal.getAllNanny();
        }
        /// <summary>
        /// getAllParents - return all parents
        /// </summary>
        /// <returns></returns>
        public List<Parent> getAllParents()
        {
            return dal.getAllParents();
        }

        public List<Nanny> matchNanny(Parent parent)
        {
            throw new NotImplementedException();
        }

        public List<Nanny> matchNanny(Address area)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// group by address
        /// </summary>
        /// <param name="isSort">delegate parameter</param>
        /// <param name="someSort"></param>
        /// <returns></returns>
        public List<Nanny> nannyAddress(Address loc, bool isSort = false, delegateSort someSort = null)
        {
            List<Nanny> nannyList = new List<Nanny>();
            Dictionary<int, int> distances = new Dictionary<int, int>();
            if (someSort == null)
            {
                someSort = sortByName;
            }
            foreach (var nanny in dal.getAllNanny())
            {
                var drivingDirectionRequest = new DirectionsRequest
                {
                    TravelMode = TravelMode.Walking,
                    Origin = loc.ToString(),
                    Destination = nanny.personAddress.ToString()
                };
                DirectionsResponse directions = GoogleMaps.Directions.Query(drivingDirectionRequest);
                Route route = directions.Routes.First();
                Leg leg = route.Legs.First();
                distances.Add(Int32.Parse(nanny.ID), leg.Distance.Value);
            }
            var nanniesByDistance =
                from nanny in dal.getAllNanny()
                from d in distances
                where d.Key == Int32.Parse(nanny.ID)
                group nanny by d.Value into temp
                select new { dist = temp.Key, nan = temp};
            foreach(var n in nanniesByDistance)
            {
                foreach (var item in n.nan)
                    nannyList.Add(item);
            }
            return nannyList;
            ////////////////////////////Check if this function works!!!!!!!!!!!!!!!!
        }
        /// <summary>
        /// group by minimum age
        /// </summary>
        /// <param name="isSort"></param>
        /// <param name="someSort"></param>
        /// <returns></returns>/!?
        public List<Nanny> nannyAge(bool isSort = false, delegateSort someSort = null)
        {
            List<Nanny> nannyList = new List<Nanny>();
            if (someSort == null)
            {
                someSort = sortByName;
            }
            var nannies = from nanny in dal.getAllNanny()
                          orderby nanny.lastName, nanny.firstName
                          group nanny by nanny.minAge into g
                          select new { age = g.Key, nan = g};
            foreach( var item in nannies)
            {
                foreach (var nanny in item.nan)
                    nannyList.Add(nanny);
            }
            return nannyList;//////we didnt use sort 
        }

        public List<Child> noNanny()
        {
            throw new NotImplementedException();
        }

        public int numberOfContracts(delegateCondition condition)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// removeChild - remove child from the array and from the contract 
        /// the child sign in
        /// </summary>
        /// <param name="childId"></param>
        /// <returns>Boolian variable indicates if the
        /// child is removeable
        /// </returns>
        public bool removeChild(string childId)
        {
            if (dal.ChildExist(childId) )
            {
                Contract c = dal.getAllContracts().FirstOrDefault(x => x.ChildId == childId);
                dal.removeContract(Int32.Parse(c.contractID));///ask malka
                dal.removeChild(childId);
                //TODO -> CHECK ANOTHER FIELD WITH THE CURRENT CHILD AND REMOVE THEM
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// removeContract - remove specific contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public bool removeContract(int contractId)
        {
            if (dal.ContractExist(contractId.ToString()))
            {
                dal.removeContract(contractId);
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// removeNanny - remove nanny and her contract
        /// </summary>
        /// <param name="nannyId"></param>
        /// <returns></returns>
        public bool removeNanny(string nannyId)
        {
            if (dal.NannyExist(nannyId))
            {
                Contract c = dal.getAllContracts().FirstOrDefault(x => x.NannyId == nannyId);
                dal.removeContract(Int32.Parse(c.contractID));///ask malka
                dal.removeNanny(nannyId);
                //TODO -> CHECK ANOTHER FIELD WITH THE CURRENT nanny AND REMOVE THEM
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// removeParent - remove parent her/his sibllings and contrracts are 
        /// related to him/she
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public bool removeParent(string parentId)
        {
            if (dal.ParentExist(parentId))
            {
                Contract c = dal.getAllContracts().FirstOrDefault(x => x.ParentId == parentId);
                dal.removeContract(Int32.Parse(c.contractID));///ask malka
                dal.getAllChildren().RemoveAll(x => x.parentId == parentId);
                dal.removeParent(parentId);
                //TODO -> CHECK ANOTHER FIELD WITH THE CURRENT parent AND REMOVE THEM
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// TODO!!
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<Contract> specificContracts(Predicate<Contract> condition = null)
        {
            List<Contract> result;
            if (condition == null)
            {
                result = dal.getAllContracts();
            }
            else
            {
                result = dal.getAllContracts().FindAll(condition);
            }
            return result;
        }
        /// <summary>
        /// tamatVacation - This function returns the nannies which
        /// have a vacation like the tamat vacation
        /// </summary>
        /// <returns></returns>
        public List<Nanny> tamatVacation()
        {
            List<Nanny> nannies = new List<Nanny>();
            var tmpNannies = from Nanny in dal.getAllNanny()
                                  orderby Nanny.costPerMonth
                                  where Nanny.isVacation == true
                                  select Nanny;
            foreach (var item in tmpNannies)
            {
                nannies.Add(item);
            }
            return nannies;
        }
        //updating child details
        public void updateChild(Child childToUpdate)
        {
            if (dal.ChildExist(childToUpdate.ID))
            {
                dal.updateChild(childToUpdate);
            }
            else
            {
                throw new Exception("ERROR: The child doesn't exist in the system!");
            }
        }
        /// <summary>
        /// updateContract - updating contract details
        /// </summary>
        /// <param name="contractToUpdate"></param>
        public void updateContract(Contract contractToUpdate)
        {
            if (dal.ContractExist(contractToUpdate.contractID))
            {
                dal.updateContract(contractToUpdate);
            }
            else
            {
                throw new Exception("ERROR: This contract doesn't exist in the system.");
            }
        }
        /// <summary>
        /// updateNanny - updates nanny
        /// </summary>
        /// <param name="nannyToUpdate"></param>
        public void updateNanny(Nanny nannyToUpdate)
        {
            if (dal.NannyExist(nannyToUpdate.ID))
            {
                dal.updateNanny(nannyToUpdate);
            }
            else
            {
                throw new Exception("ERROR: Nanny doesn't exist in the system.");
            }
        }
        /// <summary>
        /// updateParent - updates parent
        /// </summary>
        /// <param name="parentToUpdate"></param>
        public void updateParent(Parent parentToUpdate)
        {
            if (dal.NannyExist(parentToUpdate.ID))
            {
                dal.updateParent(parentToUpdate);
            }
            else
            {
                throw new Exception("ERROR: Parent doesn't exist in the system.");
            }
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
        /// <summary>
        /// calculate the bill if the parent pays hourly
        /// </summary>
        /// <param name="contract"> the contract in proccess</param>
        /// <returns>the total price</returns>
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
        /// <summary>
        /// calculate the bill if the parent pays monthly
        /// </summary>
        /// <param name="contract"> the contract in proccess</param>
        /// <returns>the total price</returns>
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
        /// <summary>
        /// checks if the first element is bigger (via stars->reffers to nanny) than the second
        /// </summary>
        /// <param name="first">the object we check</param>
        /// <param name="second">the object we used to compare</param>
        /// <returns></returns>
        public bool sortByStars(Nanny first, Nanny second)
        {
            return first.currentStars > second.currentStars? true : false;
        }
        /// <summary>
        /// checks if the first element is bigger (via full name) than the second
        /// </summary>
        /// <param name="first">the object we check</param>
        /// <param name="second">the object we used to compare</param>
        /// <returns></returns>
        public bool sortByName(Person first, Person second)
        {
            return ((first.lastName).CompareTo(second.lastName) >= 0 && (first.firstName).CompareTo(second.firstName) > 0) ? true : false;
        }
        /// <summary>
        /// checks if the first element is bigger (via minage) than the second
        /// </summary>
        /// <param name="first">the object we check</param>
        /// <param name="second">the object we used to compare</param>
        /// <returns></returns>
        public bool sortByMinAge(Nanny first, Nanny second)
        {
            return first.minAge > second.minAge ? true : false;
        }
        /// <summary>
        /// checks if the first element is bigger (via max age) than the second
        /// </summary>
        /// <param name="first">the object we check</param>
        /// <param name="second">the object we used to compare</param>
        /// <returns></returns>
        public bool sortByMaxAge(Nanny first, Nanny second)
        {
            return first.maxAge > second.maxAge ? true : false;
        }
        /// <summary>
        /// checks if the first element is bigger (via price per hour) than the second
        /// </summary>
        /// <param name="first">the object we check</param>
        /// <param name="second">the object we used to compare</param>
        /// <returns></returns>
        public bool sortByPricePerHour(Nanny first, Nanny second)
        {
            return first.costPerHour < second.costPerHour ? true : false;
        }
        /// <summary>
        /// checks if the first element is bigger (via monthly price) than the second
        /// </summary>
        /// <param name="first">the object we check</param>
        /// <param name="second">the object we used to compare</param>
        /// <returns></returns>
        public bool sortByPricePerMonth(Nanny first, Nanny second)
        {
            return first.costPerMonth < second.costPerMonth ? true : false;
        }
        /// <summary>
        /// ContractParentFittnes - check if the contract fitts to a specific term
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public bool ContractParentFittnes(Contract contract, String id)
        {
            return contract.ParentId == id;
        }
        /// <summary>
        /// ContractChildFittnes - check if the contract relates to child
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ContractChildFittnes(Contract contract, String id)
        {
            return contract.ChildId == id;
        }
        /// <summary>
        /// ContractNannyFittnes - check if the contract relates to nanny
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ContractNannyFittnes(Contract contract, String id)
        {
            return contract.NannyId == id;
        }
        /// <summary>
        /// ContractHourWageFittnes - check the wealth of the contract hourly
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool ContractHourWageFittnes(Contract contract, int wage)
        {
            return contract.hourWage >= wage;
        }
        /// <summary>
        /// ContractMonthWageFittnes - check the wealth of the contract monthly 
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ContractMonthWageFittnes(Contract contract, int wage)
        {
            return contract.monthlyWage >= wage;
        }
        /// <summary>
        /// ContractTotalHoursFittnes - check the contract above 
        /// total hours threshold
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ContractTotalHoursFittnes(Contract contract, float hours)
        {
            return contract.totalHours >= hours;
        }
        
    }
}
