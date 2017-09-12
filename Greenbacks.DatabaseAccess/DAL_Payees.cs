using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_Payees : IDAL_Payees
    {
        public void CreatePayee(PayeeModel payee)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Payee pyee = new Payee
                {
                    Name = payee.Name,
                    Notes = payee.Notes
                };

                context.Payees.Add(pyee);
                context.SaveChanges();

                payee.Id = pyee.Id;
            }
        }

        public void DeletePayee(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Payee payee = new Payee { Id = id };
                context.Payees.Attach(payee);
                context.Payees.Remove(payee);
                context.SaveChanges();
            }
        }

        public PayeeModel GetPayee(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Payees.Select(x => new PayeeModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Notes = x.Notes
                }).Single(y => y.Id == id);
            }
        }

        public PayeeModel GetPayee(string name)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Payees.Select(x => new PayeeModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Notes = x.Notes
                }).Single(y => y.Name.Equals(name));
            }
        }

        public List<PayeeModel> GetPayees()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Payees.Select(x => new PayeeModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Notes = x.Notes
                }).ToList();
            }
        }

        public bool IsPayeeInUse(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Transactions.Count(x => x.PayeeId == id) > 0;
            }
        }

        public void UpdatePayee(PayeeModel payee)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Payee pyee = context.Payees.Single(x => x.Id == payee.Id);

                pyee.Name = payee.Name;
                pyee.Notes = payee.Notes;

                context.SaveChanges();
            }
        }
    }
}
