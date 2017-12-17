using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;


namespace BL
{
    #region TIME SPAN EXTANDSION
    /// <summary>
    /// TimeSpanExtensions - extands the timeSpan functions
    /// </summary>

    public static class TimeSpanExtensions
    {
        //return years
        public static int GetYears(this TimeSpan timespan) => (int)(timespan.Days / 365.2425);

        //return months
        public static int GetMonths(this TimeSpan timespan) => (int)(timespan.Days / 30.436875);
    }
    #endregion

    class BL_imp : IBL
    {
        //defines a component of dal layer
        DAL.Idal dal;

        #region INIT
        /// <summary>
        /// dal assigning
        /// </summary>
        public void Cass_BlAdapter()
        {
            dal = DAL.DalFactory.getReference();
        }
        /// <summary>
        /// constructor
        /// </summary>
        public BL_imp()
        {
            Cass_BlAdapter();
        }
        #endregion

        #region ADD
        /// <summary>
        /// addChild - adds child into list
        /// </summary>
        /// <param name="newChild">a new child in the DS</param>

        public void addChild(Child newChild)
        {
            //Check if the child exists in the DS
            if (!ChildExist(newChild.ID))
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
        /// <param name="newContract">New contract to the records</param>

        public void addContract(Contract newContract)
        {
            //checks the contracts and throw exception
            checkContract(newContract);
            dal.addContract(newContract);
        }

        /// <summary>
        /// checkContract - checks contract validity
        /// </summary>
        /// <param name="newContract">contract to check</param>
        
        private void checkContract(Contract newContract)
        {
            //child's current age
            TimeSpan age = (ChildEntity(newContract.ChildId)).currentAge;

            //checks if all entities in the contract exist
            if (!NannyExist(newContract.NannyId) || !ParentExist(newContract.ParentId) || !ParentEntity(newContract.ParentId).childrenId.Contains(newContract.ChildId))
            {
                throw new Exception("ERROR: One or more stakeholder don't exist in the data base!\n");
            }
            //Checks if the nanny have place to another child
            else if ((((dal.getAllContracts()).FindAll(x => x.NannyId == newContract.NannyId)).Count == ((dal.getAllNanny()).Find(x => x.ID == newContract.NannyId)).maxChildren))
            {
                throw new Exception("ERROR: Nanny is busy!\n");
            }
            //Checks if the infinity is old anogth to be in nannies home
            else if (age.GetYears() == 0 && age.GetMonths() <= 3)
            {
                throw new Exception("ERROR: Babies under 3 months can't register to nanny.\n");
            }
        }

        /// <summary>
        /// addNanny - adds nanny into list
        /// </summary>
        /// <param name="newNanny">New nanny to the DS</param>

        public void addNanny(Nanny newNanny)
        {
            checkNanny(newNanny);
            if (!NannyExist(newNanny.ID))
                dal.addNanny(newNanny);
            else
                throw new Exception("ERROR: Nanny is already exit!\n");
        }

        /// <summary>
        /// checkNanny - checks nannies validity
        /// </summary>
        /// <param name="newNanny">exexption if the nanny isnt correct</param>
        
        private static void checkNanny(Nanny newNanny)
        {
            //nannies age
            TimeSpan age = newNanny.currentAge;
            // babysiter can be younger
            switch (newNanny.workField)
            {
                case Specialization.Nanny:
                    if (age.GetYears() < 18)
                        throw new Exception("You are too young to be a nanny.\n");
                    break;
                case Specialization.Babysitter:
                    if (age.GetYears() < 14)
                    {
                        throw new Exception("You are too young to be a babysitter.\n");
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// addParent - Adds parent into list
        /// </summary>
        /// <param name="newParent">New parent to the DS</param>

        public void addParent(Parent newParent)
        {   
            //Checks if parent exist
            if (!ParentExist(newParent.ID))
            {
                dal.addParent(newParent);
            }
            else
            {
                throw new Exception("ERROR: Parent is already exists!\n");
            }
        }
        #endregion

        #region GET ALL
        /// <summary>
        /// getAllChildren - Returns all the children
        /// </summary>
        /// <returns>children list</returns>

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
        /// <param name="parents">children belongs to specific parents</param>
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
        /// <returns>contracts list</returns>

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
        /// <returns>nannies list</returns>

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
        /// <returns>parents list</returns>

        public List<Parent> getAllParents()
        {
            List<Parent> parents = dal.getAllParents();
            if (parents != null)
            {
                return parents;
            }
            throw new Exception("ERROR: No parents!\n");
        }
        #endregion

        #region REMOVE
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
            if (ChildExist(childId))
            {
                //finds the contracts the child signed in
                var contracts = from contract in getAllContracts()
                                where contract.ChildId == childId
                                select contract.contractID;
                //delete all contracts the child relates to
                foreach (var cont in contracts.ToList())//Remove all contracts associated with the nanny
                {
                    removeContract(cont);
                }
                dal.removeChild(childId);
                
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
        /// <param name="contractId">bool if the the contract is removeable</param>
        /// <returns></returns>

        public bool removeContract(String contractId)
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
        /// <param name="nannyId">bool if the nanny is removeable</param>
        /// <returns></returns>

        public bool removeNanny(string nannyId, bool eraseContracts = true)
        {
            //If nanny exists
            if (NannyExist(nannyId))
            {
                //If we need to erase all associated contracts
                if (eraseContracts)
                {
                    //erase the contracts relate to the current nanny
                    var contracts = from contract in getAllContracts()
                                    where contract.NannyId == nannyId
                                    select contract.contractID;
                    foreach (var cont in contracts.ToList())//Remove all contracts associated with the nanny
                    {
                        removeContract(cont);
                    }
                }
                //remove nanny
                dal.removeNanny(nannyId);
                
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
            if (ParentExist(parentId))
            {   
                //contracts relate to the parent
                var contracts = from contract in getAllContracts()
                                where contract.ParentId == parentId
                                select contract.contractID;
                //Remove all contracts associated with the nanny
                foreach (var cont in contracts.ToList())
                {
                    removeContract(cont);
                }
                //List<Child> rChild = dal.getAllChildren().FindAll(x => x.parentId == parentId);
                //Remove all contracts associated with the nanny
                foreach (String c in ParentEntity(parentId).childrenId)
                {
                    removeChild(c);
                }
                dal.removeParent(parentId);
               
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// updateChild - updating child details
        /// </summary>
        /// <param name="childToUpdate">a full structure of child to update</param>

        public void updateChild(Child childToUpdate)
        {
            if (ChildExist(childToUpdate.ID))
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
        /// <param name="contractToUpdate">contract to update</param>

        public void updateContract(Contract contractToUpdate)
        {
            //checks the contract
            checkContract(contractToUpdate);
            //if contract exist, update
            if (ContractExist(contractToUpdate.contractID))
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
            //check nanny validity
            checkNanny(nannyToUpdate);
            //if nanny exist, update
            if (NannyExist(nannyToUpdate.ID))
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
        /// <param name="parentToUpdate">void</param>

        public void updateParent(Parent parentToUpdate)
        {
            if (NannyExist(parentToUpdate.ID))
            {
                dal.updateParent(parentToUpdate);
            }
            else
            {
                throw new Exception("ERROR: Parent doesn't exist in the system.");
            }
        }
        #endregion

        #region GROUPING

        #region CONTRACT DISTANCE
        /// <summary>
        /// grouping by distance (todo- think how to group by another address)
        /// </summary>
        /// <param name="isSort"> boolean parameter indicates if the user wants the results orgenized by some parameter</param>
        /// <param name="someSort">the sort the user wants to use</param>
        /// <returns>dictionary of contracts sorted by the user preferance</returns>

        public Dictionary<int, List<Contract>> contractDistance(bool isSort = false, Func<Contract, int> sort = null)
        {
            //by default the function sort by total hours
            sort = sort == null ? ((Contract c) => c.totalHours) : sort;
            //gets all contracts grouping by distance from the nanny to the parent area 
            var contracts = (from contract in dal.getAllContracts()
                            orderby sort, contract.ChildId
                            group contract by calculateDistance(ParentEntity(contract.ParentId).areaToSearchNanny, NannyEntity(contract.NannyId).personAddress)).ToDictionary(n => n.Key, v => v.ToList());//i didnt consider the optional location
            return contracts;
        }

        //overlapping of the func contractDistance, grouping for the input list

        public Dictionary<int, List<Contract>> contractDistance(List<Contract> cont, bool isSort = false, Func<Contract, int> sort = null)
        {
            //by default the function sort by total hours
            sort = sort == null ? ((Contract c) => c.totalHours) : sort;
            //gets all contracts grouping by distance from the nanny to the parent area 
            var contracts = isSort ?  (from contract in cont
                             orderby sort, contract.ChildId
                             group contract by calculateDistance(ParentEntity(contract.ParentId).areaToSearchNanny, NannyEntity(contract.NannyId).personAddress)).ToDictionary(n => n.Key, v => v.ToList()):
                             (from contract in cont
                              orderby contract.ChildId
                              group contract by calculateDistance(ParentEntity(contract.ParentId).areaToSearchNanny, NannyEntity(contract.NannyId).personAddress)).ToDictionary(n => n.Key, v => v.ToList());//i didnt consider the optional location
            return contracts;
        }
        #endregion

        #region Language Grouping
        /// <summary>
        /// nannies grouping by language
        /// </summary>
        /// <param name="isSort">if the user want the results orgenized</param>
        /// <param name="someSort">the sort the user wants</param>
        /// <returns>the nannies gruaping by</returns>

        public Dictionary<Language, List<Nanny>> nannyLanguage(Boolean isSort = false, Func<Nanny, int> sort = null)
        {
            //grouping by the first language because the list is problematic
            sort = sort == null ? ((Nanny n) => n.expYears) : sort;
            var nannies = isSort ?  (from nanny in dal.getAllNanny()
                           orderby sort, nanny.lastName, nanny.firstName
                           group nanny by nanny.nannyLanguage[0] into tmp//!!
                           select new { lang = tmp.Key, nan = tmp }).ToDictionary(k => k.lang, v => v.nan.ToList()):
                           (from nanny in dal.getAllNanny()
                            orderby nanny.lastName, nanny.firstName
                            group nanny by nanny.nannyLanguage[0] into tmp//!!
                            select new { lang = tmp.Key, nan = tmp }).ToDictionary(k => k.lang, v => v.nan.ToList());
            return nannies;
        }
        
        //overlapping
        public Dictionary<Language, List<Nanny>> nannyLanguage(List<Nanny>  nan, Boolean isSort = false, Func<Nanny, int> sort = null)
        {
            //grouping by the first language because the list is problematic
            sort = sort == null ? ((Nanny n) => n.expYears) : sort;
            var nannies = isSort ? (from nanny in nan
                                    orderby sort, nanny.lastName, nanny.firstName
                                    group nanny by nanny.nannyLanguage[0] into tmp//!!
                                    select new { lang = tmp.Key, nan = tmp }).ToDictionary(k => k.lang, v => v.nan.ToList()) :
                           (from nanny in nan
                            orderby nanny.lastName, nanny.firstName
                            group nanny by nanny.nannyLanguage[0] into tmp//!!
                            select new { lang = tmp.Key, nan = tmp }).ToDictionary(k => k.lang, v => v.nan.ToList());
            return nannies;
        }
        #endregion

        #region LIFT GROUPING
        /// <summary>
        /// nannies grouping by lift
        /// </summary>
        /// <param name="isSort"></param>
        /// <param name="someSort"></param>
        /// <returns></returns>

        public Dictionary<bool, List<Nanny>> nannyLift(Boolean isSort = false, Func<Nanny, float> sort = null)
        {
            sort = sort == null ? ((Nanny n) => n.currentStars) : sort;
            var nannies = isSort ? (from nanny in dal.getAllNanny()
                           orderby sort, nanny.lastName, nanny.firstName
                           group nanny by nanny.isLift into tmp//!!
                           select new { b = tmp.Key, nan = tmp }).ToDictionary(k => k.b, v => v.nan.ToList()):
                           (from nanny in dal.getAllNanny()
                            orderby nanny.lastName, nanny.firstName
                            group nanny by nanny.isLift into tmp//!!
                            select new { b = tmp.Key, nan = tmp }).ToDictionary(k => k.b, v => v.nan.ToList());
            return nannies;
        }
        //overlapping 
        public Dictionary<bool, List<Nanny>> nannyLift(List<Nanny> nanniesResults, Boolean isSort = false, Func<Nanny, float> sort = null)
        {
            sort = sort == null ? ((Nanny n) => n.currentStars) : sort;
            var nannies = isSort ? (from nanny in nanniesResults
                                    orderby sort, nanny.lastName, nanny.firstName
                                    group nanny by nanny.isLift into tmp//!!
                                    select new { b = tmp.Key, nan = tmp }).ToDictionary(k => k.b, v => v.nan.ToList()) :
                           (from nanny in nanniesResults
                            orderby nanny.lastName, nanny.firstName
                            group nanny by nanny.isLift into tmp//!!
                            select new { b = tmp.Key, nan = tmp }).ToDictionary(k => k.b, v => v.nan.ToList());
            return nannies;
        }

        #endregion

        #region ADDRESS GROUP
        /// <summary>
        /// group by address
        /// </summary>
        /// <param name="isSort">delegate parameter</param>
        /// <param name="someSort"></param>
        /// <returns></returns>

        public Dictionary<int, List<Nanny>> nannyAddress(Address loc, bool isSort = false, Func<Nanny, float> sort = null, float? kilometres = null)
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
            //if the func get a parameter kilometers the results will be in the desired range
            var nanniesByDistance = kilometres == null?
                (from nanny in dal.getAllNanny()
                from d in distances
                where d.Key == Int32.Parse(nanny.ID) 
                orderby sort , nanny.firstName, nanny.lastName
                group nanny by d.Value into temp
                select new { lang = temp.Key, nan = temp}).ToDictionary(k => k.lang, v => v.nan.ToList()):
                (from nanny in dal.getAllNanny()
                 from d in distances
                 where d.Key == Int32.Parse(nanny.ID) && d.Value <= kilometres
                 orderby sort, nanny.firstName, nanny.lastName
                 group nanny by d.Value into temp
                 select new { lang = temp.Key, nan = temp }).ToDictionary(k => k.lang, v => v.nan.ToList());
            
            return nanniesByDistance;
            
        }
        //overlapping
        public Dictionary<int, List<Nanny>> nannyAddress(List<Nanny> nannyResults, Address loc, bool isSort = false, Func<Nanny, float> sort = null, float? kilometres = null)
        {
            sort = sort == null ? ((Nanny n) => n.currentStars) : sort;
            Dictionary<int, int> distances = new Dictionary<int, int>();
            //!
            foreach (var nanny in nannyResults)
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
            //if the func get a parameter kilometers the results will be in the desired range
            var nanniesByDistance = kilometres == null ?
                (from nanny in nannyResults
                 from d in distances
                 where d.Key == Int32.Parse(nanny.ID)
                 orderby sort, nanny.firstName, nanny.lastName
                 group nanny by d.Value into temp
                 select new { lang = temp.Key, nan = temp }).ToDictionary(k => k.lang, v => v.nan.ToList()) :
                (from nanny in nannyResults
                 from d in distances
                 where d.Key == Int32.Parse(nanny.ID) && d.Value <= kilometres
                 orderby sort, nanny.firstName, nanny.lastName
                 group nanny by d.Value into temp
                 select new { lang = temp.Key, nan = temp }).ToDictionary(k => k.lang, v => v.nan.ToList());

            return nanniesByDistance;

        }
        #endregion

        #region MIN AGE GROUP
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
            var nannies = isSort ? (from nanny in dal.getAllNanny()
                           orderby sort, nanny.lastName, nanny.firstName
                           group nanny by nanny.minAge into g
                           select new { age = g.Key, nan = g }).ToDictionary(k => k.age, v => v.nan.ToList()):
                           (from nanny in dal.getAllNanny()
                            orderby nanny.lastName, nanny.firstName
                            group nanny by nanny.minAge into g
                            select new { age = g.Key, nan = g }).ToDictionary(k => k.age, v => v.nan.ToList());

            return nannies;//////we didnt use sort 
        }
        //OVERLAPPING
        public Dictionary<int, List<Nanny>> nannyAge(List<Nanny> nannyResults, bool isSort = false, Func<Nanny, int> sort = null)
        {

            if (sort == null)
            {
                sort = (Nanny n) => n.maxAge;
            }
            var nannies = isSort ? (from nanny in nannyResults
                                    orderby sort, nanny.lastName, nanny.firstName
                                    group nanny by nanny.minAge into g
                                    select new { age = g.Key, nan = g }).ToDictionary(k => k.age, v => v.nan.ToList()) :
                           (from nanny in nannyResults
                            orderby nanny.lastName, nanny.firstName
                            group nanny by nanny.minAge into g
                            select new { age = g.Key, nan = g }).ToDictionary(k => k.age, v => v.nan.ToList());

            return nannies;//////we didnt use sort 
        }


        #endregion
        #endregion

        #region MATCH

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

        public List<Nanny> betterMatchNanny(Address area, Parent parent, Gender gender, List<SKILLS> skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars, String childId = null)
        {
            //Get all the nannies that fit to the parent's terms
            List<Nanny> nanniesBasic = initialMatch(parent, gender, skill, languages, minExpYears, spec, maxCostPerHour, liftInBuilding, tamatHoliday, minStars, childId);
            //Add location constrain to the selection
            List<Nanny> nanniesAddress = null;//nanniesAddress(area, );
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
        
        //help function to initial match
        private bool checkHours(Nanny nanny, Parent parent)
        {
            //The function checks fittness between the nanny work-hours anfd the parent recuirement
            foreach (var day in parent.parentWorkhours)//Check if nanny work in all the hours the parents require
            {
                if (nanny.workhours.ContainsKey(day.Key))
                    //Check nanny doesn't start later of finish earlier than the parents need.
                    if (nanny.workhours[day.Key].Key > day.Value.Key || nanny.workhours[day.Key].Value < day.Value.Value)
                    {
                        return false;
                    }
                else
                    return false;

            }
            return true;
        }

        public List<Nanny> initialMatch(Parent parent, Gender gender, List<SKILLS> skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars, String childId = null)
        {
            //general constraints
            bool b = getAllNanny()[0].currentStars >= minStars;
            var nannies = (from nanny in getAllNanny()
                          where skill.TrueForAll(nanny.nannySkills.Contains) && nanny.nannyGender.Equals(gender) && languages.Any(nanny.nannyLanguage.Contains) && nanny.expYears >= minExpYears
                          && nanny.workField.Equals(spec) && nanny.costPerHour < maxCostPerHour && (nanny.isVacation || !tamatHoliday) && nanny.currentStars >= minStars
                          && (nanny.isLift || !liftInBuilding) && checkHours(nanny, parent)
                          select nanny).ToList();
            //if the existance of the lift is important to the parent
            //if (liftInBuilding)
            //{
            //    nannies = nannies.FindAll(x => x.isLift);
            //}

            //if the search is about a specific child
            if (childId != null)
            {
                float age = (ChildEntity(childId).currentAge.GetMonths() / 12 + ChildEntity(childId).currentAge.GetYears());
                nannies = nannies.FindAll(x => (x.minAge <= age) && (x.maxAge >= age) );
            }
            return nannies;
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
            //If parent did not define an address from which to search, use their home address.
            if (addressToSearchFrom == null)
                addressToSearchFrom = parent.personAddress;
            else
                addressToSearchFrom = parent.areaToSearchNanny;
            //returns the nannies near the parent area, grouping by distance and sorted according to current stars
            return nannyAddress(addressToSearchFrom,  true, (Nanny n) => n.currentStars ,maxDistance);
        }
        
        #endregion

        #region CALCULATE DISTANCE
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
        #endregion

        #region CONTRACTS
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
        #endregion

        #region CHECK VACATION TYPE
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
        #endregion

        #region CHILDREN WITH NO NANNY
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
            foreach (var child in childNoNanny.ToList())//remove all children for whom a signed contract has been found.
            {
                children.Remove(child);
            }
            return children;
        }
        #endregion

        #region BILL COST
        /// <summary>
        /// calculatets the nannies wage
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        public decimal CalculateBill(Boolean choice, Contract contract)
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
#endregion

        #region FITTNESS
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
        #endregion

        #region SORTING
        /// <summary>
        /// checks if the first element is bigger (via stars->reffers to nanny) than the second
        /// </summary>
        /// <param name="first">the object we check</param>
        /// <param name="second">the object we used to compare</param>
        /// <returns></returns>

        public bool sortByStars(Nanny first, Nanny second)
        {
            return first.currentStars > second.currentStars ? true : false;
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


        #endregion

        #region EXIST
        public bool ParentExist(string id)
        {
            return dal.ParentExist(id);
        }

        public bool NannyExist(string id)
        {
            return dal.NannyExist(id);
        }

        public bool ChildExist(string id)
        {
            return dal.ChildExist(id);
        }

        public bool ContractExist(string Contractid)
        {
            return dal.ContractExist(Contractid);
        }

        #endregion

        #region ENTITY
        public Parent ParentEntity(string id)
        {
            return dal.ParentEntity(id);
        }

        public Nanny NannyEntity(string id)
        {
            return dal.NannyEntity(id);
        }

        public Child ChildEntity(string id)
        {
            return dal.ChildEntity(id);
        }

        public Contract ContractEntity(string Contractid)
        {
            return dal.ContractEntity(Contractid);
        }
        #endregion
    }
}
