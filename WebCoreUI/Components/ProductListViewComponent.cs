using Microsoft.AspNetCore.Mvc;

namespace WebCoreUI.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
