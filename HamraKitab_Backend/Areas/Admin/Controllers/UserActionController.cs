using Microsoft.AspNetCore.Mvc;

namespace HamraKitab_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserActionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
