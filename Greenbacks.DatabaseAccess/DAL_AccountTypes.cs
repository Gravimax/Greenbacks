using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_AccountTypes : IDAL_AccountTypes
    {
        public bool AccountTypeInUse(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Accounts.Count(x => x.AccountTypeId == id) > 0;
            }
        }

        public void CreateAccountType(AccountTypeModel accountType)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                AccountType type = new AccountType
                {
                    Asset = accountType.Asset,
                    Description = accountType.Description,
                    Name = accountType.Name
                };

                context.AccountTypes.Add(type);
                context.SaveChanges();

                accountType.Id = type.Id;
            }
        }

        public void CreateAccountTypes(List<AccountTypeModel> accountTypes)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                foreach (var item in accountTypes)
                {
                    context.AccountTypes.Add(new AccountType
                    {
                        Asset = item.Asset,
                        Description = item.Description,
                        Name = item.Name
                    });

                    context.SaveChanges();
                }
            }
        }

        public void DeleteAccountType(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                AccountType type = new AccountType { Id = id };
                context.AccountTypes.Attach(type);
                context.AccountTypes.Remove(type);
                context.SaveChanges();
            }
        }

        public List<AccountTypeModel> GetAccountTypes()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.AccountTypes.Select(x => new AccountTypeModel
                {
                    Asset = x.Asset,
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
        }

        public void UpdateAccountType(AccountTypeModel accountType)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                AccountType type = context.AccountTypes.Single(x => x.Id == accountType.Id);

                type.Asset = accountType.Asset;
                type.Description = accountType.Description;
                type.Name = accountType.Name;

                context.SaveChanges();
            }
        }
    }
}
