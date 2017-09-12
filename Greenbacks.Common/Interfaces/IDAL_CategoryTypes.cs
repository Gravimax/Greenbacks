using Greenbacks.Common.Models;
using System.Collections.Generic;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_CategoryTypes
    {
        List<CategoryTypeModel> GetCategoryTypes();

        void CreateCategoryType(CategoryTypeModel categoryType);

        void CreateCategoryTypes(List<CategoryTypeModel> categoryTypes);

        void UpdateCategoryType(CategoryTypeModel categoryType);

        void DeleteCategoryType(int categoryTypeId);
    }
}
