using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
