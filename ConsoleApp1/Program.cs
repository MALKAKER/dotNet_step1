using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IBL mybl = FactoryBL.Instance;
                Dictionary<DayOfWeek, KeyValuePair<int, int>> tmpDate = new Dictionary<DayOfWeek, KeyValuePair<int, int>>
                {
                    { DayOfWeek.Monday, new KeyValuePair<int, int>(0700, 1600) }
                };
                List<String> tmpChildren = new List<String>();
                tmpChildren.Add("316301191");
                //tmpChildren.Add("2010");
                Address address = new Address
                {
                    house = "22a",
                    city = "Lud",
                    country = "Israel",
                    addressLine2 = "what is it??",
                    flat = "7",
                    floor = "-3.5",
                    postCode = "1234567",
                    street = "vered"
                };
                BankAccount bank = new BankAccount
                {
                    bankName = Bank.Discount,
                    branchAddress = address,
                    accountNumber = 12345678,
                    branchNumber = 290
                    
                };
                Person person = new Person
                {
                    dateOfBirth = new DateTime(1996, 11 ,2),
                    email = "malkiaker@gmail.com",
                    mobile = "0584465913",
                    personAddress = address,
                    landLinePhone = "02-5807777",
                    ID = "316301191",
                    firstName = "ISRAELA",
                    lastName = "ISRAELI"
                };
                Parent parent = new Parent
                {
                    dateOfBirth = new DateTime(1996, 11, 2),
                    email = "malkiaker@gmail.com",
                    mobile = "0584465913",
                    personAddress = address,
                    landLinePhone = "02-5807777",
                    ID = "316301191",
                    firstName = "ISRAELA",
                    lastName = "ISRAELI",
                    childrenId = tmpChildren,
                    areaToSearchNanny = address,
                    firstNameAnotherParent = "ISRAEL",
                    lastNameAnotherParent = "ISRAELI",
                    mobileAnotherParent = "0545337060",
                    parentWorkhours = tmpDate
                     
                };
                List<Language> temp = new List<Language>();
                temp.Add(Language.Arabic);
                temp.Add(Language.English);
                List<String> temp2 = new List<String>();
                temp2.Add("Blah blah");
                List<String> temp3 = new List<String>();
                temp3.Add("great nanny!");
                temp3.Add("Stupid nanny!");
                Dictionary<DayOfWeek, KeyValuePair<int, int>> tempDic = new Dictionary<DayOfWeek, KeyValuePair<int, int>>();
                tempDic.Add(DayOfWeek.Sunday, new KeyValuePair<int, int>(8, 12));
                Nanny nanny = new Nanny
                {
                    expYears = 2,
                    maxChildren = 3,
                    minAge = 1,
                    maxAge = 8,
                    costPerHour = 25,
                    costPerMonth = 1000,
                    currentStars = 4,
                    dateOfBirth = new DateTime(1996, 11, 2),
                    email = "malkax2@gmail.com",
                    firstName = "Malka",
                    ID = "201046869",
                    isLift = true,
                    isVacation = true,
                    landLinePhone = "02-5847689",
                    lastName = "Isacker",
                    mobile = "0657849375",
                    nannyAccount = bank,
                    nannyGender = Gender.female,
                    nannyLanguage = temp,
                    nannySkills = temp2,
                    personAddress = address,
                    recommendations = temp3,
                    workField = Specialization.Babysitter,
                    workhours = tempDic
                };
                
                List<Language> temppp = new List<Language>();
                temppp.Add(Language.Hebrew);
                Child child = new Child
                {
                    childDetails = new Dictionary<ChildInfo, string>(),
                    childHMO = HMO.Maccabi,
                    childLanguage = temppp,
                    dateOfBirth = new DateTime(2016, 3, 12),
                    firstName = "Malkkkkk",
                    ID = "201046869",
                    lastName = "Katzenelenbogenstein",
                    parentId = "201046869",
                    specialNeeds = true
                };
                Contract contract = new Contract
                {
                    ChildId = child.ID,
                    contractID = "4566666",
                    DurationOfEmployment = new KeyValuePair<DateTime, DateTime>(new DateTime(2000, 11, 2), new DateTime(2005, 11, 2)),
                    hourWage = 90,
                    isAnotherChild = true,
                    isContract = true,
                    isInterview = true,
                    monthlyWage = 9000,
                    NannyId = nanny.ID,
                    ParentId = nanny.ID,
                    schedule = nanny.workhours
                };
                Console.WriteLine("Address:" +
                    address.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Bank account:" +
                    bank.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Person:" +
                    person.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Parent: " +
                    parent.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Nanny: " +
                    nanny.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Child: " +
                    child.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Contract: " +
                    contract.ToString());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                //mybl.specificContracts(c => c.ChildId == "5");
                //mybl.specificContracts(c => c.ChildId == "5");
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "/n");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
