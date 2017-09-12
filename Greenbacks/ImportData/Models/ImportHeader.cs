using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks.ImportData.Models
{
    public class ImportHeader : IImportHeader
    {
        public string BankId { get; set; }

        public string AccountId { get; set; }

        public string AccountType { get; set; }
    }
}
