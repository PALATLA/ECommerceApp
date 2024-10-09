using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
