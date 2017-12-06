using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Child : Person
    {
        //Fields

        //the parent of the child
        //will update from the parent class and the checking is there

        #region Fields:
        /*      private String parentIdP;
                private Dictionary<ChildInfo, String> childDetailsP;
                private List<Language> childLanguageP;
                private HMO childHMOP;
                private bool specialNeedsP;*/

        public String parentId { get; set; }
        //if the child has special needs
        public bool specialNeeds = false;
        //information about the child
        public Dictionary<ChildInfo, String> childDetails { get; set; }
        //the language the child speaks
        public List<Language> childLanguage { get; set; }
        // the HMO the child registered to
        public HMO childHMO { get; set; }
        #endregion

        //Function

        public override string ToString()
        {
            return String.Format("{0}\nLanguage: {1}\n" + (specialNeeds ? "Special Needs child\n" : "\n"), base.ToString(), childLanguage[0].ToString());
        }
        //constructor
        public Child(String myFirstName, String myLastName, String myID, DateTime myDateOfBirth,
            String myParentId, bool mySpecialNeeds, Dictionary<ChildInfo, String> myChildDetails, List<Language> myChildLanguage,
            HMO myChildHMO)
            : base(myFirstName, myLastName, myID, myDateOfBirth)
        {
            parentId = myParentId;
            specialNeeds = mySpecialNeeds;
            childDetails = myChildDetails;
            childLanguage = myChildLanguage;
            childHMO = myChildHMO;
        }
        //empty ctor
        public Child()
        {

        }
    }
}
