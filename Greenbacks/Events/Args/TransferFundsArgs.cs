using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Greenbacks
{
    public class TransferFundsArgs
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal TransferAmount { get; set; }
        public string Memo { get; set; }
    }
}
