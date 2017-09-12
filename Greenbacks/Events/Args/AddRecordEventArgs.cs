using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class RecordChangedEventArgs : EventArgs
    {
        public RecordChangedEventArgs(TableNames tableName, object record)
        {
            this.TableName = tableName;
            this.Record = record;
        }

        public readonly TableNames TableName;
        public readonly object Record;
    }
}
