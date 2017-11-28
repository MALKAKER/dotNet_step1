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

        //another methods to BL:

        //matches optional nannies to parent according to specific constrains
        List<Nanny> matchNanny(Parent parent);

        //(help function to betterMatchNanny) matches optional nannies to parent according to area?
        List<Nanny> matchNanny(Address area);

        //matches optional nannies to parent according to specific constrains and area
        List<Nanny> betterMatchNanny(Address area, Parent parent);

        //returns children that still dont have nanny
        List<Child> noNanny();

        //returns the nannies vication
        List<Nanny> tamatVacation();

        //returns list of contracts that fit to some conditions
        List<Contract> specificContracts(Predicate<Contract> condition);

        //returns the number of the contracts that fit to the condition
        int numberOfContracts(delegateCondition condition);

        //methods that returns lists grouping by some conditions:

        //nannies grouping by age
        List<Nanny> nannyAge(Boolean isSort =false, delegateSort someSort = null);
        
        //nannies grouping by address
        List<Nanny> nannyAddress(Address loc, Boolean isSort = false, delegateSort someSort = null);
        //nannies grouping by launguage
        List<Nanny> nannyLanguage(Boolean isSort = false, delegateSort someSort = null);
        //nannies grouping by lift
        List<Nanny> nannyLift(Boolean isSort = false, delegateSort someSort = null);
        
        //list of banck branches?
        List<Contract> contractDistance(Boolean isSort = false, delegateSort someSort = null);
        
    }
}
