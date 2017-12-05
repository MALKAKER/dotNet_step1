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
        
        //return all the children according to parent
        List<Child> getAllChildren();

        //get lists of parents and return their children
        List<Child> getAllChildren(List<Parent> parents);
        
        List<Contract> getAllContracts();

        //Another methods to BL:

        //matches optional nannies to parent according to specific constrains
        List<Nanny> initialMatch(Parent parent, Gender gender, String skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars);

        //(help function to betterMatchNanny) matches optional nannies to parent according to area?
        Dictionary<int, List<Nanny>> nanniesNearby(Parent parent, float maxDistance);

        //matches optional nannies to parent according to specific constrains and area
        List<Nanny> betterMatchNanny(Address area, Parent parent, Gender gender, String skill, List<Language> languages, int minExpYears, Specialization spec, decimal maxCostPerHour, bool liftInBuilding, bool tamatHoliday, float minStars);

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

        //nannies grouping by address
        Dictionary<int, List<Nanny>> nannyAddress(Address loc, float kilometres, Boolean isSort = false, Func<Nanny, float> sort = null);

        //nannies grouping by launguage
        Dictionary<Language, List<Nanny>> nannyLanguage(Boolean isSort = false, Func<Nanny,int> sort = null);

        //nannies grouping by lift
        Dictionary<bool, List<Nanny>> nannyLift(Boolean isSort = false, Func<Nanny, float> sort = null);

        //the distance from the parents location to the nannie's
        Dictionary<int, List<Contract>> contractDistance(bool isSort = false, Func<Contract, int> sort = null);
        
    }
}
