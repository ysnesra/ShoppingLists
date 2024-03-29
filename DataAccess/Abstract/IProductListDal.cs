﻿using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductListDal: IEntityRepository<ProductList>
    {
        //Kullanıcıya ait Alışverişlistelerini görüntüleme
        List<ProductListDto> GetProductListDetails(int userId);

        //Kullanıcının mail adresine göre Alışverişlistelerini görüntüleme
        List<ProductListDto> GetProductListDetailByEmail(string email);

        List<ProductListDetailDto> GetProductListInsideShowWithProductName(int ProductListId);

        //AlışverişListesindeli ürünleri gösterirken 'Aldım' seçeneğinin işaretli olmasınıda gösteren metot
        //List<ProductListDetailDto> GetProductListWithIsItBuySelected(int productListId, int productId, bool isItBuy);

      

        //Alışveriş listesi içinde Ürünleri listeleme
        List<ProductListsUsersProductsDto> GetProductShowFromProductList();

        // List<ProductDto> GetProductDetailsWithCategoryName();

        List<Product> GetProductById(int productId);

        void AddProductListItem(int productId, int productListId);

        void DeletedProductListItem(int productId, int productListId);

        void UpdatedProductListItem(int productId, int productListId, bool IsItBuy);
    }
}
