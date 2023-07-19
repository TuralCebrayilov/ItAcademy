using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Controllers
{
    public class EmployersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
