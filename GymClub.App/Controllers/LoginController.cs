using Microsoft.AspNetCore.Mvc;

namespace GymClub.App.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
