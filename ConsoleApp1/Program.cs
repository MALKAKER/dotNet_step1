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
                #region INIT
                IBL mybl = FactoryBL.Instance;
                Dictionary<DayOfWeek, KeyValuePair<int, int>> tmpDate = new Dictionary<DayOfWeek, KeyValuePair<int, int>>
                {
                    { DayOfWeek.Monday, new KeyValuePair<int, int>(0700, 1600) }
                };
                List<String> tmpChildren = new List<String>();
                tmpChildren.Add("201046869");
                tmpChildren.Add("316301191");
                //tmpChildren.Add("2010");
                Address address = new Address
                {
                    house = "7",
                    city = "Jerusalem",
                    country = "Israel",
                    addressLine2 = "what is it??",
                    flat = "7",
                    floor = "-3.5",
                    postCode = "1234567",
                    street = "Beit Ha-Defus"
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
                Parent parent1 = new Parent
                {
                    dateOfBirth = new DateTime(1996, 11, 2),
                    email = "malkiaker@gmail.com",
                    mobile = "0584465913",
                    personAddress = address,
                    landLinePhone = "02-5807777",
                    ID = "201046869",
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
                List<SKILLS> temp2 = new List<SKILLS>();
                temp2.Add(SKILLS.Calm);
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
                    isLift = false,
                    isVacation = false,
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
                temp.Add(Language.French);
                temp.Reverse();
                Nanny nanny1 = new Nanny
                {
                    expYears = 2,
                    maxChildren = 3,
                    minAge = 2,
                    maxAge = 9,
                    costPerHour = 25,
                    costPerMonth = 1000,
                    currentStars = 4,
                    dateOfBirth = new DateTime(1996, 11, 2),
                    email = "malkax2@gmail.com",
                    firstName = "Malka",
                    ID = "316301191",
                    isLift = true,
                    isVacation = true,
                    landLinePhone = "02-5847689",
                    lastName = "Isacker",
                    mobile = "0657849375",
                    nannyAccount = bank,
                    nannyGender = Gender.female,
                    nannyLanguage = temp,
                    nannySkills = temp2,
                    personAddress = new Address
                    {
                        house = "9",
                        city = "Beitar Illit",
                        country = "Israel",
                        addressLine2 = "what is it??",
                        flat = "7",
                        floor = "-3.5",
                        postCode = "1234567",
                        street = "HaRav Ya'akov Mutsafi"
                    },
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
                Child child1 = new Child
                {
                    childDetails = new Dictionary<ChildInfo, string>(),
                    childHMO = HMO.Maccabi,
                    childLanguage = temppp,
                    dateOfBirth = new DateTime(2016, 3, 12),
                    firstName = "Malkkkkk",
                    ID = "316301191",
                    lastName = "Katzenelenbogenstein",
                    parentId = "316301191",
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
                    ParentId = parent.ID,
                    schedule = nanny.workhours
                };
                Contract contract1 = new Contract
                {
                    ChildId = child1.ID,
                    contractID = "4566666",
                    DurationOfEmployment = new KeyValuePair<DateTime, DateTime>(new DateTime(2000, 11, 2), new DateTime(2005, 11, 2)),
                    hourWage = 90,
                    isAnotherChild = true,
                    isContract = true,
                    isInterview = true,
                    monthlyWage = 9000,
                    NannyId = nanny1.ID,
                    ParentId = parent1.ID,
                    schedule = nanny1.workhours
                };
                #endregion

                #region CHECK TOSTRING

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
                #endregion
                
                #region ADD 
                mybl.addNanny(nanny);
                mybl.addNanny(nanny1);
                
                mybl.addParent(parent);
                mybl.addParent(parent1);
                
                mybl.addChild(child);
                mybl.addChild(child1);
                
                mybl.addContract(contract);
                mybl.addContract(contract1);
                #endregion

                #region GETALL
                mybl.getAllContracts();
                mybl.getAllNanny();
                mybl.getAllParents();
                mybl.getAllChildren();
                #endregion

                #region REMOVE 

                mybl.removeContract(contract1.contractID);
                mybl.getAllContracts();
                mybl.removeNanny(nanny1.ID);
                mybl.getAllNanny();
                mybl.removeParent(parent1.ID);
                mybl.getAllParents();
                mybl.getAllChildren();
                mybl.removeChild(child1.ID);
                mybl.getAllChildren();

                #endregion

                #region ADD 
                mybl.addNanny(nanny1);
                mybl.addParent(parent1);
                mybl.addChild(child1);
                mybl.addChild(child);
                mybl.getAllContracts();
                mybl.addContract(contract1);

                #endregion

                #region GETALL
                mybl.getAllContracts();
                mybl.getAllNanny();
                mybl.getAllParents();
                mybl.getAllChildren();
                #endregion

                #region WORKING FUNCTION
                int t = mybl.tamatVacation().Count;
                t = mybl.childrenWithNoNanny().Count;
                mybl.CalculateBill(true, contract1);
                mybl.CalculateBill(true, contract1);
                mybl.specificContracts(x => x.monthlyWage >= 1000);
                mybl.numberOfContracts(x => x.monthlyWage >= 1000);
                Dictionary<bool, List<Nanny>> tmp = mybl.nannyLift();
                Dictionary<Language, List<Nanny>> tmp1 = mybl.nannyLanguage();
                Dictionary<int, List<Nanny>> tmp2 = mybl.nannyAge();
                Dictionary<int, List<Contract>> tmp3 = mybl.contractDistance();
                #endregion
                
                mybl.initialMatch(parent, Gender.female, temp2, temp, 1, Specialization.Babysitter, 30, true, true, 3);

                Console.WriteLine("done\n");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                

                //mybl.specificContracts(c => c.ChildId == "5");
                //mybl.specificContracts(c => c.ChildId == "5");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
