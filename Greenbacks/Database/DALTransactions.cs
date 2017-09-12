using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Greenbacks.Database
{
    public class DALTransactions
    {
        public TransactionModel GetPreviousTransaction(int accountId, int payeeId)
        {
            return new DAL_Transactions().GetPreviousTransaction(accountId, payeeId);
        }

        public ObservableCollection<TransactionModel> GetAccountTransactions(int accountId)
        {
            return new DAL_Transactions().GetAccountTransactions(accountId).ToObservableCollection();
        }


        public List<TransactionModel> GetTransactionsByDate(DateTime startDate, DateTime endDate)
        {
            return new DAL_Transactions().GetTransactionsByDate(startDate, endDate);
        }


        public void CreateTransaction(TransactionModel transaction)
        {
            if (transaction != null)
            {
                new DAL_Transactions().CreateTransaction(transaction);
                return;
            }

            throw new ArgumentNullException(nameof(transaction), "Argument cannot be null");
        }

        public void AddTransactions(List<TransactionModel> transactions)
        {
            if (transactions != null)
            {
                new DAL_Transactions().CreateTransactions(transactions);
                return;
            }

            throw new ArgumentNullException(nameof(transactions), "Argument cannot be null");
        }

        public void UpdateTransaction(TransactionModel transaction)
        {
            if (transaction != null)
            {
                new DAL_Transactions().UpdateTransaction(transaction);
                return;
            }

            throw new ArgumentNullException(nameof(transaction), "Argument cannot be null");
        }

        public void DeleteTransaction(TransactionModel transaction)
        {
            if (transaction != null)
            {
                new DAL_Transactions().DeleteTransaction(transaction.Id);
                return;
            }

            throw new ArgumentNullException(nameof(transaction), "Argument cannot be null");
        }

        public void UpdateTransClr(int id, bool clr)
        {
            new DAL_Transactions().UpdateTransClr(id, clr);
        }


        public int GetTransactionCategory(int payeeId)
        {
            if (payeeId > 0)
            {
                return new DALTransactions().GetTransactionCategory(payeeId);
            }

            return -1;
        }
    }
}
