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
    public class StudentController : Controller
    {
        private readonly AppDbContext _Db;
        private readonly IWebHostEnvironment _env;
        public StudentController(AppDbContext Db, IWebHostEnvironment env)
        {

            _Db = Db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Students> students = await _Db.Students.Include(x => x.Courses).Include(x => x.Groups).ToListAsync();
            return View(students);
        }
        #region create
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _Db.Courses.ToListAsync();
            ViewBag.Groups = await _Db.Groups.ToListAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Students students, int CatId,int GatId)
        {
            ViewBag.Courses = await _Db.Courses.ToListAsync();
            ViewBag.Groups = await _Db.Groups.ToListAsync();

            #region Save Image


            if (students.Photo == null)
            {
                ModelState.AddModelError("Photo", "Image can't be null!!");
                return View();
            }
            if (!students.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image type");
                return View();
            }
            if (students.Photo == null)
            {
                ModelState.AddModelError("Photo", "max 1mb !!");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "images");
            students.Image = await students.Photo.SaveFileAsync(folder);
            #endregion
            #region Exist Item
            bool isExist = await _Db.Students.AnyAsync(x => x.Name == students.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This students is already exist !");
                return View(students);
            }
            #endregion
            students.GroupsId = CatId;
            students.CoursesId = CatId;
            await _Db.Students.AddAsync(students);
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
            Students _DbStudents = await _Db.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbStudents == null)
            {
                return BadRequest();
            }
            ViewBag.Courses = await _Db.Courses.ToListAsync();
            ViewBag.Groups = await _Db.Groups.ToListAsync();
            return View(_DbStudents);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Students students, int CatId , int GatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Students _DbStudents = await _Db.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbStudents == null)
            {
                return BadRequest();
            }
            ViewBag.Courses = await _Db.Courses.ToListAsync();
            ViewBag.Groups = await _Db.Groups.ToListAsync();
            //#region Exist Item
            //bool isExist = await _Db.Teachers.AnyAsync(x => x.Name == teachers.Name && CatId != id);
            //if (isExist)
            //{
            //    ModelState.AddModelError("Name", "This teachers is already exist !");
            //    return View(teachers);
            //}
            //#endregion
            #region Save Image


            if (students.Photo != null)
            {
                if (!students.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil seçin!!");
                    return View();
                }
                if (students.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "images");
                _DbStudents.Image = await students.Photo.SaveFileAsync(folder);

            }

            #endregion
            _DbStudents.Name = students.Name;
            _DbStudents.Payment = students.Payment;
            _DbStudents.Birthday = students.Birthday;
            _DbStudents.Mobil = students.Mobil;
          
           
            _DbStudents.GroupsId = GatId;
            _DbStudents.CoursesId = CatId;

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
            Students _DbStudents = await _Db.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbStudents == null)
            {
                return BadRequest();
            }
            if (_DbStudents.IsDeactive)
            {
                _DbStudents.IsDeactive = false;
            }
            else
            {
                _DbStudents.IsDeactive = true;
            }
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
