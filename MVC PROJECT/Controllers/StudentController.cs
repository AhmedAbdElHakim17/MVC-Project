using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.CORE;
using MVC_PROJECT.Models;
using MVC_PROJECT.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_PROJECT.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public StudentController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Index()
        {
            var studentlist = await unitOfWork.Students.GetAllAsync();
            var stdList = new List<StudentViewModel>();
            foreach (var item in studentlist)
            {
                stdList.Add(new StudentViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    ImgUrl = item.ImgUrl,
                    Address = item.Address,
                    Age = item.Age,
                    DeptId = item.DeptId,
                    DeptName = (await unitOfWork.Departments.GetByIdAsync(item.DeptId)).Name,
                    Departments = await unitOfWork.Departments.GetAllAsync()
                });
            }
            return View(stdList);
        }
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> DetailsVM(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var studentModel = id == 0 || User.IsInRole("Student") ?
                await unitOfWork.Students.FindAsync(s => s.UserId == user.Id) : await unitOfWork.Students.GetByIdAsync(id);
            if (studentModel != null)
            {
                var std = new StudentViewModel()
                {
                    Id = studentModel.Id,
                    Name = studentModel.Name,
                    Email = studentModel.Email,
                    ImgUrl = studentModel.ImgUrl,
                    Address = studentModel.Address,
                    Age = studentModel.Age,
                    DeptId = studentModel.DeptId,
                    DeptName = (await unitOfWork.Departments.GetByIdAsync(studentModel.DeptId)).Name,
                    CourseStds = unitOfWork.Students.SelectedIdsForShow(studentModel.Id),
                };
                return View(std);
            }
            return RedirectToAction("Home");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var std = new StudentViewModel()
            {
                Courses = await unitOfWork.Courses.GetAllAsync(),
                Departments = await unitOfWork.Departments.GetAllAsync(),
            };
            return View(std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(student.Email);
                var std = new Student()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    ImgUrl = student.ImgUrl,
                    Address = student.Address,
                    Age = student.Age,
                    DeptId = student.DeptId,
                    CourseStds = unitOfWork.Students.SelectedIdsForCreation(student.SelectedListIds),
                    UserId = user?.Id
                };
                await unitOfWork.Students.AddAsync(std);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        [HttpGet]
        [Authorize(Roles = "HR,Student,Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var student = id == 0 || User.IsInRole("Student") ?
                await unitOfWork.Students.FindAsync(s => s.UserId == user.Id,nameof(Student.CourseStds)) : await unitOfWork.Students.FindAsync(s => s.Id == id, nameof(Student.CourseStds));
            StudentViewModel std = new StudentViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ImgUrl = student.ImgUrl,
                Address = student.Address,
                Age = student.Age,
                DeptId = student.DeptId,
                DeptName = (await unitOfWork.Departments.GetByIdAsync(student.DeptId)).Name,
                SelectedListIds = student.CourseStds.Select(cs => cs.CrsId).ToList(),
                Courses = await unitOfWork.Courses.GetAllAsync(),
                Departments = await unitOfWork.Departments.GetAllAsync(),
            };
            return View(std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            var user = await unitOfWork.UserManager.FindByEmailAsync(student.Email);
            Student std = new Student()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ImgUrl = student.ImgUrl,
                Address = student.Address,
                Age = student.Age,
                DeptId = student.DeptId,
                UserId = user.Id,
                Department = await unitOfWork.Departments.GetByIdAsync(student.DeptId),
                CourseStds = unitOfWork.Students.SelectedIdsForUpdate(student.SelectedListIds, student.Id)
            };
            unitOfWork.Students.Update(std);
            await unitOfWork.CompleteAsync();
            return (User.IsInRole("Student") ? RedirectToAction("Index", "Home") : RedirectToAction("Index"));
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Delete(int id)
        {
            Student student = await unitOfWork.Students.GetByIdAsync(id);
            AppUser user = await unitOfWork.UserManager.FindByEmailAsync(student.Email);
            if (user != null)
                await unitOfWork.UserManager.DeleteAsync(user);
            if (student != null)
            unitOfWork.Students.Delete(student);
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
//public IActionResult GetStudentsByDeptId(int deptId)
//{
//    List<Student> StdList = studentRepository.GetByDeptId(deptId);
//    return Json(StdList);
//}

//public IActionResult CourseInStudent()
//{
//    string? cName = HttpContext.Session.GetString("CourseName");
//    string? cTopic = HttpContext.Session.GetString("CourseTopic");
//    int? min = HttpContext.Session.GetInt32("Min Degree");
//    return Content($"{cName} \n {cTopic} \n {min}");
//}