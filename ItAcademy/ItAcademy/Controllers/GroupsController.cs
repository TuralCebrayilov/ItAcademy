using ItAcademy.DAL;
using ItAcademy.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItAcademy.Controllers
{
    public class GroupsController : Controller
    {

        private readonly AppDbContext _Db;
        private readonly IWebHostEnvironment _env;
        public GroupsController(AppDbContext Db, IWebHostEnvironment env)
        {

            _Db = Db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Groups> groups = await _Db.Groups.ToListAsync();

            return View(groups);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Groups groups)
        {
            bool isExist = await _Db.Groups.AnyAsync(x => x.Name == groups.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu kurs artıq mövcuddur !");
                return View(groups);
            }
            await _Db.Groups.AddAsync(groups);
            await _Db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Groups _DbGroups = await _Db.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbGroups == null)
            {
                return BadRequest();
            }
            return View(_DbGroups);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Groups groups)
        {
            if (id == null)
            {
                return NotFound();
            }
            Groups _DbGroups = await _Db.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbGroups == null)
            {
                return BadRequest();
            }
            bool isExist = await _Db.Groups.AnyAsync(x => x.Name == groups.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This groups is already exist !");
                return View(groups);
            }

            _DbGroups.Name = groups.Name;
            
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {

            Groups groups = await _Db.Groups.FindAsync(id);


            //_DbGroups.Name = service.Name;
            //_DbGroups.Description = service.Description;

            return View(groups);
        }
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Courses _DbGroups = await _Db.Courses.FirstOrDefaultAsync(x => x.Id == id);
        //    if (_DbGroups == null)
        //    {
        //        return BadRequest();
        //    }
        //    return View(_DbGroups);

        //}
        //[HttpPost]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeletePost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Courses _DbGroups = await _Db.Courses.FirstOrDefaultAsync(x => x.Id == id);
        //    if (_DbGroups == null)
        //    {
        //        return BadRequest();
        //    }
        //    _DbGroups.IsDeactive = true;
        //    await _Db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Groups _DbGroups = await _Db.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbGroups == null)
            {
                return BadRequest();
            }
            if (_DbGroups.IsDeactive)
            {
                _DbGroups.IsDeactive = false;
            }
            else
            {
                _DbGroups.IsDeactive = true;
            }
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
