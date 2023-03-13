using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;


namespace WebCoreUI.Controllers
{
    public class ProductListController : Controller
    {
        IProductListService _productListService;
     
        public ProductListController(IProductListService productListService)
        {
            _productListService = productListService;
          
        }
        public IActionResult UserProfile()
        {
            return View();
        }
        //Hangi kullanıcı login olduysa onun Alışveriş listelerini gösteren method
        public IActionResult ProductListByUserId()
        {
            string emailForClaim = User.FindFirstValue("Email");   //Email bilgisini direk cookieden alırız
            var response = _productListService.GetProductListDetailByEmail(emailForClaim).Data;

            return View(response);

            //Claimdeki Id yi almak istesydik:   
            //int userId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        //AlışVerişListesi içindeki ürünleri gösteren method
        public IActionResult ProductListShowInside(int productListId)
        {
            var response = _productListService.GetProductListInsideShowWithProductName(productListId).Data;
            ViewBag.ProductListId=productListId;
            return View(response);
        }

        //AlışVerişListesinin içine Ürünlistesinden ürün ekleme Ekranı
        [HttpGet]
        public IActionResult ProductAddFromProductList(int productListId)
        {
            //Formun içinden eklemeyeceksin ürünler listesinden ekleyeceksin
            //ViewBag.Categories = _productListService.GetBySelectListCategory().Data;
           
            //Alışveriş listesi içinden tüm ürünleri gösteren method
            ViewBag.ProductListId = productListId;

            return View(_productListService.GetProductShowFromProductList().Data);

        }

        //AlışVerişListesinin içine Ürünlistesinden ürün ekleme       
        public IActionResult ProductAdd(int productListId, int productId)
        {
            
            ViewBag.ProductListId = productListId;

            //Claimdeki Id yi almak istesydik:   
            int userId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _productListService.AddProductListItem(productListId,productId,userId);

            return RedirectToAction( nameof(ProductListShowInside), productListId);


        }
        public IActionResult ProductDeleteFromProductList(int productListId,int productId)
        {
            ViewBag.ProductListId = productListId;

            //Claimdeki Id yi alrız:   
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _productListService.DeletedProductListItem( productId, productListId, userId);
            return RedirectToAction(nameof(ProductListShowInside), ViewBag.ProductListId);
        }
        //Alışveriş listesinde Alışverişe Çıkıyorum checkboxı işaretlenince Ajaxın post isteğiyle geldiği action
        [HttpPost]
        public async Task<string> SaveCheckbox(int productListIdSelected, bool shopGoSelected)
        {
            var changeproductList = _productListService.ProductListShopGoSelected(productListIdSelected, shopGoSelected);
           
            if (changeproductList == null)
            {
                return "Hata oluştu";
            }                      
            return "Güncelleme başarılı";
        }

        //Alışveriş listesinin içinde Aldım checkboxı işaretlenince Ajaxın post isteğiyle geldiği action
        [HttpPost]
        public async Task<string> SaveIsItBuyCheckbox(int productListIdSelected, int productIdSelected, bool isItBuySelected)
        {
            ViewBag.ProductListId = productListIdSelected;
            var changeproductList = _productListService.ProductListIsItBuySelected(productListIdSelected, productIdSelected, isItBuySelected);

            if (changeproductList == null)
            {
                return "Hata oluştu";
            }
            return "Güncelleme başarılı";
        }
    }
}
    

