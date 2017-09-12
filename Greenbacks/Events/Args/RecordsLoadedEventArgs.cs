using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class RecordsLoadedEventArgs : EventArgs
    {
        public RecordsLoadedEventArgs(string message, Exception error)
        {
            this.Message = message;
            this.Error = error;
        }

        public readonly string Message;
        public readonly Exception Error;
    }
}
