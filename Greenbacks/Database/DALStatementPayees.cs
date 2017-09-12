using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;

namespace Greenbacks.Database
{
    public class DALStatementPayees : IDAL_StatementPayees
    {
        public void CreatePayee(StatementPayeeModel payee)
        {
            if (payee != null)
            {
                new DAL_StatementPayees().CreatePayee(payee);
                return;
            }

            throw new ArgumentNullException(nameof(payee), "Argument cannot be null");
        }

        public void DeletePayee(StatementPayeeModel payee)
        {
            if (payee != null)
            {
                new DAL_StatementPayees().DeletePayee(payee);
                return;
            }

            throw new ArgumentNullException(nameof(payee), "Argument cannot be null");
        }

        public List<StatementPayeeModel> GetPayeeList()
        {
            return new DAL_StatementPayees().GetPayeeList();
        }

        public StatementPayeeModel GetStatementPayee(string name)
        {
            return new DAL_StatementPayees().GetStatementPayee(name);
        }

        public List<StatementPayeeModel> GetStatementPayees(int id)
        {
            return new DAL_StatementPayees().GetStatementPayees(id);
        }

        public void UpdatePayee(StatementPayeeModel payee)
        {
            if (payee != null)
            {
                new DAL_StatementPayees().UpdatePayee(payee);
                return;
            }

            throw new ArgumentNullException(nameof(payee), "Argument cannot be null");
        }
    }
}
