using Microsoft.AspNetCore.Mvc;

namespace ItAcademy.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
