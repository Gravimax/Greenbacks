using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class TableChangedEventArgs : EventArgs
    {
        public TableChangedEventArgs(TableNames tableName)
        {
            this.TableName = tableName;
        }

        public readonly TableNames TableName;
    }
}
