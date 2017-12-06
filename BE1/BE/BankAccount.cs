using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class BankAccount
    {
        //Fields:
        public Bank bankName { get; set; }
        //public short bankNumber
        //{
        //    get
        //    {
        //        return bankNumber;
        //    }
        //    set
        //    {
        //        //the enum value for each bank
        //        bankNumber = (short)bankName;
        //    }
        //}
        public int branchNumber
        {
            get
            {
                return branchNumber;
            }
            set
            {
                if ( value < 0 || value > 1000)
                {
                    throw new Exception("Invalid branch name!\n");
                }
                branchNumber = value;
            }
        }
        public Address branchAddress{ get; set; }
        public long accountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                if (value < 100000 || value >= 100000000)
                {
                    throw new Exception("Invalid account number!\n");
                }
            }
        }
        


        //Methods:

        //Constructors:
        public BankAccount(Bank myBankName, int myBranchNumber, Address myBranchAddress, long myAccountNumber)
        {
            UpdateBankAccount(myBankName, myBranchNumber, myBranchAddress, myAccountNumber);
        }
        public BankAccount() { }

        //Update bank account details:
        public void UpdateBankAccount(Bank myBankName, int myBranchNumber, Address myBranchAddress, long myAccountNumber)
        {
            bankName = myBankName;
            branchNumber = myBranchNumber;
            branchAddress = myBranchAddress;
            accountNumber = myAccountNumber;
        }
        public void UpdateBankAccount(BankAccount newAccount)
        {
            bankName = newAccount.bankName;
            branchNumber = newAccount.branchNumber;
            branchAddress = newAccount.branchAddress;
            accountNumber = newAccount.accountNumber;
        }
        public override string ToString()
        {
            return string.Format("Bank Name: {0,10}Bank Number: {1,10}Branch Number: {2,10}Branch Address: {3,10}Account Number: {4,10}\n", bankName, (short)bankName, branchNumber, branchAddress.ToString(), accountNumber);
        }
    }
}
