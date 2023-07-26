using ItAcademy.Models;
using ItAcademy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItAcademy.Controllers
{
   
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly SignInManager<AppUser> _signInManager;
        public UsersController(UserManager<AppUser> userManager,
         RoleManager<IdentityRole> roleManager
         /*SignInManager<AppUser> signInManager*/)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> DbUsers =await _userManager.Users.ToListAsync();
            List<UserVM> usersVM = new List<UserVM>();
            foreach (AppUser Dbuser in DbUsers)
            {
                UserVM userVm = new UserVM
                {
                    Name = Dbuser.Name,
                    Email = Dbuser.Email,
                    Username = Dbuser.UserName,
                    IsDeactive = Dbuser.IsDeactive,
                    Id = Dbuser.Id,
                    Role = (await _userManager.GetRolesAsync(Dbuser))[0]
                };
                usersVM.Add(userVm);
            }
            return View(usersVM);
        }
        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }
        #endregion
        #region Activity
        public async Task<IActionResult> Activity(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return BadRequest();
            }
            if (appUser.IsDeactive)
            {
                appUser.IsDeactive = false;
            }
            else
            {
                appUser.IsDeactive = true;
            }
            await _userManager.UpdateAsync(appUser);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
