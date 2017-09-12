using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_Accounts : IDAL_Account
    {
        public void CreateAccount(AccountModel account)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Account newAccount = new Account
                {
                    AccountNumber = account.AccountNumber,
                    AccountTypeId = account.AccountTypeId,
                    BankName = account.BankName,
                    Name = account.Name,
                    Notes = account.Notes
                };

                context.Accounts.Add(newAccount);
                context.SaveChanges();
                account.Id = newAccount.Id;
            }
        }

        public void DeleteAccount(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                var tracks = context.PayeeTracks.Where(x => x.AccountId == id).ToList();
                if (tracks.Count > 0)
                {
                    context.PayeeTracks.RemoveRange(tracks);
                    context.SaveChanges();
                }

                Account account = new Account { Id = id };
                context.Accounts.Attach(account);
                context.Accounts.Remove(account);
                context.SaveChanges();
            }
        }

        public AccountModel GetAccount(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Accounts.Select(y => new AccountModel
                {
                    AccountNumber = y.AccountNumber,
                    AccountTypeId = y.AccountTypeId,
                    BankName = y.BankName,
                    Id = y.Id,
                    Name = y.Name,
                    Notes = y.Notes
                }).Single(x => x.Id == id);
            }
        }

        public decimal GetAccountBalance(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                decimal credits = context.Transactions.Where(y => y.AccountId == id && y.TransactionDate <= DateTime.Now).Sum(x => (decimal?)x.Credit) ?? 0;
                decimal debits = context.Transactions.Where(y => y.AccountId == id && y.TransactionDate <= DateTime.Now).Sum(x => (decimal?)x.Debit) ?? 0;
                return credits - debits;
            }
        }


        public decimal GetAccountIncome(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Transactions.Where(x => x.AccountId == id && x.TransactionDate <= DateTime.Now).Sum(y => (decimal?)y.Credit ) ?? 0;
            }
        }


        public decimal GetAccountExpenses(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Transactions.Where(x => x.AccountId == id && x.TransactionDate <= DateTime.Now).Sum(y => (decimal?)y.Debit) ?? 0;
            }
        }


        public List<AccountModel> GetAccounts()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Accounts.Select(x => new AccountModel
                {
                    AccountNumber = x.AccountNumber,
                    AccountTypeId = x.AccountTypeId,
                    BankName = x.BankName,
                    Id = x.Id,
                    Name = x.Name,
                    Notes = x.Notes
                }).ToList();
            }
        }

        public decimal GetFutureCredit(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Transactions.Where(y => y.AccountId == id && y.TransactionDate > DateTime.Now).Sum(x => (decimal?)x.Credit) ?? 0;
            }
        }

        public decimal GetFutureDebit(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Transactions.Where(y => y.AccountId == id && y.TransactionDate > DateTime.Now).Sum(x => (decimal?)x.Debit) ?? 0;
            }
        }

        public void UpdateAccount(AccountModel account)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Account exstAccount = context.Accounts.Single(x => x.Id == account.Id);

                exstAccount.AccountTypeId = account.AccountTypeId;
                exstAccount.BankName = account.BankName;
                exstAccount.Name = account.Name;
                exstAccount.Notes = account.Notes;
                exstAccount.AccountNumber = account.AccountNumber;

                context.SaveChanges();
            }
        }
    }
}
