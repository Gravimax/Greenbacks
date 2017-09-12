using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class CategoryTypeChangedEventArgs : EventArgs
    {
        public CategoryTypeChangedEventArgs(CategoryTypeModel categoryType)
        {
            this.CategoryType = categoryType;
        }

        public readonly CategoryTypeModel CategoryType;
    }
}
