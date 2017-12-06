using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Contract
    {
       
        //constructor todo

        
        //contract id number initialized once when it created
        public String contractID;
        //

        //child id
        public String ChildId { get; set; }
        //child id
        public String NannyId { get; set; }
        //if were an interview before
        public bool isInterview = false;
        //if the contract was signed
        public bool isContract = false;
        //cost per month
        public decimal monthlyWage
        {
            get
            {
                return monthlyWage;
            }
            set
            {
                if (monthlyWage <= 0)
                {
                    throw new Exception ("Wage can not be negative!\n");
                }
                //another child discount
                if (isAnotherChild)
                {
                    monthlyWage = 0.98m * value;
                }
                else
                {
                    monthlyWage = value;
                }
            }
        }
        //cost per hour
        public decimal hourWage
        {
            get { return hourWage; }
            set
            {
                if (isAnotherChild)
                {
                    hourWage = value * totalHours * 0.98m;
                }
                else
                { hourWage = value * totalHours; }
                
            }
        }
        //if there is another children in the same nanny need check in the dal layer
        public bool isAnotherChild { get; set; }
        //the work schedule
        public Dictionary<DayOfWeek, KeyValuePair<int, int>> schedule { get; set; }
        //when the employment starts and when  it will be ended
        public KeyValuePair<DateTime, DateTime> DurationOfEmployment
        {
            get { return DurationOfEmployment; }
            set
            {
                if (value.Key >= value.Value)
                {
                    throw new Exception("Invalid duration\n");
                }
               
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

        //Function
        public override string ToString()
        {
            return String.Format("");
        }
    }
}
