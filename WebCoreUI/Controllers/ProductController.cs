using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Helpers.GuidHelpers;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using WebCoreUI.Models;

namespace WebCoreUI.Controllers
{
    public class ProductController : Controller
    {
        //Loosely coupled//zayıf bağımlılık

        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
 
        public IActionResult List()
        {     
            return View(_productService.GetProductDetails().Data);  
        }

        //Kategoriye ait ürünler listesi
        public IActionResult ListByCategory(int categoryId)
        { 
            return View("List", _productService.GetProductByCategory(categoryId).Data);
        }
        //Yeni Ürün Ekleme FormEkranı
        [HttpGet]
        public IActionResult Create(int page)
        {
            ViewBag.Categories = _productService.GetBySelectListCategory().Data;
            return View("ProductAddForm", new ProductDto());
        }

        [HttpPost]
        public IActionResult Create(ProductDto productAdd,IFormFile file)
        {    
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            if(file !=null && file.Length > 0)
            {
                //Directory=>System.IO'nın bir class'ı.
                //Path.GetExtension(file.FileName)=>> Seçmiş olduğumuz dosyanın uzantısını elde ediyoruz.(.jpg mi .png mi .txt mi)
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                bool exists = allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower());

                if (!exists)
                {
                    ModelState.AddModelError("", $" İzin Verilen dosya uzantıları {String.Join(",", allowedExtensions)}");
                    return View();
                }
                ////GuidHelper clasındaki CreateGuid metotunu çağırıp eşsiz bir guid stringi oluşturuyoruz
                //string guid = GuidHelper.CreateGuid();
                //var fileName = guid + $"{productAdd.ProductName.ToLower()}" + Path.GetExtension(file.FileName);

                //dosya isminin formatı belirlenir elma-0.jpg
                var fileName = $"{productAdd.ProductName.ToLower()}-{productAdd.ProductId}" + Path.GetExtension(file.FileName);

                //dosyanın kaydedileceği dizin belirlenir
                var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/products");

                if (!System.IO.File.Exists(FolderPath)) //böyle bir dizin yoksa oluşturur
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }

                //dosyanın kaydedileceği dizin + dosya adı birleştirilir
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/products", fileName);

                //Stream tipi ile taşındığından path FileStream olarak kopyası oluşturulur
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);

                    productAdd.ImageUrl = "/uploads/products/" + fileName;
                   
                }

            }

            _productService.AddProductDto(productAdd);
            return RedirectToAction(nameof(List));
        }
    }
}
