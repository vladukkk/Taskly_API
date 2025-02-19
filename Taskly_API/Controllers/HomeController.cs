using Microsoft.AspNetCore.Mvc;

namespace Taskly_API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
