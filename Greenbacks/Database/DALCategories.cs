using Greenbacks.Common.Models;
using Greenbacks.DatabaseAccess;
using Greenbacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.Database
{
    public class DALCategories
    {
        public List<GenericComboItem> GetCategoriesList()
        {
            var context = new DAL_Categories();
            var categories = context.GetCategories();
            var cateTypes = new DAL_CategoryTypes().GetCategoryTypes();

            return categories.Select(x => new GenericComboItem
            {
                ID = x.Id,
                Value = GetCategoryName(x, cateTypes.Single(y => y.Id == x.CategoryTypeId).Name)
            }).ToList();
        }

        private string GetCategoryName(CategoryModel category, string categoryType)
        {
            return "<" + categoryType + "> " + category.Name + (!string.IsNullOrEmpty(category.SubCategoryName) ? (" -- " + category.SubCategoryName) : string.Empty);
        }

        public CategoryModel GetCategory(int id)
        {
            return new DAL_Categories().GetCategory(id);
        }

        public CategoryModel GetCategory(string name)
        {
            return new DAL_Categories().GetCategory(name);
        }

        public CategoryModel GetCategory(int categoryTypeId, string name)
        {
            return new DAL_Categories().GetCategory(categoryTypeId, name);
        }

        public List<CategoryModel> GetCategories()
        {
            return new DAL_Categories().GetCategories();
        }

        public void CreateCategory(CategoryModel category)
        {
            if (category != null)
            {
                new DAL_Categories().CreateCategory(category);
                return;
            }

            throw new ArgumentNullException(nameof(category), "Argument cannot be null");
        }

        public void UpdateCategory(CategoryModel category)
        {
            if (category != null)
            {
                new DAL_Categories().UpdateCategory(category);
                return;
            }

            throw new ArgumentNullException(nameof(category), "Argument cannot be null");
        }

        public void DeleteCategory(CategoryModel category)
        {
            if (category != null)
            {
                DeleteCategory(category.Id);
                return;
            }

            throw new ArgumentNullException(nameof(category), "Argument cannot be null");
        }

        public void DeleteCategory(int categoryId)
        {
            new DAL_Categories().DeleteCategory(categoryId);
        }

        public bool IsCategoryInUse(int categoryId)
        {
            return new DAL_Categories().IsCategoryInUse(categoryId);
        }

        public List<GenericComboItem> GetParentCategories()
        {
            var categories = new DAL_Categories().GetParentCategories();
            var cateTypes = new DAL_CategoryTypes().GetCategoryTypes();

            return categories.Select(x => new GenericComboItem
            {
                ID = x.Id,
                Value = x.Name
            }).ToList();
        }

        public List<GenericComboItem> GetParentCategories(int catType)
        {
            var categories = new DAL_Categories().GetParentCategories(catType);
            var cateTypes = new DAL_CategoryTypes().GetCategoryTypes();

            return categories.Select(x => new GenericComboItem
            {
                ID = x.Id,
                Value = x.Name
            }).ToList();
        }

        public List<GenericComboItem> GetChildCategories(int catId)
        {
            var categories = new DAL_Categories().GetChildCategories(catId);

            return categories.Select(x => new GenericComboItem
            {
                Value = x.Name
            }).ToList();
        }
    }
}
