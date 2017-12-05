using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Parent : Person
    {
        //Fields
        public Dictionary<DayOfWeek, KeyValuePair<int, int>> parentWorkhours
        {
            get { return parentWorkhours; }
            set
            {
                foreach (var day in value)
                    this.parentWorkhours.Add(day.Key, new KeyValuePair<int, int>(day.Value.Key, day.Value.Value));

            }
        }
        //here I think we need google maps API, and the check of the input is in the ADDRESS
        public Address areaToSearchNanny{ get;set; }
        //another parent details (optional)
        public String firstNameAnotherParent
        {
            get { return firstNameAnotherParent; }
            set
            {
                if (value.Length < 2)
                {
                    throw new Exception("The firstname is too short!\n");
                }
                firstNameAnotherParent = value;
            }
        }
        public String lastNameAnotherParent
        {
            get { return lastNameAnotherParent; }
            set
            {
                if (value.Length < 2)
                {
                    throw new Exception("The lastname is too short!\n");
                }
                lastNameAnotherParent = value;
            }
        }
        public String mobileAnotherParent
        {
            get { return mobile; }
            set
            {
                //in israel the mobile-phone number length with prefix is about 10 digits
                if (value.Length < 11 || value.Length > 11)
                {
                    throw new Exception("Invalid phone number!\n");
                }
                mobileAnotherParent = value;
            }
        }
        //the parent kids who need nanny
        public List<String> childrenId
        {
            get { return childrenId; }
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
                    childrenId.Add(tmp);
                }
            }
        }

        //Functions
        
        public Parent(String myFirstName, String myLastName, String myID, DateTime myDateOfBirth,
            String myEmail, String myLandLinePhone, String myMobile, Address myPersonAddress, Dictionary<DayOfWeek, KeyValuePair<int, int>> myParentWorkhours,
            List<String> myChildrenId, Address myAreaToSearchNanny = null, String myFirstNameAnotherParent=null, String myLastNameAnotherParent=null, 
            String myMobileAnotherParent=null) :base(myFirstName, myLastName, myID, myDateOfBirth,
             myEmail, myLandLinePhone, myMobile, myPersonAddress)
        {
            parentWorkhours = new Dictionary<DayOfWeek, KeyValuePair<int, int>>();
            parentWorkhours = myParentWorkhours;
            areaToSearchNanny = myAreaToSearchNanny == null ? myPersonAddress: myAreaToSearchNanny;
            firstNameAnotherParent = myFirstNameAnotherParent;
            lastNameAnotherParent = myLastNameAnotherParent;
            mobileAnotherParent = myMobileAnotherParent;
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
            return String.Format("{0}\n"+firstNameAnotherParent!=null ? "Another parent:\n {1} {2}\nMobile: {3}":""+
                "Children in the current nanny: {4}\n",
                base.ToString().Substring(0,tmp), firstNameAnotherParent,
                lastNameAnotherParent, mobileAnotherParent, childrenId.ToString(), base.ToString().Substring(tmp));
        }
    }
}
