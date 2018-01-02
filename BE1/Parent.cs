using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Parent : Person
    {
        #region Fields
        private Dictionary<DayOfWeek, KeyValuePair<int, int>> parentWorkhoursP;
        private List<string> childrenIdP;
        private String passwordP;
        #endregion

        #region Attributes

        public String password 
        {
            get {return passwordP;}
            set { passwordP = value; }
        }

        public Dictionary<DayOfWeek, KeyValuePair<int, int>> parentWorkhours
        {
            get { return parentWorkhoursP; }
            set
            {
                //foreach (var day in value)
                //    this.parentWorkhoursP.Add(day.Key, new KeyValuePair<int, int>(day.Value.Key, day.Value.Value));
                parentWorkhoursP = value;

            }
        }
        
        //here I think we need google maps API, and the check of the input is in the ADDRESS
        public Address areaToSearchNanny{ get;set; }
        
        
        
        //the parent kids who need nanny
        public List<String> childrenId
        {
            get { return childrenIdP; }
            set
            {
                String tmp;
                foreach (string child in value)
                {
                    //Israeli ID number (we presume Israeli ID numbers cannot begin with 0)
                    if (child.Length == 9)
                    {
                        //Checks if ID is valid Israeli ID
                        tmp = (checkIDValidity(Convert.ToInt32(child))) ? child.ToString() : "0";
                        //Control digit of the ID was incorrect
                        if (tmp == "0")
                            throw new Exception("Error! ID number is not valid!\n (We currently only sell to Israeli ID holders ;-))\n");
                    }
                    //ID does not contain exactly 9 digits
                    else
                        throw new Exception("Invalid ID! Israeli ID must contain exactly 9 digits!\n");

                    childrenIdP = value;
                    
                    
                }
            }
        }
        #endregion
        
        #region Functions
        
        public Parent(String myFirstName, String myLastName, String myID, DateTime myDateOfBirth,
            String myEmail, String myLandLinePhone, String myMobile, Address myPersonAddress, Dictionary<DayOfWeek, KeyValuePair<int, int>> myParentWorkhours,
            List<String> myChildrenId, Address myAreaToSearchNanny = null
            ) :base(myFirstName, myLastName, myID, myDateOfBirth,
             myEmail, myLandLinePhone, myMobile, myPersonAddress)
        {
            parentWorkhours = new Dictionary<DayOfWeek, KeyValuePair<int, int>>();
            parentWorkhours = myParentWorkhours;
            areaToSearchNanny = myAreaToSearchNanny == null ? myPersonAddress: myAreaToSearchNanny;
            
            childrenId = myChildrenId;
        }
        //empty constructor
        public Parent() : base()
        {
            parentWorkhours = new Dictionary<DayOfWeek, KeyValuePair<int, int>>();
            areaToSearchNanny = null;
        }
        //toString:
        public override string ToString()
        {
            //childrenId needs a new tostring
            int tmp = base.ToString().IndexOf("Address:");
            return String.Format("{0}"+
                "Number of children in the current nanny: {1}\n{2}",
                base.ToString().Substring(0,tmp), childrenId.Count, base.ToString().Substring(tmp));
        }
        #endregion
    }
}
