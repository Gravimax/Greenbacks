using Greenbacks.Common.Models;
using System.Collections.Generic;

namespace Greenbacks.Common.Interfaces
{
    public interface IDAL_Categories
    {
        List<CategoryModel> GetCategories();

        CategoryModel GetCategory(int id);

        CategoryModel GetCategory(string name);

        CategoryModel GetCategory(int categoryTypeId, string name);

        void CreateCategory(CategoryModel category);

        void CreateCategories(List<CategoryModel> categories);

        void UpdateCategory(CategoryModel category);

        void UpdateCategories(List<CategoryModel> categories);

        void DeleteCategory(int id);

        bool IsCategoryInUse(int id);

        List<CategoryModel> GetParentCategories();

        List<CategoryModel> GetParentCategories(int catType);

        List<CategoryModel> GetChildCategories(int id);
    }
}
