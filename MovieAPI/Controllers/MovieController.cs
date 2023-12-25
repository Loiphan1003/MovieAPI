using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
