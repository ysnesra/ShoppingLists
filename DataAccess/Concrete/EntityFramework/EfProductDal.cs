using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, DbShoppingListContext>, IProductDal
    {
        //GetProductWithByCategoryBrandAll
        public List<ProductDto> GetProductDetails()
        {
            using (DbShoppingListContext context=new DbShoppingListContext())
            {
                var result = (from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName=p.ProductName,
                                 CategoryId=c.CategoryId,   
                                 ImageUrl=p.ImageUrl,
                                 CategoryName=c.CategoryName,
                                 Description = p.Description,
                                 UnitInStock =p.UnitInStock
                             }).ToList();

                return result;
            }
        }
        public ProductDto GetProductDetailByProductName(string productName)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                var result = (from p in context.Products
                              join c in context.Categories on p.CategoryId equals c.CategoryId                        
                              where p.ProductName.ToLower()==productName.ToLower()
                              select new ProductDto
                              {
                                  ProductId = p.ProductId,
                                  CategoryId=p.Category.CategoryId,
                                  ProductName = p.ProductName,
                                  ImageUrl=p.ImageUrl,
                                  CategoryName = c.CategoryName,
                                  Description = p.Description,
                                  UnitInStock = p.UnitInStock
                              }).FirstOrDefault();

                return result;
            }
        }

        public List<ProductDto> GetProductByCategory(int categoryId)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                var products = context.Products.Include(p => p.Category)
                        .Where(p => p.CategoryId == categoryId)
                        .Select(p=>new ProductDto
                        {
                            ProductId=p.ProductId,
                            CategoryId =p.Category.CategoryId,
                            ImageUrl=p.ImageUrl,    
                            ProductName=p.ProductName,  
                            Description=p.Description,   
                            CategoryName=p.Category.CategoryName,
                            UnitInStock = p.UnitInStock
                        }).ToList();    
                
                return products;
            }
        }
    }
}
