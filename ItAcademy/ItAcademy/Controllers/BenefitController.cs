using ItAcademy.DAL;
using ItAcademy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItAcademy.Controllers
{
    public class BenefitController : Controller
    {
        private readonly AppDbContext _Db;
        private readonly UserManager<AppUser> _userManager;
        public BenefitController(AppDbContext Db, UserManager<AppUser> userManager)
        {
                _Db = Db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Benefit> benefits= await _Db.Benefits.ToListAsync();
            
            /*List<Benefit> benefits = new List<Benefit>();*/
            //if (!string.IsNullOrEmpty(Search))
            //{
            //    var benefitDb = from b in _Db.Benefits select d;
            //    benefits = await _Db.Benefits.Where(x => x.Description.Contains(search)).OrderByDescending(x => x.Id).ToListAsync();
            //    return View(benefits);
            //}

            //decimal take = 4;
            //ViewBag.PageCount = Math.Ceiling((decimal)(await _Db.Benefits.CountAsync() / take));
            //ViewBag.Currentpage = page;
            //benefits = await _Db.Benefits.OrderByDescending(x => x.Id).Skip((page - 1) * 4).Take((int)take).ToListAsync();



            return View(benefits);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Benefit benefit)
        {
            if (benefit.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Məbləğ düzgün daxil edilməyib.");
                return View();
            }
            benefit.By = User.Identity.Name;
            Budget budget = await _Db.Budgets.FirstOrDefaultAsync();
            budget.LastModifiedDescription = benefit.Description;
            budget.LastModifiedDate = DateTime.UtcNow.AddHours(4);
            budget.LastModifiedAmount = benefit.Amount;
            budget.LastModifiedBy = benefit.By;
            budget.TotalBudget += benefit.Amount;

            await _Db.Benefits.AddAsync(benefit);
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
