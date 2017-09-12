using Greenbacks.Common.Models;
using System.Collections.Generic;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_Account
    {
        AccountModel GetAccount(int id);

        List<AccountModel> GetAccounts();

        decimal GetAccountIncome(int id);

        decimal GetAccountExpenses(int id);

        decimal GetAccountBalance(int id);

        decimal GetFutureCredit(int id);

        decimal GetFutureDebit(int id);

        void CreateAccount(AccountModel account);

        void UpdateAccount(AccountModel account);

        void DeleteAccount(int id);
    }
}
