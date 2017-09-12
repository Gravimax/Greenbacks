using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;

namespace Greenbacks.Database
{
    public class DALPayees
    {
        public PayeeModel GetPayee(string name)
        {
            return new DAL_Payees().GetPayee(name);
        }

        public PayeeModel GetPayee(int id)
        {
            return new DAL_Payees().GetPayee(id);
        }

        public List<PayeeModel> GetPayees()
        {
            return new DAL_Payees().GetPayees();
        }

        public void CreatePayee(PayeeModel payee)
        {
            if (payee != null)
            {
                new DAL_Payees().CreatePayee(payee);
                return;
            }

            throw new ArgumentNullException(nameof(payee), "Argument cannot be null");
        }

        public void UpdatePayee(PayeeModel payee)
        {
            if (payee != null)
            {
                new DAL_Payees().UpdatePayee(payee);
                return;
            }

            throw new ArgumentNullException(nameof(payee), "Argument cannot be null");
        }

        public void DeletePayee(PayeeModel payee)
        {
            if (payee != null)
            {
                new DAL_Payees().DeletePayee(payee.Id);
                return;
            }

            throw new ArgumentNullException(nameof(payee), "Argument cannot be null");
        }

        public bool IsPayeeInUse(int payeeId)
        {
            return new DAL_Payees().IsPayeeInUse(payeeId);
        }
    }
}
