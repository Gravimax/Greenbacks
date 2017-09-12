using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class ScrollToRecordEventArgs : EventArgs
    {
        public ScrollToRecordEventArgs(object record)
        {
            this.Record = record;
        }

        public readonly object Record;
    }
}
