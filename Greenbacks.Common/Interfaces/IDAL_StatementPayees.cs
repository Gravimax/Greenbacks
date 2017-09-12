using Greenbacks.Common.Models;
using System.Collections.Generic;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_StatementPayees
    {
        List<StatementPayeeModel> GetStatementPayees(int id);

        StatementPayeeModel GetStatementPayee(string name);

        List<StatementPayeeModel> GetPayeeList();

        void CreatePayee(StatementPayeeModel payee);

        void UpdatePayee(StatementPayeeModel payee);

        void DeletePayee(StatementPayeeModel payee);
    }
}
