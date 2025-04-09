using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PROJECT.CORE;
using MVC_PROJECT.Models;
using MVC_PROJECT.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_PROJECT.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public StudentController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Index()
        {

            var studentlist = await unitOfWork.Students.GetAllAsync(nameof(Student.Department));
            var stdList = mapper.Map<List<StudentViewModel>>(studentlist);
            return View("Student_Index",stdList);
        }
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> DetailsVM(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if(user == null) return NotFound(user);
            var studentModel = id == 0 || User.IsInRole("Student") ?
                await unitOfWork.Students.FindAsync(s => s.UserId == user.Id, nameof(Student.Department)) : await unitOfWork.Students.FindAsync(s => s.Id == id, nameof(Student.Department));
            if (studentModel != null)
            {
                var std = mapper.Map<StudentViewModel>(studentModel);
                std.CourseStds = unitOfWork.Students.SelectedIdsForShow(studentModel.Id);
                return View("Student_DetailsVM",std);
            }
            return RedirectToAction("Index");
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
            return View("AddStudent", std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(student.Email);
                if (user == null)
                {
                    return View("Error", new ErrorViewModel { RequestId = "No user found with this email. Please register first." });
                }
                var std = mapper.Map<Student>(student);
                std.CourseStds = unitOfWork.Students.SelectedIdsForCreation(student.SelectedListIds);
                std.UserId = user?.Id;
                await unitOfWork.Students.AddAsync(std);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View("AddStudent", student);
        }
        [HttpGet]
        [Authorize(Roles = "HR,Student,Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound(user);
            var student = id == 0 || User.IsInRole("Student") ?
                await unitOfWork.Students.FindAsync(s => s.UserId == user.Id, nameof(Student.CourseStds)) : await unitOfWork.Students.FindAsync(s => s.Id == id, nameof(Student.CourseStds));
            var std = mapper.Map<StudentViewModel>(student);
            std.SelectedListIds = unitOfWork.Students.GetSelectedIds(student);
            std.Courses = await unitOfWork.Courses.GetAllAsync();
            std.Departments = await unitOfWork.Departments.GetAllAsync();
            return View("EditStudent", std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return View("EditStudent",student);
            }
            var user = await unitOfWork.UserManager.FindByEmailAsync(student.Email);
            if (user == null) return NotFound();
            var std = mapper.Map<Student>(student);
            std.UserId = user.Id;
            std.CourseStds = unitOfWork.Students.SelectedIdsForUpdate(student.SelectedListIds, student.Id);
            unitOfWork.Students.Update(std);
            await unitOfWork.CompleteAsync();
            return (User.IsInRole("Student") ? RedirectToAction("Index", "Home") : RedirectToAction("Index"));
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return NotFound(student);
            var user = await unitOfWork.UserManager.FindByEmailAsync(student.Email);
            if (user == null) return NotFound();
            if (user == null) return NotFound(user);
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