using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class TransactionChangedEventArgs : EventArgs
    {
        public TransactionChangedEventArgs(TransactionModel transaction)
        {
            this.Transaction = transaction;
        }

        public readonly TransactionModel Transaction;
    }
}
