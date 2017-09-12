using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class PreviousTransactionEventArgs : EventArgs
    {
        public PreviousTransactionEventArgs(int accountId, int payeeId)
        {
            this.AccountId = accountId;
            this.PayeeId = payeeId;
        }

        public readonly int AccountId;
        public readonly int PayeeId;
    }
}
