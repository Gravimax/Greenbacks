using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_Transactions : IDAL_Transactions
    {
        public void CreateTransaction(TransactionModel transaction)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Transaction trans = new Transaction
                {
                    AccountId = transaction.AccountId,
                    CategoryId = transaction.CategoryId,
                    CheckNumber = transaction.CheckNumber,
                    Clear = transaction.Clear,
                    Credit = transaction.Credit,
                    Debit = transaction.Debit,
                    Expense = transaction.Expense,
                    Memo = transaction.Memo,
                    PayeeId = transaction.PayeeId,
                    Reimbersable = transaction.Reimbersable,
                    Taxable = transaction.Taxable,
                    TaxDeductable = transaction.TaxDeductable,
                    TransactionDate = transaction.TransactionDate
                };

                context.Transactions.Add(trans);
                context.SaveChanges();

                transaction.Id = trans.Id;
            }
        }

        public void CreateTransactions(List<TransactionModel> transactions)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {

            }
        }

        public void DeleteTransaction(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Transaction trans = new Transaction { Id = id };
                context.Transactions.Attach(trans);
                context.Transactions.Remove(trans);
                context.SaveChanges();
            }
        }

        public List<TransactionModel> GetAccountTransactions(int accountId)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                var trans = context.Transactions.Select(x => new TransactionModel
                {
                    AccountId = x.AccountId,
                    Balance = x.Balance,
                    CategoryId = x.CategoryId,
                    CheckNumber = x.CheckNumber,
                    Clear = x.Clear,
                    Credit = x.Credit,
                    Debit = x.Debit,
                    Expense = x.Expense,
                    Id = x.Id,
                    Memo = x.Memo,
                    PayeeId = x.PayeeId,
                    Reimbersable = x.Reimbersable,
                    Taxable = x.Taxable,
                    TaxDeductable = x.TaxDeductable,
                    TransactionDate = x.TransactionDate
                }).Where(y => y.AccountId == accountId).OrderBy(z => z.TransactionDate).ToList();

                decimal balance = 0;

                foreach (var item in trans)
                {
                    if (item.Credit > 0)
                    {
                        balance += item.Credit;
                    }
                    else
                    {
                        balance -= item.Debit;
                    }

                    item.Balance = balance;
                }

                return trans;
            }
        }


        public List<TransactionModel> GetTransactionsByDate(DateTime startDate, DateTime endDate)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                var trans = context.Transactions.Select(x => new TransactionModel
                {
                    AccountId = x.AccountId,
                    Balance = x.Balance,
                    CategoryId = x.CategoryId,
                    CheckNumber = x.CheckNumber,
                    Clear = x.Clear,
                    Credit = x.Credit,
                    Debit = x.Debit,
                    Expense = x.Expense,
                    Id = x.Id,
                    Memo = x.Memo,
                    PayeeId = x.PayeeId,
                    Reimbersable = x.Reimbersable,
                    Taxable = x.Taxable,
                    TaxDeductable = x.TaxDeductable,
                    TransactionDate = x.TransactionDate
                }).Where(y => y.TransactionDate >= startDate && y.TransactionDate <= endDate).ToList();

                return trans;
            }
        }


        public TransactionModel GetPreviousTransaction(int accountId, int payeeId)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Transactions.Select(x => new TransactionModel
                {
                    AccountId = x.AccountId,
                    Balance = x.Balance,
                    CategoryId = x.CategoryId,
                    CheckNumber = x.CheckNumber,
                    Clear = x.Clear,
                    Credit = x.Credit,
                    Debit = x.Debit,
                    Expense = x.Expense,
                    Id = x.Id,
                    Memo = x.Memo,
                    PayeeId = x.PayeeId,
                    Reimbersable = x.Reimbersable,
                    Taxable = x.Taxable,
                    TaxDeductable = x.TaxDeductable,
                    TransactionDate = x.TransactionDate
                }).Where(y => y.AccountId == accountId && y.PayeeId == payeeId).OrderByDescending(z => z.TransactionDate).FirstOrDefault();
            }
        }


        public void UpdateTransaction(TransactionModel transaction)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Transaction trans = context.Transactions.Single(x => x.Id == transaction.Id);

                trans.Balance = transaction.Balance;
                trans.CategoryId = transaction.CategoryId;
                trans.CheckNumber = transaction.CheckNumber;
                trans.Clear = transaction.Clear;
                trans.Credit = transaction.Credit;
                trans.Debit = transaction.Debit;
                trans.Expense = transaction.Expense;
                trans.Memo = transaction.Memo;
                trans.PayeeId = transaction.PayeeId;
                trans.Reimbersable = transaction.Reimbersable;
                trans.Taxable = transaction.Taxable;
                trans.TaxDeductable = transaction.TaxDeductable;
                trans.TransactionDate = transaction.TransactionDate;

                context.SaveChanges();
            }
        }

        public void UpdateTransClr(int id, bool clr)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Transaction trans = context.Transactions.Single(x => x.Id == id); // Using the context.Attach() method does not seem to work.
                trans.Clear = clr;
                context.SaveChanges();
            }
        }

        public int GetTransactionCategory(int payeeId)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Transaction trans = context.Transactions.FirstOrDefault(x => x.PayeeId == payeeId);
                if (trans != null)
                {
                    return trans.CategoryId;
                }

                return 0;
            }
        }
    }
}
