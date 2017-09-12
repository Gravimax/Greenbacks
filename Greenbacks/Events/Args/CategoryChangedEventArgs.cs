using Greenbacks.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenbacks
{
    public class CategoryChangedEventArgs : EventArgs
    {
        public CategoryChangedEventArgs(CategoryModel category)
        {
            this.Category = category;
        }

        public readonly CategoryModel Category;
    }
}
