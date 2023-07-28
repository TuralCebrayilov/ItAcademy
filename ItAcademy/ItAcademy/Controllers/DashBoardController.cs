using ItAcademy.Helper;
using ItAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ItAcademy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Error()
        {
            return View();
        }
    }
}
