using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class PayeeChangedEventArgs : EventArgs
    {
        public PayeeChangedEventArgs(PayeeModel payee)
        {
            this.Payee = payee;
        }

        public readonly PayeeModel Payee;
    }
}
