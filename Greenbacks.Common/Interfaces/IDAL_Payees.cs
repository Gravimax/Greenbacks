using Greenbacks.Common.Models;
using System.Collections.Generic;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_Payees
    {
        PayeeModel GetPayee(string name);

        PayeeModel GetPayee(int id);

        List<PayeeModel> GetPayees();

        void CreatePayee(PayeeModel payee);

        void UpdatePayee(PayeeModel payee);

        void DeletePayee(int id);

        bool IsPayeeInUse(int id);
    }
}
