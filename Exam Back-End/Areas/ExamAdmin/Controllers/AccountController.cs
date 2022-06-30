using Exam_Back_End.Models;
using Exam_Back_End.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Back_End.Areas.ExamAdmin.Controllers
{
    [Area("ExamAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signIn;

        public AccountController(UserManager<AppUser> manager, SignInManager<AppUser> signIn)
        {
            _manager = manager;
            _signIn = signIn;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]


        public async Task<IActionResult> Register(RegisterVM register)
        {
            AppUser user = new AppUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.Username,
                Email = register.Email
            };

            if (register.Term == true)
            {
                IdentityResult result = await _manager.CreateAsync(user, register.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "You cannot registration without our conditions!");
                return View();
            }

            return RedirectToAction("Index", "DashBoard");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _manager.FindByNameAsync(login.Username);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signIn.PasswordSignInAsync(user, login.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect password or login");
                return View();
            }

            return RedirectToAction("Index", "DashBoard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Show()
        {
            return Json(User.Identity.Name == null);
        }

        public async Task<IActionResult> Edit()
        {
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);

            EditUserVM editUser = new EditUserVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
            };

            return View(editUser);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(EditUserVM edit)
        {
            AppUser existed = await _manager.FindByNameAsync(User.Identity.Name);

            EditUserVM editUser = new EditUserVM
            {
                FirstName = edit.FirstName,
                LastName = edit.LastName,
                Username = edit.Username
            };
            if (!ModelState.IsValid) return View(editUser);

            bool cases = edit.CurrentPassword != null && edit.Password == null && edit.ConfirmPassword == null;

            if(edit.Email == null && edit.Email != existed.Email)
            {
                ModelState.AddModelError("", "Cannot chaged email!");
                return View(editUser);
            }

            if (cases)
            {
                existed.FirstName = edit.FirstName;
                existed.LastName = edit.LastName;
                existed.UserName = edit.Username;
                await _manager.UpdateAsync(existed);
            }
            else{
                existed.FirstName = edit.FirstName;
                existed.LastName = edit.LastName;
                existed.UserName = edit.Username;

                IdentityResult result = await _manager.ChangePasswordAsync(existed, edit.CurrentPassword, edit.Password);

                if (!result.Succeeded)
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }
            }

            return RedirectToAction("Index", "DashBoard");
        }
    }
}

