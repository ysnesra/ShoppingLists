using Business.Abstract;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebCoreUI.Models;

namespace WebCoreUI.Controllers
{
   [Authorize]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult List()
        {
            return View(_userService.GetUserLists().Data);

        }
        //Yeni Kullanıcı FormEkranı
        [HttpGet]
        public IActionResult Create()
        {
            return View("UserForm", new UserDetailRegisterDto());
        }

        [HttpPost]
        public IActionResult Create(UserDetailRegisterDto userAdd)
        {          
            _userService.AddUserDto(userAdd);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Edit(int userId)
        {
            //var user=_userService.GetById(userId).Data;
            var user = _userService.GetByUserIdDto(userId).Data;
            return View("UserEditForm", user);
        }
        [HttpPost]
        public IActionResult Edit(UserEditDto userEdit)
        {
            _userService.EditUserDto(userEdit);
            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int userId)
        {
            _userService.DeleteUserDto(userId);
            return RedirectToAction(nameof(List));
        }

        //ÜyeGirişi Formu
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserLoginDto model)
        {
            
            if (ModelState.IsValid)
            {
                //Mail ve şifre eşleşiyor mu
                var existUser = _userService.GetByLoginFilter(model).Data;

                //Claims listesi oluşturp cookide neleri tutulacağı tanımlandı
                //ClaimTypes.... diyerek hazır claim nesnelerinde tutar
                List<Claim> claims = new List<Claim>(); 
                claims.Add(new Claim(ClaimTypes.NameIdentifier, existUser.UserId.ToString()));
                claims.Add(new Claim("Email", existUser.Email));
                claims.Add(new Claim(ClaimTypes.Role, existUser.Role));

                //ClaimsIdentity nesnesi içerisine claimleri ekliyor.Hangi Authentication ı kullanılıyorsa onu da parametre olarak verilir//CookieAuthentication
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme
                    );
                //ClaimsPrincipal bizden ClaimsIdentity nesnemizi ister
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        //ÜyeOl formu
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(UserDetailRegisterDto model)
        {
            if (ModelState.IsValid)
            {                    
                _userService.AddUserDto(model);
                return RedirectToAction(nameof(Login));
            }
            //hata varsa View a verileri geri gönder hatasını görsün
            ModelState.AddModelError("", "Kullanıcı üye olma işlemi gerçekleşmedi");
            return View(model);
            
        }
     
        //public IActionResult Profile()
        //{
        //    return View();
        //}
        
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  //Cookieyi kapat
            return RedirectToAction(nameof(Login));
        }

    }
}
