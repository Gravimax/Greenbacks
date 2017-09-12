using Greenbacks.Common.Models;
using Greenbacks.ImportData.Models;

namespace Greenbacks.Models
{
    public class ImportTransTemp
    {
        public int PayeeId { get; set; }

        public string PayeeName { get; set; }

        public int CategoryId { get; set; }

        public BankTransaction BankTrans { get; set; }

        public TransactionModel TransModel { get; set; }
    }
}
