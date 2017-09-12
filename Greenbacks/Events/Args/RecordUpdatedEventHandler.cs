using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks.Events
{
    public class RecordUpdatedEventArgs : EventArgs
    {
        public RecordUpdatedEventArgs(TableNames tableName)
        {
            this.TableName = tableName;
        }

        public readonly TableNames TableName;
    }
}
