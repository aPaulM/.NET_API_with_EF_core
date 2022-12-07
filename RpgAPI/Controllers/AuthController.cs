using Microsoft.AspNetCore.Mvc;

namespace RpgAPI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
