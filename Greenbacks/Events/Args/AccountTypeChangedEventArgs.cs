using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class AccountTypeChangedEventArgs : EventArgs
    {
        public AccountTypeChangedEventArgs(AccountTypeModel accountType)
        {
            this.AccountType = accountType;
        }

        public readonly AccountTypeModel AccountType;
    }
}
