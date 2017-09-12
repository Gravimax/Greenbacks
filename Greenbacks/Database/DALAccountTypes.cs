using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;

namespace Greenbacks.Database
{
    public class DALAccountTypes
    {
        public List<AccountTypeModel> GetAccountTypes()
        {
            return new DAL_AccountTypes().GetAccountTypes();
        }

        public void AddAccountType(AccountTypeModel accountType)
        {
            if (accountType != null)
            {
                new DAL_AccountTypes().CreateAccountType(accountType);
                return;
            }

            throw new ArgumentNullException(nameof(accountType), "Argument cannot be null");
        }

        public void UpdateAccountType(AccountTypeModel accountType)
        {
            if (accountType != null)
            {
                new DAL_AccountTypes().UpdateAccountType(accountType);
                return;
            }

            throw new ArgumentNullException(nameof(accountType), "Argument cannot be null");
        }

        public void DeleteAccountType(AccountTypeModel accountType)
        {
            if (accountType != null)
            {
                new DAL_AccountTypes().DeleteAccountType(accountType.Id);
                return;
            }

            throw new ArgumentNullException(nameof(accountType), "Argument cannot be null");
        }
    }
}
