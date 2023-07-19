using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
