using MahasDemo.DAL.Data.Model;
using MahasDemo.PL.Models.AccountViews;
using MahasDemo.PL.Models.UsersViews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MahasDemo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users = await _userManager.Users.Select(u=>new UsersViewModel
                {
                    Id= u.Id,
                    Fname= u.Fname,
                    Lname= u.Lname,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToListAsync();
                return View(users);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var mappedUser = new UsersViewModel
                    {
                        Id= user.Id,
                        Fname= user.Fname,
                        Lname= user.Lname,
                        Email= user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = await _userManager.GetRolesAsync(user)
                    };
                    return View(new List<UsersViewModel>{ mappedUser });
                }
                return View(new List<UsersViewModel>());
            }
        }

        public async Task<IActionResult> Details(string? id,string viewName="Details")
        {
            if (id is null)
                return BadRequest();
            var user =await _userManager.FindByIdAsync(id);
            if(user == null)
                return NotFound();
            var mappedUser = new UsersViewModel
            {
                Id = user.Id,
                Fname = user.Fname,
                Lname = user.Lname,
                Email= user.Email,
                PhoneNumber= user.PhoneNumber,
                Roles =await _userManager.GetRolesAsync(user)
            };
            return View(viewName,mappedUser);
        }

        public Task<IActionResult> Edit(string? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string? id,UsersViewModel userVM)
        {
            if(id != userVM.Id)
                return BadRequest();
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            user.PhoneNumber= userVM.PhoneNumber;
            user.Fname= userVM.Fname;
            user.Lname= userVM.Lname;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return RedirectToAction("Index", "User");

            foreach(var item in result.Errors)
                 ModelState.AddModelError("",item.Description);
            return View(userVM);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if(id == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction("Index");
            foreach (var item in result.Errors)
                ModelState.AddModelError("", item.Description);

            return BadRequest();
        }
    }
}
