using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
