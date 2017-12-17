using BE;
using System;
using System.Collections.Generic;


namespace BL
{
    //delegate for some conditions to search contract
    public delegate bool delegateCondition(Contract contract, Object opt = null);
    //delegate for some methods to sort the nannies
    public delegate bool delegateSort(Person first, Person second);
    
    public interface IBL
    {
        //Nanny functions:
        // ask the lecturer about the blah

        #region NANNY
        void addNanny(Nanny newNanny);
        Boolean removeNanny(String nannyId, bool eraseContracts = true);
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
        Boolean removeContract(String contractId);
        void updateContract(Contract contractToUpdate);
        #endregion

        //lists of data

        #region GET ALL
        List<Nanny> getAllNanny();

        //
        List<Parent> getAllParents();

        //return all the children according to parent
        List<Child> getAllChildren();

        //get lists of parents and return their children
        List<Child> getAllChildren(List<Parent> parents);

        List<Contract> getAllContracts();
        #endregion

        //existance

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
        
        //retreive

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

        //Another methods to BL:
        
        #region ANOTHER METHODS
        //calculate bill
        decimal CalculateBill(Boolean choice, Contract contract);
        //matches optional nannies to parent according to specific constrains
        List<Nanny> initialMatch(Parent parent, Gender gender, List<SKILLS> skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars, String childId = null);

        //(help function to betterMatchNanny) matches optional nannies to parent according to area?
        Dictionary<int, List<Nanny>> nanniesNearby(Parent parent, float maxDistance);

        //matches optional nannies to parent according to specific constrains and area
        List<Nanny> betterMatchNanny(Address area, Parent parent, Gender gender, List<SKILLS> skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars, String childId = null);

        //returns children that still dont have nanny
        List<Child> childrenWithNoNanny();

        //returns the nannies vication
        List<Nanny> tamatVacation();

        //returns list of contracts that fit to some conditions
        List<Contract> specificContracts(Predicate<Contract> condition);

        //returns the number of the contracts that fit to the condition
        int numberOfContracts(Predicate<Contract> condition);

        //methods that returns lists grouping by some conditions:

        //nannies grouping by age
        Dictionary<int, List<Nanny>> nannyAge(Boolean isSort =false, Func<Nanny, int> sort = null);
        Dictionary<int, List<Nanny>> nannyAge(List<Nanny> nan, Boolean isSort = false, Func<Nanny, int> sort = null);

        //nannies grouping by address
        Dictionary<int, List<Nanny>> nannyAddress(Address loc,  Boolean isSort = false, Func<Nanny, float> sort = null, float? kilometres = null);
        Dictionary<int, List<Nanny>> nannyAddress(List<Nanny> nan, Address loc, Boolean isSort = false, Func<Nanny, float> sort = null, float? kilometres = null);

        //nannies grouping by launguage
        Dictionary<Language, List<Nanny>> nannyLanguage(Boolean isSort = false, Func<Nanny,int> sort = null);
        Dictionary<Language, List<Nanny>> nannyLanguage(List<Nanny> nan, Boolean isSort = false, Func<Nanny, int> sort = null);

        //nannies grouping by lift
        Dictionary<bool, List<Nanny>> nannyLift(Boolean isSort = false, Func<Nanny, float> sort = null);
        Dictionary<bool, List<Nanny>> nannyLift(List<Nanny> nan, Boolean isSort = false, Func<Nanny, float> sort = null);

        //the distance from the parents location to the nannie's
        Dictionary<int, List<Contract>> contractDistance(bool isSort = false, Func<Contract, int> sort = null);
        Dictionary<int, List<Contract>> contractDistance(List<Contract> cont, bool isSort = false, Func<Contract, int> sort = null);

#endregion
    }
}
