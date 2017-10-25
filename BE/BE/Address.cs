using System;
using System.Collections.Generic;
using System.Text;
namespace BE
{
    public class Address
    {
        //Fields:
        public String country{ get; set; }
        //public String county{ get; set; }
        public String city
        {
            get
            {
                return city;
            }
            set
            {
                if (value.Contains("1") || value.Contains("2") || value.Contains("3")
                    || value.Contains("4") || value.Contains("5") || value.Contains("6") 
                    || value.Contains("7") || value.Contains("8") || value.Contains("9") 
                    || value.Contains("0"))
                {
                    throw new Exception("Invalid city!\n");
                }
                
                city =  char.ToUpper(value[0]) + value.Substring(1).ToLower();
            }
        }
        public String postCode
        {
            get
            {
                return postCode;
            }
            set
            {
                if (value.Length != 7)
                {
                    if (value.Length != 5)
                    {
                        throw new Exception("Invalid post code!\n");
                    }
                    throw new Exception("Invalid post code!\nPost codes now contain 7 digits.\n");
                }
                postCode = value;
            }
        }
        public String street
        {
            get { return street; }
            set
            {
                if (value.Contains("1") || value.Contains("2") || value.Contains("3")
                    || value.Contains("4") || value.Contains("5") || value.Contains("6")
                    || value.Contains("7") || value.Contains("8") || value.Contains("9")
                    || value.Contains("0"))
                {
                    throw new Exception("Invalid street!\n");
                }
                street = value;
            }
        }
        public String addressLine2
        {
            get;
            set;
        }
        //There is no exeption because there is a number of building like 12A
        public string house
        {
            get;
            set;
        }
        //There is no exeption because there is a number of building like 3A, and its string because 
        //there are private houses
        public string flat
        {
            get;
            set;
        }
        //there is a negative floor or with letter
        public string floor
        {
            get;
            set;
        }


        //Methods:

        //Constructors:
        public Address(String myCountry, String myCity, String myPostCode, String myStreet, string myHouse,/* String myCounty=null,*/ String myAddressLine2=null, string myFlat=null, string myFloor=null)
        {
            UpdateAddress(myStreet, myPostCode, myHouse, myFloor, myFlat, /*myCounty,*/ myCountry, myCity, myAddressLine2);
        }
        public Address() { }

        //Update function (updates class details):
        internal void UpdateAddress(String myCountry, String myCity, String myPostCode, String myStreet, String myHouse, /*String myCounty = null,*/ String myAddressLine2 = null, string myFlat = null, string myFloor = null)
        {
            country = myCountry;
            //county = myCounty;
            city = myCity;
            postCode = myPostCode;
            street = myStreet;
            addressLine2 = myAddressLine2;
            house = myHouse;
            flat = myFlat;
            floor = myFloor;
        }

        public void UpdateAddress(Address newAddress)
        {
            country = newAddress.country;
            //county = myCounty;
            city = newAddress.city;
            postCode = newAddress.postCode;
            street = newAddress.street;
            addressLine2 = newAddress.addressLine2;
            house = newAddress.house;
            flat = newAddress.flat;
            floor = newAddress.floor;
        }
        public override string ToString()
        {
            //address format in Israel
            //27 Rue Yafo
            //91999 JERUSALEM
            //ISRAEL
            return String.Format(flat!=null?"{0}/{1}":"{0}"+" {2}\n{3} {4}\n{5}", house, flat, street, postCode, city, country);
        }
    }
}