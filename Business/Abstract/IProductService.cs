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
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);  
        IDataResult<Product> GetById(int productId); //tek bir ürünü getirir.//ürün detayına gitme

        IDataResult<List<ProductDto>> GetProductDetails(); 
        IDataResult<List<ProductDto>> GetProductByCategory(int categoryId);  //Categoriye ait ürünleri listeleme    
        IDataResult<ProductDto> GetProductDetailByProductName(string productName);  //Ürün ismine göre tek bir ürünü getirir
        IDataResult<List<SelectListItem>> GetBySelectListCategory();

        IResult AddProductDto(ProductDto productDto);   
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
    }
}
