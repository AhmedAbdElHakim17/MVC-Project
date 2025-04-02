namespace MVC_PROJECT.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var depts = await unitOfWork.Departments.GetAllAsync();
            if(depts.Count == 0) return NotFound();
            var deptModel = new List<DepartmentViewModel>();
            foreach (var item in depts)
            {
                deptModel.Add(new DepartmentViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Location = item.Location,
                });
            }
            return View(deptModel);
        }
        public async Task<IActionResult> DetailsVM(int id)
        {
            var dept = await unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return NotFound();
            var deptModel = new DepartmentViewModel
            {
                Id = id,
                Name = dept.Name,
                Description = dept.Description,
                Location = dept.Location,
            };
            return View(deptModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DepartmentViewModel dept)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Description = dept.Description,
                    Location = dept.Location,
                };
                await unitOfWork.Departments.AddAsync(department);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dept = await unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return NotFound();
            var department = new DepartmentViewModel()
            {
                Id = dept.Id,
                Name = dept.Name,
                Description = dept.Description,
                Location = dept.Location,
            };
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel dept)
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Description = dept.Description,
                    Location = dept.Location,
                };
                unitOfWork.Departments.Update(department);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(dept);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Department dept = await unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return NotFound();
            if (dept != null)
            {
                unitOfWork.Departments.Delete(dept);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(id);
        }
    }
}
