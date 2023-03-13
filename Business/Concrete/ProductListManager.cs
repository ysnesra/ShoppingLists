using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductListManager : IProductListService
    {
        IProductListDal _productListDal;

        public ProductListManager(IProductListDal productListDal)
        {
            _productListDal = productListDal;
        }

        public IResult Add(ProductList productList)
        {
            _productListDal.Add(productList);
            return new SuccessResult(Messages.ProductListAdded);
        }

        public IResult Delete(ProductList productList)
        {
            _productListDal.Delete(productList);
            return new SuccessResult(Messages.ProductListDeleted);
        }

        public IDataResult<List<ProductList>> GetAll()
        {
            return new SuccessDataResult<List<ProductList>>(_productListDal.GetAll(),Messages.ProductListListed);
        }

        public IDataResult<ProductList> GetByUserId(int userId)
        {
            return new SuccessDataResult<ProductList>(_productListDal.Get(p=>p.UserId == userId),Messages.ProductListDetail);     
        }
        public IDataResult<List<ProductListDto>> GetProductListDetails(int userId)
        {
            return new SuccessDataResult<List<ProductListDto>>(_productListDal.GetProductListDetails(userId), Messages.ProductListListed);
        }
        public IDataResult<List<ProductListDto>> GetProductListDetailByEmail(string email)
        {
            var result = _productListDal.GetProductListDetailByEmail(email);

            if (result == null)
            {
                return new ErrorDataResult<List<ProductListDto>>(Messages.EmailInvalid);
            }
            return new SuccessDataResult<List<ProductListDto>>(result, Messages.ProductListDetail);
        }
        public IDataResult<ProductListDetailDto> AddProductListItem(int productListId, int productId, int userId)
        {         
            try
            {
                //List<Product> dbproduct = _productListDal.GetProductById(productId);

                //var productlist = new ProductList
                //{
                //    UserId = userId,
                //    ProductListId = productListId,
                //    Products = dbproduct,

                //};
                _productListDal.AddProductListItem(productId, productListId);

                return new SuccessDataResult<ProductListDetailDto>();
            }
            catch (Exception)
            {

                return new ErrorDataResult<ProductListDetailDto>();
            }
            
        }
        public IDataResult<ProductListDetailDto> DeletedProductListItem(int productId, int productListId, int userId)
        {
            try
            {              
                _productListDal.DeletedProductListItem(productId, productListId);

                return new SuccessDataResult<ProductListDetailDto>();
            }
            catch (Exception)
            {

                return new ErrorDataResult<ProductListDetailDto>();
            }

        }
        public IResult Update(ProductList productList)
        {
            _productListDal.Update(productList);
            return new SuccessResult(Messages.ProductListUpdated);
        }

        public IDataResult<List<ProductListDetailDto>> GetProductListInsideShowWithProductName(int productListId)
        {
            return new SuccessDataResult<List<ProductListDetailDto>>(_productListDal.GetProductListInsideShowWithProductName(productListId));
        }

        public IDataResult<List<ProductListsUsersProductsDto>> GetProductShowFromProductList()
        {
            return new SuccessDataResult<List<ProductListsUsersProductsDto>>(_productListDal.GetProductShowFromProductList());
        }

        //Alışverişe Çıkıyorum checkboxı seçildikten sonra db ye kaydetme
        public IDataResult<ProductListDto> ProductListShopGoSelected(int productListIdSelected, bool shopGoSelected)
        {
            var productListChange = _productListDal.Get(x=>x.ProductListId==productListIdSelected); 
            if(productListChange!=null)
            {      
                productListChange.ShopGo= shopGoSelected;
                _productListDal.Update(productListChange);

                return new SuccessDataResult<ProductListDto>();
            }
            return new ErrorDataResult<ProductListDto>();
        }
       
        public IDataResult<ProductListDetailDto> ProductListIsItBuySelected(int productListIdSelected, int productIdSelected, bool isItBuySelected)
        {
            //var productListChange = _productListDal.Get(x => x.ProductListId == productIdSelected);
            var productListChange = _productListDal.GetProductListWithIsItBuySelected(productListIdSelected, productIdSelected, isItBuySelected).FirstOrDefault(x => x.ProductId == productIdSelected);
           
            if (productListChange != null)
            {
                productListChange.IsItBuy = isItBuySelected;
                var dbproductList = new ProductList
                {
                    ProductListId = productListIdSelected,
                    ProductListName = productListChange.ProductListName,
                    IsItBuy = productListChange.IsItBuy,     
                };
                _productListDal.Update(dbproductList);

                return new SuccessDataResult<ProductListDetailDto>();
            }
            return new ErrorDataResult<ProductListDetailDto>();
        }

        //public IDataResult<ProductListDetailDto> DeleteProductListItem(int productId,int productListId,int userId)
        //{
        //    //Kullanının listesinde böyle bir ürün var mı? 
        //    ProductList dbproductlist = _productListDal.Get(x => x.ProductListId == productListId && x.UserId == userId);

        //}


        //public IDataResult<ProductListDetailForProductAddDto> GetProductListAddProductName(int productListId, ProductListDetailForProductAddDto productAdddto)
        //{
        //    var productdto=_productListDal.Get(x=>x.ProductListId==productListId);
        //    return new SuccessDataResult<ProductListDetailForProductAddDto>(result);
        //}


        //Kategoriyi liste şeklinde getiren method
        //public IDataResult<List<SelectListItem>> GetBySelectListCategory()
        //{

        //    List<SelectListItem> selectList = new List<SelectListItem>();

        //    foreach (var item in _productListDal.GetProductDetailsWithCategoryName().Select(a => new { a.CategoryId, a.CategoryName }))
        //    {
        //        selectList.Add(new SelectListItem() { Text = item.CategoryName, Value = item.CategoryId.ToString() });
        //    }
        //    return new SuccessDataResult<List<SelectListItem>>(selectList);

        //}
    }
}
