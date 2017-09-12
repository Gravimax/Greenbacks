using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks.ImportData.Models
{
    public class BankTransaction
    {
        public int PayeeId { get; set; }

        public string TransactionType { get; set; }

        public DateTime DatePosted { get; set; }

        public decimal TransactionAmount { get; set; }

        public string TransactionId { get; set; }

        public string Name { get; set; }

        public string Memo { get; set; }
    }
}
