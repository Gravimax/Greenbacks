using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_Categories : IDAL_Categories
    {
        public void CreateCategories(List<CategoryModel> categories)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                foreach (var item in categories)
                {
                    Category cat = new Category
                    {
                        CategoryTypeId = item.CategoryTypeId,
                        Name = item.Name,
                        ParentCategoryId = item.ParentCategoryId,
                        SubCategoryName = item.SubCategoryName
                    };

                    context.Categories.Add(cat);
                    context.SaveChanges();

                    item.Id = cat.Id;
                }
            }
        }

        public void CreateCategory(CategoryModel category)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Category cat = new Category
                {
                    CategoryTypeId = category.CategoryTypeId,
                    Name = category.Name,
                    ParentCategoryId = category.ParentCategoryId,
                    SubCategoryName = category.SubCategoryName
                };

                context.Categories.Add(cat);
                context.SaveChanges();

                category.Id = cat.Id;
            }
        }

        public void DeleteCategory(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Category cat = new Category { Id = id };
                context.Categories.Attach(cat);
                context.Categories.Remove(cat);
                context.SaveChanges();
            }
        }

        public List<CategoryModel> GetCategories()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Categories.Select(x => new CategoryModel
                {
                    CategoryTypeId = x.CategoryTypeId,
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                    SubCategoryName = x.SubCategoryName
                }).ToList();
            }
        }

        public CategoryModel GetCategory(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Categories.Select(x => new CategoryModel
                {
                    CategoryTypeId = x.CategoryTypeId,
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                    SubCategoryName = x.SubCategoryName
                }).Single(y => y.Id == id);
            }
        }

        public CategoryModel GetCategory(string name)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Categories.Select(y => new CategoryModel
                {
                    CategoryTypeId = y.CategoryTypeId,
                    Id = y.Id,
                    Name = y.Name,
                    ParentCategoryId = y.ParentCategoryId,
                    SubCategoryName = y.SubCategoryName
                }).First(x => x.Name == name);
            }
        }

        public CategoryModel GetCategory(int categoryTypeId, string name)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Categories.Select(x => new CategoryModel
                {
                    CategoryTypeId = x.CategoryTypeId,
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                    SubCategoryName = x.SubCategoryName
                }).First(y => y.CategoryTypeId == categoryTypeId && y.Name == name);
            }
        }

        public List<CategoryModel> GetChildCategories(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                List<CategoryModel> temp = new List<CategoryModel>();

                var list = context.Categories.Where(y => y.ParentCategoryId == id).Select(x => x.Name).Distinct();

                foreach (var item in list)
                {
                    temp.Add(new CategoryModel { Name = item });
                }

                return temp;
            }
        }

        public List<CategoryModel> GetParentCategories()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Categories.Select(x => new CategoryModel
                {
                    CategoryTypeId = x.CategoryTypeId,
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                    SubCategoryName = x.SubCategoryName
                }).Where(y => y.ParentCategoryId == 0).ToList();
            }
        }

        public List<CategoryModel> GetParentCategories(int catType)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Categories.Select(x => new CategoryModel
                {
                    CategoryTypeId = x.CategoryTypeId,
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                    SubCategoryName = x.SubCategoryName
                }).Where(y => y.ParentCategoryId == 0 && y.CategoryTypeId == catType).ToList();
            }
        }

        public bool IsCategoryInUse(int id)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.Transactions.Count(x => x.CategoryId == id) > 0;
            }
        }

        public void UpdateCategory(CategoryModel category)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                Category cat = context.Categories.Single(x => x.Id == category.Id);

                cat.CategoryTypeId = category.CategoryTypeId;
                cat.Name = category.Name;
                cat.ParentCategoryId = category.ParentCategoryId;
                cat.SubCategoryName = category.SubCategoryName;

                context.SaveChanges();
            }
        }

        public void UpdateCategories(List<CategoryModel> categories)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                foreach (var item in categories)
                {
                    Category cat = context.Categories.Single(x => x.Id == item.Id);

                    cat.CategoryTypeId = item.CategoryTypeId;
                    cat.Name = item.Name;
                    cat.ParentCategoryId = item.ParentCategoryId;
                    cat.SubCategoryName = item.SubCategoryName;

                    context.SaveChanges();
                }
            }
        }
    }
}
