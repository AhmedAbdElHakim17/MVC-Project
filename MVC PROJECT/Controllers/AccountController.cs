using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_PROJECT.Enums;
using MVC_PROJECT.Models;
using MVC_PROJECT.ViewModels;

namespace MVC_PROJECT.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            Student student = await unitOfWork.Students.FindAsync(s => s.Email == userViewModel.Email);
            if (student != null)
            {
                return await CreateUserWithRole(userViewModel, UserRoles.Student.ToString(), student);
            }
            Instructor instructor = await unitOfWork.Instructors.FindAsync(s => s.Email == userViewModel.Email);
            if (instructor != null)
            {
                return await CreateUserWithRole(userViewModel, UserRoles.Instructor.ToString(), instructor);
            }
            ModelState.AddModelError("", "No student or instructor found for this email.");
            return View(userViewModel);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel userVM)
        {
            if (!ModelState.IsValid)
                return View(userVM);
            AppUser appUser = await unitOfWork.UserManager.FindByEmailAsync(userVM.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Email or Password is Wrong");
                return View(userVM);
            }
            var result = await unitOfWork.SignInManager.PasswordSignInAsync(appUser, userVM.Password, userVM.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email or Password is Wrong");
            return View(userVM);
        }
        public async Task<IActionResult> SignOut()
        {
            await unitOfWork.SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        private async Task<ActionResult> CreateUserWithRole(RegisterUserViewModel userViewModel, string Role, Object obj)
        {
            AppUser user = new AppUser()
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
            };
            IdentityResult result = await unitOfWork.UserManager.CreateAsync(user, userViewModel.Password);
            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(userViewModel);
            }
            IdentityResult roleResult = await unitOfWork.UserManager.AddToRoleAsync(user, Role);
            if (!roleResult.Succeeded)
            {
                AddErrors(roleResult);
                return View(userViewModel);
            }
            if (obj is Student student)
            {
                user.Address = student.Address;
                student.UserId = user.Id;
                unitOfWork.Students.Update(student);
            }
            else if (obj is Instructor instructor)
            {
                instructor.UserId = user.Id;
                unitOfWork.Instructors.Update(instructor);
            }
            else
            {
                ModelState.AddModelError("", "Invalid user type.");
                return View(userViewModel);
            }
            await unitOfWork.CompleteAsync();
            await unitOfWork.SignInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}

