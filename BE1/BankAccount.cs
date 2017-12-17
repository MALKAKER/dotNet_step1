using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class BankAccount
    {
        #region Feilds
        private int branchNumberP;
        private long accountNumberP;
        #endregion
        
        #region Attributes
        public Bank bankName { get; set; }
  
        public int branchNumber
        {
            get
            {
                return branchNumberP;
            }
            set
            {
                if ( value < 0 || value > 1000)
                {
                    throw new Exception("Invalid branch name!\n");
                }
                branchNumberP = value;
            }
        }

        public Address branchAddress{ get; set; }

        public long accountNumber
        {
            get
            {
                return accountNumberP;
            }
            set
            {
                if (value < 100000 || value >= 100000000)
                {
                    throw new Exception("Invalid account number!\n");
                }
                accountNumberP = value;
            }
        }
        #endregion

        #region Methods

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
            return string.Format("Bank Name: {0,10}\tBank Number: {1,10}\tBranch Number: {2,10}\tBranch Address: {3,10}\tAccount Number: {4,10}\n", bankName, (short)bankName, branchNumber, branchAddress.ToString(), accountNumber);
        }
        #endregion
    }
}
