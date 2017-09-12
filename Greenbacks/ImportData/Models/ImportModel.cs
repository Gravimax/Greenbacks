using System;
using System.Collections.Generic;

namespace Greenbacks.ImportData.Models
{
    public class ImportModel
    {
        public IImportHeader ImportHeader { get; set; }

        public Status Status { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public BankAccountInfo AccountInfo { get; set; }

        public List<BankTransaction> TransactionList { get; set; }

        public LedgerBalance LedgerBalance { get; set; }

        public AvailableBalance AvailableBalance { get; set; }
    }
}
