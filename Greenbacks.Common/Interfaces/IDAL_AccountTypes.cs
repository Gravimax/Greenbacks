using Greenbacks.Common.Models;
using System.Collections.Generic;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_AccountTypes
    {
        List<AccountTypeModel> GetAccountTypes();

        bool AccountTypeInUse(int id);

        void CreateAccountType(AccountTypeModel accountType);

        void CreateAccountTypes(List<AccountTypeModel> accountTypes);

        void UpdateAccountType(AccountTypeModel accountType);

        void DeleteAccountType(int id);
    }
}
