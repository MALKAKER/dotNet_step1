﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    
    public class Contract
    {
        #region Fields

        private decimal monthlyWageP;
        //contract id number initialized once when it created
        public String contractID;
        //if were an interview before
        public bool isInterview = false;
        //if the contract was signed
        public bool isContract = false;

        #endregion

        #region Attributes
        //the date the contract was generated
        public DateTime openRequest { get; set; }
        //parent
        public String ParentId { get; set; }
        //child id
        public String ChildId { get; set; }
        //nanny id
        public String NannyId { get; set; }
        //cost per month
        public decimal monthlyWage
        {
            get
            {
                return monthlyWageP;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception ("Wage can not be negative!\n");
                }
                
                monthlyWageP = value;
              
            }
        }
        
        //cost per hour
        public decimal hourWage{ get; set; }

        //mother choice about the wage
        Boolean motherChoice { get; set; }
        
        //if there is another children in the same nanny need check in the dal layer
        public bool isAnotherChild { get; set; }
        
        //the work schedule
        public Dictionary<DayOfWeek, KeyValuePair<int, int>> schedule { get; set; }
        
        //when the employment starts and when  it will be ended
        public KeyValuePair<DateTime, DateTime> DurationOfEmployment
        {
            get { return DurationOfEmploymentP; }
            set
            {
                if (value.Key >= value.Value)
                {
                    throw new Exception("Invalid duration\n");
                }
                DurationOfEmploymentP = value;
            }
        }
        
        //prop to get the work total hours
        public int totalHours
        {
            get
            {
                int num=0;
                foreach (KeyValuePair < DayOfWeek, KeyValuePair < int, int>> pair in schedule)
                {
                    num += pair.Value.Value - pair.Value.Key;
                }
                return num;
            }
        }

        public KeyValuePair<DateTime, DateTime> DurationOfEmploymentP { get; private set; }
        #endregion

        #region Function
        public override string ToString()
        {
            return String.Format("Contract number {0} between {1}'s parent and {2}", contractID,ChildId,NannyId);
        }

        public Contract()
        {

        }
        #endregion
    }
}
