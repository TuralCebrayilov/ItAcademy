using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Controllers
{
    public class PositionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
