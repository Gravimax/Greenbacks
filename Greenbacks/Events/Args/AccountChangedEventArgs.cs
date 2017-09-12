using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class AccountChangedEventArgs : EventArgs
    {
        public AccountChangedEventArgs(AccountModel account)
        {
            this.Account = account;
        }

        public readonly AccountModel Account;
    }
}
