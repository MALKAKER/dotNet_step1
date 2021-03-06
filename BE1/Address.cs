﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BE
{
    public class Address
    {
        #region Feilds
        private string cityP;
        private string postCodeP;
        private string streetP;
        private string houseP;
        private string flatP;

        #endregion

        #region Attributes
        public String country{ get; set; }
        //public String county{ get; set; }
  
        public String city
        {
            get
            {
                return cityP;
            }
            set
            {
                //Console.WriteLine("kuku");
                if (value.Any(char.IsDigit))
                {
                    throw new Exception("Invalid city!\n");
                }
                
                cityP =  char.ToUpper(value[0]) + value.Substring(1).ToLower();
            }
        }
        public String postCode
        {
            get
            {
                return postCodeP;
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
                postCodeP = value;
            }
        }
        public String street
        {
            get { return streetP; }
            set
            {
                if (value.Any(char.IsDigit))
                {
                    throw new Exception("Invalid street!\n");
                }
                streetP = value;
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
            get { return houseP; }
            set
            {
                if (value.Contains("-"))
                {
                    throw new Exception("Invalid house number");
                }
                houseP = value;
            }
        }
        //There is no exeption because there is a number of building like 3A, and its string because 
        //there are private houses
        public string flat
        {
            get { return flatP; }
            set
            {
                if (value.Contains("-"))
                {
                    throw new Exception("Invalid flat number");
                }
                flatP = value;
            }
        }
        //there is a negative floor or with letter
        public string floor
        {
            get;
            set;
        }
        #endregion

        #region Methods:

        //Constructors:
        public Address(String myCountry, String myCity, String myPostCode, String myStreet, string myHouse, String myAddressLine2=null, string myFlat=null, string myFloor=null)
        {
            UpdateAddress( myCountry,  myCity,  myPostCode,  myStreet,  myHouse,  myAddressLine2,  myFlat,  myFloor);
        }
        public Address() { }

        //Update function (updates class details):
        internal void UpdateAddress(String myCountry, String myCity, String myPostCode, String myStreet, string myHouse,  String myAddressLine2/* = null*/, string myFlat/* = null*/, string myFloor/* = null*/)
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

            return String.Format((flat!=null?"{0}/{1}":"{0}")+" {2}\n{3} {4}\n{5}", house, flat, street, postCode, city, country);
        }
        #endregion
    }
}