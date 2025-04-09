using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace MVC_PROJECT.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InstructorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Index()
        {
            var instuctors = await unitOfWork.Instructors.GetAllAsync([nameof(Instructor.Department), nameof(Instructor.Courses)]);
            var InsList = mapper.Map<List<InstructorViewModel>>(instuctors);
            return View("Instructor_Index",InsList);
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
            return View("AddInstructor", insModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(InstructorViewModel insVM)
        {
            if (ModelState.IsValid)
            {
                var ins = mapper.Map<Instructor>(insVM);
                await unitOfWork.Instructors.AddAsync(ins);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            insVM.Departments = await unitOfWork.Departments.GetAllAsync();
            insVM.Courses = await unitOfWork.Courses.GetAllAsync();
            return View("AddInstructor", insVM);
        }
        [HttpGet]
        [Authorize(Roles = "HR,Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var ins = await unitOfWork.Instructors.FindAsync(i => i.Id == id, [nameof(Instructor.Courses), nameof(Instructor.Department)]);
            if (ins == null) return View("Error", new ErrorViewModel { RequestId = "This Instructor doesn't Exist, Please Try Again" });
            var insVM = mapper.Map<InstructorViewModel>(ins);
            insVM.Departments = await unitOfWork.Departments.GetAllAsync();
            return View("EditInstructor", insVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InstructorViewModel insVM, int id)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(insVM.Email);
                if (user == null) return View("Error", new ErrorViewModel { RequestId = "This User doesn't Exist, Please Try Again" });
                var ins = mapper.Map<Instructor>(insVM);
                if (ins.UserId.IsNullOrEmpty())
                {
                    ins.UserId = user?.Id;
                }
                unitOfWork.Instructors.Update(ins);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            insVM.Departments = await unitOfWork.Departments.GetAllAsync();
            return View("EditInstructor", insVM);
        }
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Delete(int id)
        {
            var ins = await unitOfWork.Instructors.FindAsync(i => i.Id == id, nameof(Instructor.Courses));
            if (ins == null) return View("Error", new ErrorViewModel { RequestId = "This Instructor doesn't Exist, Please Try Again" });
            var user = await unitOfWork.UserManager.FindByIdAsync(ins.UserId);
            if (user != null)
                await unitOfWork.UserManager.DeleteAsync(user);
            unitOfWork.Instructors.Delete(ins);
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DetailsVM(int id)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user == null) return View("Error", new ErrorViewModel { RequestId = "This User doesn't Exist, Please Try Again" });
            var ins = id == 0 || User.IsInRole("Instructor") ?
                await unitOfWork.Instructors.FindAsync(i => i.UserId == user.Id, [nameof(Instructor.Department), nameof(Instructor.Courses)]) : 
                await unitOfWork.Instructors.FindAsync(i => i.Id == id, [nameof(Instructor.Department), nameof(Instructor.Courses)]);
            if (ins != null)
            {
                var insVM = mapper.Map<InstructorViewModel>(ins);
                insVM.Departments = await unitOfWork.Departments.GetAllAsync();
                return View("Instructor_DetailsVM", insVM);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
