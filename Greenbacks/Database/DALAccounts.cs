using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.Database
{
    public class DALAccounts
    {
        public List<AccountModel> GetAccountSummaries()
        {
            var context = new DAL_Accounts();
            var accounts = context.GetAccounts();
            var acctTypes = new DAL_AccountTypes().GetAccountTypes();

            foreach (var account in accounts)
            {
                account.Asset = acctTypes.Single(x => x.Id == account.AccountTypeId).Asset;
                account.Income = context.GetAccountIncome(account.Id);
                account.Expenses = context.GetAccountExpenses(account.Id);
                account.Balance = account.Income - account.Expenses;
                
                decimal futureCredit = context.GetFutureCredit(account.Id);
                decimal futureDebit = context.GetFutureDebit(account.Id);
                account.FutureBalance = futureCredit - futureDebit;
            }

            return accounts;
        }

        public AccountModel GetAccount(int accountId)
        {
            if (accountId <= 0) { throw new ArgumentOutOfRangeException(nameof(accountId), "Invalid account Id"); }
            return new DAL_Accounts().GetAccount(accountId);
        }

        public List<AccountModel> GetAccounts()
        {
            return new DAL_Accounts().GetAccounts();
        }

        public List<AccountModel> GetAccountList()
        {
            return new DAL_Accounts().GetAccounts();
        }

        public void CreateAccount(AccountModel account)
        {
            if (account != null)
            {
                new DAL_Accounts().CreateAccount(account);
                return;
            }

            throw new ArgumentNullException(nameof(account), "Argument cannot be null");
        }

        public void UpdateAccount(AccountModel account)
        {
            if (account != null)
            {
                new DAL_Accounts().UpdateAccount(account);
                return;
            }

            throw new ArgumentNullException(nameof(account), "Argument cannot be null");
        }

        public void DeleteAccount(AccountModel account)
        {
            if (account != null)
            {
                new DAL_Accounts().DeleteAccount(account.Id);
                return;
            }

            throw new ArgumentNullException(nameof(account), "Argument cannot be null");
        }
    }
}
