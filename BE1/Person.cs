using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Person
    {
        #region Fields
        //Private Fields:
        private string firstNameP;
        private string lastNameP;
        private DateTime dateOfBirthP;
        private string emailP;
        private string landLinePhoneP;
        private string mobileP;
        private string IDP;

        //Public Fields:
        public String firstName
        {
            get { return firstNameP; }
            set
            {
                if (value.Length < 2)
                {
                    throw new Exception("The firstname is too short!\n");
                }
                firstNameP = value;
            }
        }
        public String lastName
        {
            get { return lastNameP; }
            set
            {
                if (value.Length < 2)
                {
                    throw new Exception("The last name is too short!\n");
                }
                lastNameP = value;
            }
        }
        public String ID
        {
            get { return IDP; }
            set
            {
                //Israeli ID number (we presume Israeli ID numbers cannot begin with 0)
                if (value.Length == 9)
                {
                    //Checks if ID is valid Israeli ID
                    IDP = (checkIDValidity(Convert.ToInt32(value))) ? value.ToString() : "0";
                    //Control digit of the ID was incorrect
                    if (IDP == "0")
                        throw new Exception("Error! ID number is not valid!\n (We currently only sell to Israeli ID holders ;-))\n");
                }
                //ID does not contain exactly 9 digits
                else
                    throw new Exception("Invalid ID! Israeli ID must contain exactly 9 digits!\n");
            }
        }
        public DateTime dateOfBirth
        {
            get { return dateOfBirthP; }
            set
            {
                //if the #year is bigger than the current number the person isn't born,
                //assume the year expressed in 4 digits
                if (value.Year > DateTime.Now.Year)
                {
                    throw new Exception("Invalid year!\n");
                }
                dateOfBirthP = value;
            }
        }

        //possible additions (for instance, 
        //child doesn't have phone or address by his own)
        public String email
        {
            get
            {
                return emailP;
            }
            set
            {
                if (!value.Contains("@") && !(value.Contains(".com") || value.Contains(".COM")))
                {
                    throw new Exception("Invalid e-mail address!\n");
                }
                emailP = value;
            }
        }
        public String landLinePhone
        {
            get { return landLinePhoneP; }
            set
            {
                //in israel the phone number length with prefix is about 9 digits
                if (value.Length < 10 || value.Length > 10)
                {
                    throw new Exception("Invalid phone number!\n");
                }
                landLinePhoneP = value;
            }
        }
        public String mobile
        {
            get { return mobileP; }
            set
            {
                //in israel the mobile-phone number length with prefix is about 10 digits
                if (value.Length < 10 || value.Length > 10)
                {
                    throw new Exception("Invalid phone number!\n");
                }
                mobileP = value;
            }
        }
        //the checking is in address 
        public Address personAddress { get; set; }
        //Returns current age (now minus date of birth)
        public TimeSpan currentAge
        {
            get
            {
                TimeSpan age = DateTime.Now - dateOfBirth;
                return age;
            }
        }
        #endregion

        //Methods
        //constructor
        public Person()
        {

        }
        public Person(String myFirstName, String myLastName, String myID, DateTime myDateOfBirth,
            String myEmail = null, String myLandLinePhone = null, String myMobile = null, Address myPersonAddress = null)
        {
            firstName = myFirstName;
            lastName = myLastName;
            ID = myID;
            dateOfBirth = myDateOfBirth;
            email = myEmail;
            landLinePhone = myLandLinePhone;
            mobile = myMobile;
            personAddress = myPersonAddress;
        }
        public override string ToString()
        {
            //Israela Israeli///////////////////////////////////////////////////
            //025807777
            //0541234567
            //Address:
            //27 Rue Yafo
            //91999 JERUSALEM
            //ISRAEL
            String address = personAddress == null ? null : personAddress.ToString();
            return String.Format(
                "{0} {1}\n" +
                (email != null ? "e-mail: {4}\n" : "") +
                (landLinePhone != null ? "Landline phone: {5}\n" : "") +
                (mobile != null ? "Mobile: {6}\n" : "") +
                (personAddress != null ? "Address:\n{7}\n" : "\n"),
                firstName, lastName, ID, dateOfBirth, email, landLinePhone, mobile, address);
        }

        //Checks validity of Israeli ID (checks if the control digit is correct for the given ID)
        public static bool checkIDValidity(int ID)
        {
            int first8 = ID / 10;
            int IDcontrolDigit = ID % 10;
            int currentTwoDigits, i, sum = 0;
            //This is how an Israeli ID control digit is calculated (including the line after the loop).
            for (i = 0; i < 4; first8 = first8 / 100, i++)
            {
                currentTwoDigits = first8 % 100;
                //Adds sum of digits of multiplication of every second digit by 2.
                sum += ((currentTwoDigits % 10) * 2) / 10 + ((currentTwoDigits % 10) * 2) % 10;
                sum += currentTwoDigits / 10;
            }
            int controlDigit = 10 - (sum % 10);
            //Checks if control digit of the ID received is indeed the correct control digit.
            if (IDcontrolDigit == controlDigit)
                return true;
            //The control digit is incorrect
            else
                return false;
        }
    }
}
