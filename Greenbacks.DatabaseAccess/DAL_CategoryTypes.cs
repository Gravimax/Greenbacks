using Greenbacks.Common.Interfaces;
using Greenbacks.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Greenbacks.DatabaseAccess
{
    public class DAL_CategoryTypes : IDAL_CategoryTypes
    { 
        public void CreateCategoryType(CategoryTypeModel categoryType)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                CategoryType type = new CategoryType
                {
                    Name = categoryType.Name
                };

                context.CategoryTypes.Add(type);
                context.SaveChanges();

                categoryType.Id = type.Id;
            }
        }

        public void CreateCategoryTypes(List<CategoryTypeModel> categoryTypes)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                foreach (var item in categoryTypes)
                {
                    context.CategoryTypes.Add(new CategoryType
                    {
                        Name = item.Name
                    });
                }

                context.SaveChanges();
            }
        }

        public void DeleteCategoryType(int categoryTypeId)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                CategoryType type = new CategoryType { Id = categoryTypeId };
                context.CategoryTypes.Attach(type);
                context.CategoryTypes.Remove(type);
                context.SaveChanges();
            }
        }

        public List<CategoryTypeModel> GetCategoryTypes()
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                return context.CategoryTypes.Select(x => new CategoryTypeModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
        }

        public void UpdateCategoryType(CategoryTypeModel categoryType)
        {
            using (GreenbacksDBEntities context = new GreenbacksDBEntities())
            {
                CategoryType type = context.CategoryTypes.Single(x => x.Id == categoryType.Id);

                type.Name = categoryType.Name;

                context.SaveChanges();
            }
        }
    }
}
