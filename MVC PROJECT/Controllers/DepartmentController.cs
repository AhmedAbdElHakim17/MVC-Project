using AutoMapper;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace MVC_PROJECT.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var depts = await unitOfWork.Departments.GetAllAsync();
            if (depts.Count == 0) return NotFound();
            var deptVMs = mapper.Map<List<DepartmentViewModel>>(depts);
            //var deptVMs = depts.Select(d => new DepartmentViewModel
            //{

            //    Id = d.Id,
            //    Name = d.Name,
            //    Description = d.Description,
            //    Location = d.Location,
            //});
            return View("Department_Index", deptVMs);
        }
        public async Task<IActionResult> DetailsVM(int id)
        {
            var dept = await unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return NotFound();
            var deptModel = mapper.Map<DepartmentViewModel>(dept);
            //var deptModel = new DepartmentViewModel
            //{
            //    Id = id,
            //    Name = dept.Name,
            //    Description = dept.Description,
            //    Location = dept.Location,
            //};
            return View("Department_DetailsVM",deptModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View("AddDepartment");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DepartmentViewModel dept)
        {
            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Id = dept.Id,
                //    Name = dept.Name,
                //    Description = dept.Description,
                //    Location = dept.Location,
                //};
                var department = mapper.Map<Department>(dept);
                await unitOfWork.Departments.AddAsync(department);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View("AddDepartment");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return NotFound();
            var department = mapper.Map<DepartmentViewModel>(dept);
            //var department = new DepartmentViewModel()
            //{
            //    Id = dept.Id,
            //    Name = dept.Name,
            //    Description = dept.Description,
            //    Location = dept.Location,
            //};
            return View("EditDepartment",department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel dept)
        {
            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Id = dept.Id,
                //    Name = dept.Name,
                //    Description = dept.Description,
                //    Location = dept.Location,
                //};
                var department = mapper.Map<Department>(dept);
                unitOfWork.Departments.Update(department);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View("EditDepartment",dept);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return NotFound();
            if (dept.InsList?.Count > 0 || dept.StdList?.Count > 0) return View(id); 
            unitOfWork.Departments.Delete(dept);
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
            //return Content("The Department must be Empty of Students or Instructors to get deleted");
        }
    }
}
