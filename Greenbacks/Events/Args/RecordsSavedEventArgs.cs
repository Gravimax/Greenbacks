using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class RecordsSavedEventArgs : EventArgs
    {
        public RecordsSavedEventArgs(TableNames tableName, DateTime savedDate)
        {
            this.TableName = tableName;
            this.SavedDate = savedDate;
        }

        public readonly TableNames TableName;
        public readonly DateTime SavedDate;
    }
}
