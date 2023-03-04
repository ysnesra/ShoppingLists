
using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, DbShoppingListContext>, ICategoryDal
    {
        public List<CategoryDto> GetCategoryList()
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                var categoryDtoList = context.Categories.Select(x=> new CategoryDto
                { CategoryId=x.CategoryId,CategoryName=x.CategoryName}).ToList();

                return categoryDtoList;
            }
        }
    }
}
