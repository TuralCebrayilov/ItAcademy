using ItAcademy.DAL;
using ItAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItAcademy.Controllers
{
    public class CostController : Controller
    {
        private readonly AppDbContext _Db;
        public CostController(AppDbContext Db)
        {
            _Db = Db;
        }
        public async Task<IActionResult> Index()
        {
            List<Cost> costs = await _Db.Costs.ToListAsync();
            //  if (!string.IsNullOrEmpty(search))
            //  {
            //      var benefitDb = from b in _Db.Benefits select/* d*/;
            //      benefits = await _Db.Benefits.Where(x => x.Description.Contains(/*search*/)).OrderByDescending(x => x.Id).ToListAsync();
            //return View(benefits);
            //  }

            //  decimal take = 4;
            //  ViewBag.PageCount=Math.Ceiling((decimal)(await _Db.Benefits.CountAsync()/take));
            //  ViewBag.Currentpage = /*page*/;
            //  benefits = await _Db.Benefits.OrderByDescending(x => x.Id).Skip((/*page-1*/)*4).Take((int)take).ToListAsync();



            return View(costs);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cost cost)
        {
            if (cost.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Məbləğ düzgün daxil edilməyib.");
                return View();
            }
            cost.By = User.Identity.Name;
            Budget budget = await _Db.Budgets.FirstOrDefaultAsync();
            budget.LastModifiedDescription = cost.Description;
            budget.LastModifiedDate = DateTime.UtcNow.AddHours(4);
            budget.LastModifiedAmount = cost.Amount;
            budget.LastModifiedBy = cost.By;
            budget.TotalBudget -= cost.Amount;
            await _Db.Costs.AddAsync(cost);
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
