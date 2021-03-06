﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{

    public class Nanny : Person
    {
        //Feilds

        public Gender nannyGender { get; set; }
        public List<Language> nannyLanguage { get; set; }
        public Specialization workField { get; set; }
        public bool isLift { get; set; }
        public int expYears
        {
            get
            {
                return expYears;
            }
            set
            {
                //years cant be negative
                if (value < 0)
                {
                    throw new Exception("Invalid years of experience!");
                }
                expYears = value;
            }
        }
        public List<String> nannySkills { get; set; }//TODO need to conver to enum!
        public int maxChildren
        {
            get { return maxChildren; }
            set
            {
                if (value < 2)
                {
                    throw new Exception("Invalid max children!\n");
                }
                maxChildren = value;
            }
        }
        public int minAge
        {
            get { return minAge; }
            set
            {
                //the minimum should be consistent to the max age
                if (value > maxAge && value < 0)
                {
                    throw new Exception("Invalid Minimum age!\n");
                }
                minAge = value;
            }
        }
        public int maxAge
        {
            get { return maxAge; }
            set
            {
                //no matter because the first check occurs, but for safety
                if (value < minAge && value < 0)
                {
                    throw new Exception("Invalid Maximum age!\n");
                }
                maxAge = value;
            }
        }
        public decimal costPerHour
        {
            get { return costPerHour; }
            set
            {

                if (value < 0)
                {
                    throw new Exception("Invalid cost per hour!\n");
                }
                costPerHour = value;
            }
        }
        public decimal costPerMonth
        {
            get { return costPerMonth; }
            set
            {
                //need to check if the monthly payment is cheaper from the hourly  payment
                if (value < 0)
                {
                    throw new Exception("Invalid cost per month!\n");
                }
                costPerMonth = value;
            }
        }
        public Dictionary<DayOfWeek, KeyValuePair<int, int>> workhours
        {
            get { return workhours; }
            set
            {
                //need to ask the lecturer
            }
        }//TODO change from int to datetime
        public bool isVacation { get; set; }//TODO...
        public List<String> recommendations { get; set; }
        public static int[] stars = new int[5] {0, 0, 0, 0, 0};
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
        public BankAccount nannyAccount{ get; set; }

        //Functions

        //constructor

        public Nanny(String myFirstName, String myLastName, String myID, DateTime myDateOfBirth,
            String myEmail, String myLandLinePhone, String myMobile, Address myPersonAddress,Gender myNanyGender, List<Language> myNannyLanguage, Specialization myWorkField,
            bool myIsLift, int myExpYears, List<String> myNannySkills, int myMaxChildren, int myMinAge, int myMaxAge,
            decimal myCostPerHour, decimal myCostPerMonth, Dictionary<DayOfWeek, KeyValuePair<int, int>> myWorkhours,
            bool myVacation, List<String> myRecommendations, double myStars, BankAccount myNannyAccount)
            :base( myFirstName, myLastName, myID, myDateOfBirth,
             myEmail, myLandLinePhone, myMobile, myPersonAddress)
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
            workhours = myWorkhours;
            isVacation = myVacation;
            recommendations = myRecommendations;
            stars = myStars;
            nannyAccount = myNannyAccount;
        }
        
        

                                

        //to string

        public override string ToString() 
        {
            return String.Format("{0,3}\nLanguage: {1,3}Occupation: {2,3}\nCost:\nPer month: {4}\nPer hour: {3}",
                base.ToString(),nannyLanguage,
                costPerHour,costPerMonth);
        }
    }
}
