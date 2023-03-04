using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreUI.Controllers
{  
   [Authorize(Roles ="admin")]

    //Rolü Admin olan buradaki actionları çalıştırabilir
    public class AdminController : Controller
    {
        IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult AdminProfile()
        {
            return View();
        }        
    }
}
