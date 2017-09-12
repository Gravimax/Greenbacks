using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_Transactions
    {
        TransactionModel GetPreviousTransaction(int accountId, int payeeId);

        List<TransactionModel> GetAccountTransactions(int accountId);

        List<TransactionModel> GetTransactionsByDate(DateTime startDate, DateTime endDate);

        int GetTransactionCategory(int payeeId);

        void CreateTransaction(TransactionModel transaction);

        void CreateTransactions(List<TransactionModel> transactions);

        void UpdateTransaction(TransactionModel transaction);

        void DeleteTransaction(int id);

        void UpdateTransClr(int id, bool clr);
    }
}
