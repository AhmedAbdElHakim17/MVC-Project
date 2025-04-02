using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC_PROJECT.Models;
using MVC_PROJECT.Repositories;
using MVC_PROJECT.ViewModels;

namespace MVC_PROJECT.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public InstructorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Index()
        {
            var Instuctors = await unitOfWork.Instructors.GetAllAsync(nameof(Department));
            List<InstructorViewModel> list = new List<InstructorViewModel>();
            foreach (var item in Instuctors)
            {
                list.Add(new InstructorViewModel
                {
                    Id = item.Id,
                    fName = item.fName,
                    lName = item.lName,
                    Salary = item.Salary,
                    ImageUrl = item.ImageUrl,
                    Age = item.Age,
                    HiringDate = item.HiringDate,
                    DeptId = item.DeptId,
                    DeptName = item.Department.Name,
                    Courses = await unitOfWork.Courses.GetAllAsync(),
                    Departments = await unitOfWork.Departments.GetAllAsync(),
                });
            }
            return View(list);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var insModel = new InstructorViewModel()
            {
                Courses = await unitOfWork.Courses.GetAllAsync(),
                Departments = await unitOfWork.Departments.GetAllAsync()
            };
            return View(insModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(InstructorViewModel ins)
        {
            if (ModelState.IsValid)
            {
                var inst = new Instructor()
                {
                    Id = ins.Id,
                    fName = ins.fName,
                    lName = ins.lName,
                    Email = ins.Email,
                    Salary = ins.Salary,
                    ImageUrl = ins.ImageUrl,
                    Age = ins.Age,
                    HiringDate = ins.HiringDate,
                    DeptId = ins.DeptId,
                };
                await unitOfWork.Instructors.AddAsync(inst);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            ins.Departments = await unitOfWork.Departments.GetAllAsync();
            ins.Courses = await unitOfWork.Courses.GetAllAsync();
            return View(ins);
        }
        [HttpGet]
        [Authorize(Roles = "HR,Admin")]
        public async Task<IActionResult> Edit(int id)
        {

            var ins = await unitOfWork.Instructors.GetByIdAsync(id);
            if (ins == null) return NotFound();
            var insVM = new InstructorViewModel()
            {
                Id = ins.Id,
                fName = ins.fName,
                lName = ins.lName,
                Email = ins.Email,
                Salary = ins.Salary,
                ImageUrl = ins.ImageUrl,
                Age = ins.Age,
                HiringDate = ins.HiringDate,
                DeptId = ins.DeptId,
                DeptName = (await unitOfWork.Departments.GetByIdAsync(ins.DeptId)).Name,
                Courses = await unitOfWork.Courses.GetAllAsync(),
                Departments = await unitOfWork.Departments.GetAllAsync(),
            };
            return View(insVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InstructorViewModel insVM, int id)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(insVM.Email);
                var ins = new Instructor()
                {
                    Id = id,
                    fName = insVM.fName,
                    lName = insVM.lName,
                    Email = (await unitOfWork.Instructors.FindAsync(i => i.Id == id)).Email,
                    Salary = insVM.Salary,
                    ImageUrl = insVM.ImageUrl,
                    Age = insVM.Age,
                    HiringDate = insVM.HiringDate,
                    DeptId = insVM.DeptId,
                };
                if (ins.UserId.IsNullOrEmpty())
                {
                    ins.UserId = user?.Id;
                }
                unitOfWork.Instructors.Update(ins);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            insVM.DeptName = (await unitOfWork.Departments.GetByIdAsync(insVM.DeptId)).Name;
            insVM.Courses = await unitOfWork.Courses.GetAllAsync();
            insVM.Departments = await unitOfWork.Departments.GetAllAsync();
            return View(insVM);
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Delete(int id)
        {
            Instructor ins = await unitOfWork.Instructors.GetByIdAsync(id);
            AppUser user = await unitOfWork.UserManager.FindByIdAsync((string)ins.UserId);
            if (user != null)
                await unitOfWork.UserManager.DeleteAsync(user);
            if (ins != null)
            {
                unitOfWork.Instructors.Delete(ins);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(id);
        }
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DetailsVM(int id)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            var insModel = id == 0 || User.IsInRole("Instructor") ?
                await unitOfWork.Instructors.FindAsync(i => i.UserId == user.Id) : await unitOfWork.Instructors.GetByIdAsync(id);
            if (insModel != null)
            {
                var ins = new InstructorViewModel()
                {
                    Id = id,
                    fName = insModel.fullName.Split(" ")[0],
                    lName = insModel.fullName.Split(" ")[1],
                    Email = insModel.Email,
                    Salary = insModel.Salary,
                    ImageUrl = insModel.ImageUrl,
                    HiringDate = insModel.HiringDate,
                    Age = insModel.Age,
                    DeptId = insModel.DeptId,
                    DeptName = (await unitOfWork.Departments.GetByIdAsync(insModel.DeptId)).Name,
                    Courses = (await unitOfWork.Courses.GetAllAsync()).Where(c => c.InsID == insModel.Id).ToList(),
                    Departments = await unitOfWork.Departments.GetAllAsync()
                };
                return View(ins);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
