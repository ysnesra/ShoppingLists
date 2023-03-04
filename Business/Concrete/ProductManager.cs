using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Dependency Injection____soyut nesne ile bağlantı kurma
        IProductDal _productDal;
        //IFileHelper _fileHelper;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
            //_fileHelper = fileHelper;
        }
       
        public IResult Add(Product product)
        {   
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult AddProductDto(ProductDto productDto)
        {
            //Aynı ürün isminde ürün var mı kontrol edelim
            var dbProduct = _productDal.Get(x => x.ProductName.ToLower() == productDto.ProductName.ToLower());
            if (dbProduct is null)   //db de bu mail yok demekki ekleyebilriz
            {
                Product newProduct = new Product()
                {
                    ProductName = productDto.ProductName,
                    UnitInStock=productDto.UnitInStock,
                    CategoryId=productDto.CategoryId,  
                    Description = productDto.Description,   
                    ImageUrl=productDto.ImageUrl
                };
                _productDal.Add(newProduct);

                return new SuccessResult(Messages.UserAdded);
            }
            return new ErrorResult(Messages.NoAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //ProductDal daki GetAll metotunu çağırılarak ürünler listelendi
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);  
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==categoryId));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productId),Messages.ProductDetail);
        }
        
        //Kategoriyi liste şeklinde getiren method
        public IDataResult<List<SelectListItem>> GetBySelectListCategory()
        {

            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in _productDal.GetProductDetails().Select(a => new { a.CategoryId, a.CategoryName }))
            {
                selectList.Add(new SelectListItem() { Text = item.CategoryName, Value = item.CategoryId.ToString() });
            }
            return new SuccessDataResult<List<SelectListItem>>(selectList) ;

        }

        //Categoriye ait ürünleri listeleme
        public IDataResult<List<ProductDto>> GetProductByCategory(int categoryId)
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetProductByCategory(categoryId));
        }

        //Ürün ismine göre ürün detayını getirme
        public IDataResult<ProductDto> GetProductDetailByProductName(string productName)
        {

            return new SuccessDataResult<ProductDto>(_productDal.GetProductDetailByProductName(productName));
        }
     
        public IDataResult<List<ProductDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
