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

        /// <summary>
        /// addChild - adds child into list
        /// </summary>
        /// <param name="newChild"></param>
        
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

        /// <summary>
        /// addContract - Adds constract into list
        /// </summary>
        /// <param name="newContract"></param>

        public void addContract(Contract newContract)
        {
            DateTime age = (dal.ChildEntity(newContract.ChildId)).currentAge;
            if (((dal.getAllContracts()).FindAll(x => x.NannyId == newContract.NannyId)).Count < ((dal.getAllNanny()).Find(x => x.ID == newContract.NannyId)).maxChildren)
            {
                throw new Exception("ERROR: Nanny is busy!\n");
            }
            else if(dal.NannyExist(newContract.NannyId) && dal.ParentExist(newContract.ParentId) && dal.ParentEntity(newContract.ParentId).childrenId.Contains(newContract.ChildId))
            {
                dal.addContract(newContract);
            }
            else if (age.Year == 0 && age.Month <= 3)
            {
                throw new Exception("ERROR: Babies under 3 months can't register to nanny.\n");
            } 
            else
            {
                throw new Exception("ERROR: One or more stakeholder don't exist in the data base!\n");
            }
        }

        /// <summary>
        /// addNanny - adds nanny into list
        /// </summary>
        /// <param name="newNanny"></param>

        public void addNanny(Nanny newNanny)
        {

            DateTime age = newNanny.currentAge;
            if (!dal.NannyExist(newNanny.ID))
                dal.addNanny(newNanny);
            else
                throw new Exception("ERROR: Nanny is already exit!\n");
            switch (newNanny.workField)
            {
                case Specialization.Nanny:
                    if (age.Year < 18)
                        throw new Exception("You are too young to be a nanny.\n");
                    break;
                case Specialization.Babysitter:
                    if (age.Year < 14)
                    {
                        throw new Exception("You are too young to be a babysitter.\n");
                    }
                    break;
                default:
                    break;
            }
            dal.addNanny(newNanny);     
        }

        /// <summary>
        /// addParent - Adds parent into list
        /// </summary>
        /// <param name="newParent"></param>

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

        /// <summary>
        /// betterMatchNanny - returns all the nannies that fit to all terms including the address
        /// </summary>
        /// <param name="area"></param>
        /// <param name="parent"></param>
        /// <param name="gender"></param>
        /// <param name="skill"></param>
        /// <param name="languages"></param>
        /// <param name="minExpYears"></param>
        /// <param name="spec"></param>
        /// <param name="maxCostPerHour"></param>
        /// <param name="liftInBuilding"></param>
        /// <param name="tamatHoliday"></param>
        /// <param name="minStars"></param>
        /// <returns></returns>

        public List<Nanny> betterMatchNanny(Address area, Parent parent, Gender gender, String skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars)
        {
            //Get all the nannies that fit to the parent's terms
            List<Nanny> nanniesBasic = initialMatch(parent, gender, skill, languages, minExpYears, spec, maxCostPerHour, liftInBuilding, tamatHoliday, minStars);
            //Add location constrain to the selection
            List<Nanny> nanniesAddress = initialMatch(parent, gender, skill, languages, minExpYears, spec, maxCostPerHour, liftInBuilding, tamatHoliday, minStars);
            //Pick the results that apear at both lists
            var mergeNannies = from nannyBasic in nanniesBasic
                               from nannyAddress in nanniesAddress
                               where nannyBasic.ID == nannyAddress.ID 
                               orderby nannyAddress.lastName, nannyAddress.firstName
                               select nannyAddress;
            //Returns nannie's array 
            return mergeNannies.ToList();
        }

        /// <summary>
        /// calculate distance between 2 addresses
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="dest"></param>
        /// <returns></returns>

        public int calculateDistance(Address loc, Address dest)
        {
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Walking,
                Origin = loc.ToString(),
                Destination = dest.ToString()
            };
            DirectionsResponse directions = GoogleMaps.Directions.Query(drivingDirectionRequest);
            Route route = directions.Routes.First();
            Leg leg = route.Legs.First();
            return leg.Distance.Value;
        }
        
        /// <summary>
        /// grouping by distance (todo- think how to group by another address)
        /// </summary>
        /// <param name="isSort"></param>
        /// <param name="someSort"></param>
        /// <returns></returns>

        public Dictionary<int, List<Contract>> contractDistance(bool isSort = false, Func<Contract, int> sort = null)
        {
            sort = sort == null ? ((Contract c) => c.totalHours) : sort;
            var contracts = (from contract in dal.getAllContracts()
                            orderby sort, contract.ChildId
                            group contract by calculateDistance(dal.ParentEntity(contract.ParentId).areaToSearchNanny, dal.NannyEntity(contract.NannyId).personAddress)).ToDictionary(n => n.Key, v => v.ToList());//i didnt consider the optional location
            return contracts;
        }

        /// <summary>
        /// getAllChildren - Returns all the children
        /// </summary>
        /// <returns></returns>

        public List<Child> getAllChildren()
        {
            List<Child> children = dal.getAllChildren();
            if (children != null)
            {
                return children;
            }
            throw new Exception("ERROR: No children!\n");
        }

        /// <summary>
        /// getAllChildren(List<Parent> parents) - return all the children 
        /// belongs to specific parent
        /// </summary>
        /// <param name="parents"></param>
        /// <returns></returns>

        public List<Child> getAllChildren(List<Parent> parents)
        {
            List<Child> children = dal.getAllChildren(parents);
            if (children != null)
            {
                return children;
            }
            throw new Exception("ERROR: No children!");
        }

        /// <summary>
        /// getAllContracts - return all contacts
        /// </summary>
        /// <returns></returns>

        public List<Contract> getAllContracts()
        {
            List<Contract> contracts = dal.getAllContracts();
            if (contracts != null)
            {
                return contracts;
            }
            throw new Exception("ERROR: No availble contracts!\n");
        }

        /// <summary>
        /// getAllNanny - return all nanny
        /// </summary>
        /// <returns></returns>

        public List<Nanny> getAllNanny()
        {
            List<Nanny> nannies = dal.getAllNanny();
            if (nannies != null)
            {
                return nannies;
            }
            throw new Exception("ERROR: No nannies!\n");
        }

        /// <summary>
        /// getAllParents - return all parents
        /// </summary>
        /// <returns></returns>

        public List<Parent> getAllParents()
        {
            List<Parent> parents = dal.getAllParents();
            if (parents != null)
            {
                return parents;
            }
            throw new Exception("ERROR: No parents!\n");
        }

        /// <summary>
        /// group by address
        /// </summary>
        /// <param name="isSort">delegate parameter</param>
        /// <param name="someSort"></param>
        /// <returns></returns>

        public Dictionary<int, List<Nanny>> nannyAddress(Address loc, float kilometres, bool isSort = false, Func<Nanny, float> sort = null)
        {
            sort = sort == null ? ((Nanny n) => n.currentStars) : sort;
            Dictionary<int, int> distances = new Dictionary<int, int>();
            //!
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
                (from nanny in dal.getAllNanny()
                from d in distances
                where d.Key == Int32.Parse(nanny.ID) && d.Value <= kilometres
                orderby sort , nanny.firstName, nanny.lastName
                group nanny by d.Value into temp
                select new { lang = temp.Key, nan = temp}).ToDictionary(k => k.lang, v => v.nan.ToList());
            
            return nanniesByDistance;
            ////////////////////////////Check if this function works!!!!!!!!!!!!!!!!
        }

        /// <summary>
        /// group by minimum age
        /// </summary>
        /// <param name="isSort"></param>
        /// <param name="someSort"></param>
        /// <returns></returns>/!?

        public Dictionary<int, List<Nanny>> nannyAge(bool isSort = false, Func<Nanny, int> sort = null)
        {
            
            if (sort == null)
            {
                sort = (Nanny n) => n.maxAge;
            }
            var nannies = (from nanny in dal.getAllNanny()
                          orderby sort, nanny.lastName, nanny.firstName
                          group nanny by nanny.minAge into g
                          select new { age = g.Key, nan = g}).ToDictionary(k => k.age, v => v.nan.ToList());
            
            return nannies;//////we didnt use sort 
        }

        /// <summary>
        /// numberOfContracts - counts the number of contracts fullfil the specific terms
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>

        public int numberOfContracts(Predicate<Contract> condition)
        {
            return specificContracts(condition).Count;
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
        /// specificContracts - returns contracts that fits to specific parameters
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

        /// <summary>
        /// updateChild - updating child details
        /// </summary>
        /// <param name="childToUpdate"></param>

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

        /// <summary>
        /// nannies grouping by language
        /// </summary>
        /// <param name="isSort"></param>
        /// <param name="someSort"></param>
        /// <returns></returns>

        public Dictionary<Language, List<Nanny>> nannyLanguage(Boolean isSort = false, Func<Nanny,int> sort = null)
        {
            //grouping by the first language because the list is problematic
            sort = sort == null ? ((Nanny n) => n.expYears) : sort;
            var nannies = (from nanny in dal.getAllNanny()
                           orderby sort, nanny.lastName, nanny.firstName
                           group nanny by nanny.nannyLanguage[0] into tmp//!!
                           select new {lang = tmp.Key, nan =tmp }).ToDictionary(k => k.lang, v => v.nan.ToList());
            return nannies;
        }

        /// <summary>
        /// nannies grouping by lift
        /// </summary>
        /// <param name="isSort"></param>
        /// <param name="someSort"></param>
        /// <returns></returns>

        public Dictionary<bool, List<Nanny>> nannyLift(Boolean isSort = false, Func<Nanny, float> sort = null)
        {
            sort = sort == null ? ((Nanny n) => n.currentStars) : sort;
            var nannies = (from nanny in dal.getAllNanny()
                           orderby sort, nanny.lastName, nanny.firstName
                           group nanny by nanny.isLift into tmp//!!
                           select new { b = tmp.Key, nan = tmp }).ToDictionary(k => k.b, v => v.nan.ToList());
            return nannies;
        }

        /// <summary>
        /// calculatets the nannies wage
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="contract"></param>
        /// <returns></returns>

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

        /// <summary>
        /// initialMatch - Returns the nannies who are fit to specific terms
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="gender"></param>
        /// <param name="skill"></param>
        /// <param name="languages"></param>
        /// <param name="minExpYears"></param>
        /// <param name="spec"></param>
        /// <param name="maxCostPerHour"></param>
        /// <param name="liftInBuilding"></param>
        /// <param name="tamatHoliday"></param>
        /// <param name="minStars"></param>
        /// <returns></returns>

        public List<Nanny> initialMatch(Parent parent, Gender gender, String skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars)
        {
            bool temp;
            List<Nanny> nannyList = new List<Nanny>();
            List<Nanny> tempNannyList = new List<Nanny>();
            if (tamatHoliday)//If the parent wants a nanny who takes vacations according to tamat
                tempNannyList = tamatVacation();//returns nannies who keep tamat holidays
            else
                tempNannyList = dal.getAllNanny();
            var children = from child in dal.getAllChildren()
                        where child.parentId == parent.ID//all children of this parent
                        select child;
            foreach (var child in children)
            {
                var nannies = from nanny in tempNannyList
                              where child.currentAge.Month >= nanny.minAge && child.currentAge.Month <= nanny.maxAge//nanny takes children of that age
                              where nanny.workField == spec && nanny.nannyGender == gender && (nanny.nannySkills).Contains(skill) &&
                                    minExpYears == nanny.expYears && maxCostPerHour >= nanny.costPerHour && nanny.currentStars >= minStars
                              orderby nanny.lastName, nanny.firstName/////////Change to sort by distance?
                              select nanny;
                ///////////////////Want to throw this exception????
                if (!nannies.Any())//If not nannies meet the requirements
                    throw new Exception("No nannies match the parents' requirements for this child!\n");
                foreach (var nanny in nannies)
                {
                    if (liftInBuilding && !nanny.isLift)//Checks lift requirements
                        continue;
                    temp = false;
                    foreach (var lang in languages)//Checks if nanny speaks at least one required language
                    {
                        if(nanny.nannyLanguage.Contains(lang))
                        {
                            temp = true;
                            break;
                        }
                    }
                    if (!temp)
                        continue;
                    temp = true;
                    foreach (var day in parent.parentWorkhours)//Check if nanny work in all the hours the parents require
                    {
                        if (nanny.workhours.ContainsKey(day.Key))
                            if (nanny.workhours[day.Key].Key > day.Value.Key || nanny.workhours[day.Key].Value < day.Value.Value)//Check nanny doesn't start later of finish earlier than the parents need.
                            {
                                temp = false;
                                break;
                            }
                    }
                    if (!temp)
                        continue;
                    nannyList.Add(nanny);
                }
            }
            return nannyList;
        }

        /// <summary>
        /// childrenWithNoNanny - Returns list of all the children who do not yet have a nanny
        /// (i.e.are not included in a signed contract)
        /// </summary>
        /// <returns></returns>

        public List<Child> childrenWithNoNanny()
        {
            List<Child> children = dal.getAllChildren();//get all children
            var childNoNanny = from child in dal.getAllChildren()
                               from contract in dal.getAllContracts()
                               where child.ID == contract.ChildId//put child who has contract into list of children to remove from list of all children
                               select child;
            foreach (var child in childNoNanny)//remove all children for whom a signed contract has been found.
            {
                children.Remove(child);
            }
            return children;
        }

        /// <summary>
        /// nanniesNearby - returns the nannies are located in a specific radius near the parent's location
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>

        public Dictionary<int, List<Nanny>> nanniesNearby(Parent parent, float maxDistance)
        {
            Address addressToSearchFrom = new Address();
            if (addressToSearchFrom == null)//If parent did not define an address from which to search, use their home address.
                addressToSearchFrom = parent.personAddress;
            else
                addressToSearchFrom = parent.areaToSearchNanny;
            //////////////////////commment on next line............
            return nannyAddress(addressToSearchFrom, maxDistance, true);//Do we want this? delegateSort someSort = null, float );
        }

        
    }
}
