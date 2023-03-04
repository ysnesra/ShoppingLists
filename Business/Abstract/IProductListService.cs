using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductListService
    {
        IDataResult<List<ProductList>> GetAll();
        IDataResult<ProductList> GetByUserId(int userId);

        IDataResult<List<ProductListDto>> GetProductListDetails(int userId);
        IDataResult<List<ProductListDto>> GetProductListDetailByEmail(string email); 
        IDataResult<List<ProductListDetailDto>> GetProductListInsideShowWithProductName(int productListId);
        //IDataResult<List<SelectListItem>> GetBySelectListCategory();

        IDataResult<List<ProductListsUsersProductsDto>> GetProductShowFromProductList();

        //ProductListDetailForProductAddDto modelinden product ı alışveriş listesi içine ekleme
        //IResult AddProductListDetailForProductAddDto(ProductListDetailForProductAddDto productAdd);

        IDataResult<ProductListDetailDto> AddProductListItem(int productListId, int productId, int userId);

        IResult Add(ProductList productList);
        IResult Update(ProductList productList);
        IResult Delete(ProductList productList);
    }
}
