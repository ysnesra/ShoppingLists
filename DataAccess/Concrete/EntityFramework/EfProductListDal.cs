﻿using Core.DataAccess.Entityframework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductListDal : EfEntityRepositoryBase<ProductList, DbShoppingListContext>, IProductListDal
    {
        //ProductList'i listeleme (alışverişListelerini listeleme)
        public List<ProductListDto> GetProductListDetails(int userId)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                List<ProductListDto> productlists = context.ProductLists.Where(x => x.UserId == userId).Select(x => new ProductListDto {
                    ProductListId=x.ProductListId,
                    ProductListName=x.ProductListName,
                    CreateDate=x.CreateDate.HasValue ? x.CreateDate.Value.ToShortDateString(): "Tarihi yok",              
                    ShopGo=x.ShopGo,
                    ShopFinish=x.ShopFinish,
                         
                }).ToList(); 
                return productlists;    
            }
        }

        public List<ProductListDto> GetProductListDetailByEmail(string email)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                var result = (from p in context.ProductLists
                              join u in context.Users on p.UserId equals u.UserId
                              where u.Email == email 
                              select new ProductListDto
                              {
                                  ProductListId = p.ProductListId,
                                  ProductListName = p.ProductListName,
                                  CreateDate = p.CreateDate.HasValue ? p.CreateDate.Value.ToShortDateString() : "Tarihi yok",
                                  ShopFinish = p.ShopFinish,
                                  ShopGo = p.ShopGo,                                 
                              }).ToList();
             
                return result;
                //return result==null? new ProductListDto(): result;
            }
        }

        //ProductList in içindeki ürünleri listeleyen metot
        public List<ProductListDetailDto> GetProductListInsideShowWithProductName(int productListId)
        {

            List<ProductListDetailDto> productListDetails = new List<ProductListDetailDto>();

            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                List<ProductListDetailDto> result = context.ProductListDetails.Include(x => x.ProductList).Include(x=>x.Product)
                     .Where(x => x.ProductListId == productListId)
                     .Select(x=> new ProductListDetailDto()
                     {
                         ProductListId = x.ProductListId,
                         ProductId = x.ProductId,
                         IsItBuy = x.IsBuy,
                         ProductListName = x.ProductList.ProductListName,
                         ProductName = x.Product.ProductName,
                         ImageUrl = x.Product.ImageUrl,
                         Description = x.Product.Description
                         
                     }).ToList();

                return result;
            }

            //using (DbShoppingListContext context = new DbShoppingListContext())
            //{
            //    var res = (from prodocutdetails in context.ProductListDetails
            //              join productlist in context.ProductLists on prodocutdetails.ProductListId  equals productlist.ProductListId
            //              join product in context.Products on prodocutdetails.ProductId equals product.ProductId
            //              where prodocutdetails.ProductListId == productListId
            //              select new ProductListDetailDto
            //              {
            //                  ProductListName = productlist.ProductListName,
            //                  ProductId = product.ProductId,
            //                  ProductName = product.ProductName,
            //                  ImageUrl = product.ImageUrl,
            //                  Description = product.Description,
            //                  ProductListId=productlist.ProductListId,
            //                  IsItBuy = prodocutdetails.IsBuy,
                              
            //              }).ToList();

            //    return res;
            //}
        }
  

        public List<ProductListsUsersProductsDto> GetProductShowFromProductList()
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                var result = (from p in context.Products                            
                              select new ProductListsUsersProductsDto
                              {
                                  ProductId = p.ProductId,
                                  ProductName = p.ProductName,                                
                                  ImageUrl = p.ImageUrl,                              
                                  Description = p.Description,
                                 
                              }).ToList();

                return result;

                //var result = context.Products.Include(x => x.ProductLists)
                // .SelectMany(x => x.ProductLists, (p, pl) => new ProductListDetailDto()
                // {
                //     ProductListId = pl.ProductListId,
                //     ProductListName = pl.ProductListName,
                //     ProductName = p.ProductName,
                //     Description = p.Description,
                //     ImageUrl = p.ImageUrl,
                   
                // }).ToList();

                //return result;
            }
        }
        //Productlist e product eklemek için gerekli olan ürünün çekilmesi
        public List<Product> GetProductById(int productId)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                List<Product> result = context.Products
                     .Where(x => x.ProductId == productId)
                     .ToList();               
                return result;
            }
        }
        //hangi listeye hangi ürün ekleniyor//ExecuteSqlRaw query ile ekledik
        public void AddProductListItem(int productId,int productListId)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                var rowsModified = context.Database.ExecuteSqlRaw($"INSERT INTO ProductListDetails VALUES({productId},{productListId})");
            }
        }

        //Hangi listeden hangi ürün silenecek
        public void DeletedProductListItem(int productId, int productListId)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                var rowsExisted = context.Database
                    .ExecuteSqlRaw($"SELECT COUNT(*) FROM ProductListDetails WHERE ProductId='{productId}' AND ProductListId= {productListId}");
                if (rowsExisted == 1)
                {
                    var rowdeleted = context.Database
                   .ExecuteSqlRaw($"DELETE FROM ProductListDetails WHERE ProductId='{productId}' AND ProductListId= '{productListId}'");
                }              
            }
        }

        public void UpdatedProductListItem(int productId, int productListId,bool isItBuy)
        {
            using (DbShoppingListContext context = new DbShoppingListContext())
            {
                //ProductListDetail tablosunda productId ve productListId eşleşmesi var mı(composeteKey)
                //AlışverişListesinde böyle bir ürün var mı
                //string ProductListDetailsQuery = $"SELECT COUNT(*) FROM ProductListDetails WHERE ProductId={productId} AND ProductListId={productListId}";

                //var rowCount = context.Database.ExecuteSqlRaw(ProductListDetailsQuery);

                //if (rowCount == -1) //etkilenen satır sayısı 1 ise kayıt vardır
                //{
                string UpdateQuery = $"UPDATE ProductListDetails SET IsBuy = '{isItBuy}'  WHERE ProductListId={productListId} AND ProductId={productId}";

                var update = context.Database.ExecuteSqlRaw(UpdateQuery);
                //}           
            }
        }

        //HAngi kulalnıcı hangi listesine hangi ürün eklenecek
        //public List<ProductListDetailForProductAddDto> GetProductListAddProductName(int userId,int productListId, int productId)
        //{
        //    using (DbShoppingListContext context = new DbShoppingListContext())
        //    {
        //        List<ProductListDetailForProductAddDto> resultdto = context.Users
        //            .Include(x => x.ProductLists).ThenInclude(x=>x.Products)
        //             .Where(x => x.UserId==userId && x.ProductLists.ProductListId==productListId && x.Products.ProductId==productId)
        //             .SelectMany(x => x.ProductLists, (p, pl) => new ProductListDetailForProductAddDto()
        //             {
        //                 UserId=p.UserId,
        //                 ProductListId = pl.ProductListId,
        //                 ProductId = p.ProductId,
        //                 ProductListName = pl.ProductListName,
        //                 ProductName = p.ProductName,
        //                 ImageUrl = p.ImageUrl,
        //                 Description = p.Description
        //             }).ToList();

        //    }

        //    return result;
        //}

        //using (DbShoppingListContext context = new DbShoppingListContext())
        //{
        //    ProductListDetailForProductAddDto result = context.Products.Include(x => x.ProductLists)
        //         .Where(x => x.ProductId == productId)
        //         .SelectMany(x => x.ProductLists, (p, pl) => new ProductListDetailForProductAddDto()
        //         {
        //             ProductListId = pl.ProductListId,
        //             ProductListName = pl.ProductListName,
        //             ProductName = p.ProductName,
        //             ImageUrl = p.ImageUrl,
        //             Description = p.Description
        //         }).FirstOrDefault();

        //    return result;
        //}
        //}
     
    }
}
