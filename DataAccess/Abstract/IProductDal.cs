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
    public interface IProductDal : IEntityRepository<Product>
    {
       List<ProductDto> GetProductDetails();
       List<ProductDto> GetProductByCategory(int categoryId);

        ProductDto GetProductDetailByProductName(string productName);
    }
}



