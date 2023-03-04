using Core.DataAccess;
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

        //Alışveriş listesi içinde Ürünleri listeleme
        List<ProductListsUsersProductsDto> GetProductShowFromProductList();

        // List<ProductDto> GetProductDetailsWithCategoryName();

        List<Product> GetProductById(int productId);

        void AddProductListItem(int productId, int productListId);
    }
}
