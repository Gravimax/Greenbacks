using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_StatementPayees : IDAL_StatementPayees
    {
        public void CreatePayee(StatementPayeeModel payee)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                context.StatementPayees.Add(new StatementPayee { PayeeId = payee.PayeeId, StatementName = payee.StatementName });
                context.SaveChanges();
            }
        }


        public void DeletePayee(StatementPayeeModel payee)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                StatementPayee sp = context.StatementPayees.FirstOrDefault(x => x.StatementName == payee.StatementName);
                if (sp != null)
                {
                    context.StatementPayees.Remove(sp);
                    context.SaveChanges();
                }
            }
        }


        public List<StatementPayeeModel> GetPayeeList()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                List<StatementPayeeModel> temp = new List<StatementPayeeModel>();

                foreach (var item in context.StatementPayees.ToList())
                {
                    temp.Add(new StatementPayeeModel { PayeeId = item.PayeeId, StatementName = item.StatementName });
                }

                return temp;
            }
        }


        public StatementPayeeModel GetStatementPayee(string name)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                StatementPayeeModel temp = null;

                StatementPayee sp = context.StatementPayees.FirstOrDefault(x => x.StatementName == name);

                if (sp != null)
                {
                    temp = new StatementPayeeModel { PayeeId = sp.PayeeId, StatementName = sp.StatementName };
                }

                return temp;
            }
        }


        public List<StatementPayeeModel> GetStatementPayees(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                List<StatementPayeeModel> temp = new List<StatementPayeeModel>();

                foreach (var item in context.StatementPayees.Where(x => x.PayeeId == id).ToList())
                {
                    temp.Add(new StatementPayeeModel { PayeeId = item.PayeeId, StatementName = item.StatementName });
                }

                return temp;
            }
        }


        public void UpdatePayee(StatementPayeeModel payee)
        {
            throw new NotImplementedException();
        }
    }
}
