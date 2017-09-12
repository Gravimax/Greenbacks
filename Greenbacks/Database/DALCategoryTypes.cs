using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using System;
using System.Collections.Generic;

namespace Greenbacks.Database
{
    public class DALCategoryTypes
    {
        public List<CategoryTypeModel> GetCategoryTypes()
        {
            return new DAL_CategoryTypes().GetCategoryTypes();
        }

        public void AddCategoryType(CategoryTypeModel categoryType)
        {
            if (categoryType != null)
            {
                new DAL_CategoryTypes().CreateCategoryType(categoryType);
                return;
            }

            throw new ArgumentNullException(nameof(categoryType), "Argument cannot be null");
        }

        public void UpdateCategoryType(CategoryTypeModel categoryType)
        {
            if (categoryType != null)
            {
                new DAL_CategoryTypes().UpdateCategoryType(categoryType);
                return;
            }

            throw new ArgumentNullException(nameof(categoryType), "Argument cannot be null");
        }

        public void DeleteCategoryType(CategoryTypeModel categoryType)
        {
            if (categoryType != null)
            {
                new DAL_CategoryTypes().DeleteCategoryType(categoryType.Id);
                return;
            }

            throw new ArgumentNullException(nameof(categoryType), "Argument cannot be null");
        }
    }
}
