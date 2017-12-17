using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Nanny : Person
    {
        #region Fields
        //Private Fields:
        private int expYearsP;
        private int maxChildrenP;
        private int minAgeP;
        private int maxAgeP;
        private decimal costPerHourP;
        private decimal costPerMonthP;
        private Dictionary<DayOfWeek, KeyValuePair<int, int>> workhoursP;
        private String passwordP;

        //Public Fields:
        public String aboutMe = null; // Optional -The nanny tells about herself
        public String clipAboutMe = null; // Optional - clip about the nanny
        public String password
        {
            get { return passwordP; }
            set { passwordP = value; }
        }
        public Gender nannyGender { get; set; }
        public List<Language> nannyLanguage { get; set; }
        public Specialization workField { get; set; }
        public bool isLift { get; set; }
        public int expYears
        {
            get
            {
                return expYearsP;
            }
            set
            {
                //years can't be negative
                if (value < 0)
                {
                    throw new Exception("Invalid years of experience!");
                }
                expYearsP = value;
            }
        }
        public List<SKILLS> nannySkills { get; set; }//TODO need to convert to enum!
        public int maxChildren
        {
            get { return maxChildrenP; }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Invalid max children!\n");
                }
                maxChildrenP = value;
            }
        }
        public int minAge
        {
            get { return minAgeP; }
            set
            {
                //the minimum should be consistent to the max age
                if ((value > maxAge) && (maxAge !=0 ) || value < 0)
                {
                    throw new Exception("Invalid Minimum age!\n");
                }
                minAgeP = value;
            }
        }
        public int maxAge
        {
            get { return maxAgeP; }
            set
            {
                //no matter because the first check occurs, but for safety
                if ((value < minAge) || value < 0)
                {
                    throw new Exception("Invalid Maximum age!\n");
                }
                maxAgeP = value;
            }
        }
        public decimal costPerHour
        {
            get { return costPerHourP; }
            set
            {

                if (value < 0)
                {
                    throw new Exception("Invalid cost per hour!\n");
                }
                costPerHourP = value;
            }
        }
        public decimal costPerMonth
        {
            get { return costPerMonthP; }
            set
            {
                //need to check if the monthly payment is cheaper from the hourly  payment
                if (value < 0)
                {
                    throw new Exception("Invalid cost per month!\n");
                }
                costPerMonthP = value;
            }
        }
        public Dictionary<DayOfWeek, KeyValuePair<int, int>> workhours
        {
            get { return workhoursP; }
            set
            {
                //foreach (var day in value)
                //    this.workhoursP.Add(day.Key, new KeyValuePair<int, int>(day.Value.Key, day.Value.Value));
                workhoursP = value;
            }
        }//TODO change from int to datetime
        public bool isVacation { get; set; }//TODO...//////////////////////////////////////////////////////////////////////
        public List<String> recommendations { get; set; }
        public int[] stars = new int[5] { 0, 0, 0, 0, 0 };
        public float currentStars
        {
            get
            {
                float average = 0;
                int num = 0;
                for (int i = 1; i <= stars.Length; i++)
                {
                    average += stars[i] * i;
                    num += stars[i];
                }
                average /= num;
                return average;
            }
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new Exception("Invalid number of stars!\n");
                }
                stars[(int)value]++;
            }
        }
        public BankAccount nannyAccount { get; set; }
        #endregion

        //Functions:
        #region Constructors
        public Nanny() : base()
        {
            workhours = new Dictionary<DayOfWeek, KeyValuePair<int, int>>();
        }
        public Nanny(String myFirstName, String myLastName, String myID, DateTime myDateOfBirth,
            String myEmail, String myLandLinePhone, String myMobile, Address myPersonAddress, Gender myNanyGender, List<Language> myNannyLanguage, Specialization myWorkField,
            bool myIsLift, int myExpYears, List<SKILLS> myNannySkills, int myMaxChildren, int myMinAge, int myMaxAge,
            decimal myCostPerHour, decimal myCostPerMonth, Dictionary<DayOfWeek, KeyValuePair<int, int>> myWorkhours,
            bool myVacation, List<String> myRecommendations, BankAccount myNannyAccount)
            : base(myFirstName, myLastName, myID, myDateOfBirth, myEmail, myLandLinePhone, myMobile, myPersonAddress)
        {
            nannyGender = myNanyGender;
            nannyLanguage = myNannyLanguage;
            workField = myWorkField;
            isLift = myIsLift;
            expYears = myExpYears;
            nannySkills = myNannySkills;
            maxChildren = myMaxChildren;
            minAge = myMinAge;
            maxAge = myMaxAge;
            costPerHour = myCostPerHour;
            costPerMonth = myCostPerMonth;
            workhours = new Dictionary<DayOfWeek, KeyValuePair<int, int>>();
            workhours = myWorkhours;
            isVacation = myVacation;
            recommendations = myRecommendations;
            nannyAccount = myNannyAccount;
        }
        #endregion

        //ToString():
        public override string ToString()
        {
            return String.Format("{0,3}\nLanguage: {1,3}\nCost:\nPer month: {3}\nPer hour: {2}",
                base.ToString(), nannyLanguage[0].ToString(), costPerHour.ToString(), costPerMonth.ToString());
        }
    }
}