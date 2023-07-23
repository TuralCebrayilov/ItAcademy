using ItAcademy.DAL;
using ItAcademy.Helper;
using ItAcademy.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ItAcademy.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _Db;
        private readonly IWebHostEnvironment _env;
        public TeachersController(AppDbContext Db, IWebHostEnvironment env)
        {

            _Db = Db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Teachers> teachers = await _Db.Teachers.Include(x => x.Courses).ToListAsync();
            return View(teachers);
        }
        #region create
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _Db.Courses.ToListAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teachers teachers, int CatId)
        {
            ViewBag.Courses = await _Db.Courses.ToListAsync();

            #region Save Image


            if (teachers.Photo == null)
            {
                ModelState.AddModelError("Photo", "Image can't be null!!");
                return View();
            }
            if (!teachers.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image type");
                return View();
            }
            if (teachers.Photo == null)
            {
                ModelState.AddModelError("Photo", "max 1mb !!");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "images");
            teachers.Image = await teachers.Photo.SaveFileAsync(folder);
            #endregion
            #region Exist Item
            bool isExist = await _Db.Teachers.AnyAsync(x => x.Name == teachers.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This teachers is already exist !");
                return View(teachers);
            }
            #endregion
            teachers.CoursesId = CatId;
            await _Db.Teachers.AddAsync(teachers);
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teachers _DbTeacers = await _Db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbTeacers == null)
            {
                return BadRequest();
            }
            ViewBag.Courses = await _Db.Courses.ToListAsync();
            return View(_DbTeacers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teachers teachers, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teachers _DbTeacers = await _Db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbTeacers == null)
            {
                return BadRequest();
            }
            ViewBag.Courses = await _Db.Courses.ToListAsync();
            //#region Exist Item
            //bool isExist = await _Db.Teachers.AnyAsync(x => x.Name == teachers.Name && CatId != id);
            //if (isExist)
            //{
            //    ModelState.AddModelError("Name", "This teachers is already exist !");
            //    return View(teachers);
            //}
            //#endregion
            #region Save Image


            if (teachers.Photo != null)
            {
                if (!teachers.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil seçin!!");
                    return View();
                }
                if (teachers.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "images");
                _DbTeacers.Image = await teachers.Photo.SaveFileAsync(folder);

            }

            #endregion
            _DbTeacers.Name = teachers.Name;
            _DbTeacers.Salary = teachers.Salary;
            _DbTeacers.Birthday = teachers.Birthday;
            _DbTeacers.PhoneNumber = teachers.PhoneNumber;

            _DbTeacers.CoursesId = CatId;

            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teachers _DbTeachers = await _Db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbTeachers == null)
            {
                return BadRequest();
            }
            if (_DbTeachers.IsDeactive)
            {
                _DbTeachers.IsDeactive = false;
            }
            else
            {
                _DbTeachers.IsDeactive = true;
            }
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
      
    }
}
