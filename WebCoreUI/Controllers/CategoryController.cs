using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebCoreUI.Models;

namespace WebCoreUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult List()
        { 
            return View(_categoryService.GetCategoryList().Data);
        }
    }
}
