namespace MVC_PROJECT.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Index()
        {
            var courses = await unitOfWork.Courses.GetAllAsync("Instructor");
            if(courses.Count == 0) return NotFound();
            var crsList = new List<CourseViewModel>();
            foreach (var item in courses)
            {
                crsList.Add(new CourseViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Topic = item.Topic,
                    TotalDeg = item.TotalDeg,
                    minDeg = item.minDeg,
                    InsName = item.Instructor.fName + " " + item.Instructor.lName
                });
            }
            return View(crsList);
        }
        public IActionResult SetSession()
        {
            //var course = AppDbContext.courses.FirstOrDefault();
            //HttpContext.Session.SetString("CourseName", course.Name);
            //HttpContext.Session.SetString("CourseTopic", course.Topic);
            //HttpContext.Session.SetInt32("Min Degree", course.minDeg);
            return Content("Data Session saved successfully");
        }
        public IActionResult GetSession()
        {
            string? cName = HttpContext.Session.GetString("CourseName");
            string? cTopic = HttpContext.Session.GetString("CourseTopic");
            int? min = HttpContext.Session.GetInt32("Min Degree");
            return Content($"{cName} / {cTopic} / {min}");
        }
        public async Task<IActionResult> DetailsVM(int id)
        {
            Course course = await unitOfWork.Courses.GetByIdAsync(id);
            if(course == null || course.InsID == null) return NotFound();
            var Instructor = await unitOfWork.Instructors.GetByIdAsync(course.InsID);
            if(Instructor == null) return NotFound();
            CourseViewModel Crsmodel = new CourseViewModel()
            {
                Id = id,
                Name = course.Name,
                Topic = course.Topic,
                TotalDeg = course.TotalDeg,
                minDeg = course.minDeg,
                InsName = Instructor.fullName,
                insList = await unitOfWork.Instructors.GetAllAsync()
            };
            return View(Crsmodel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var crs = new CourseViewModel()
            {
                insList = await unitOfWork.Instructors.GetAllAsync()
            };
            return View(crs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                var crs = new Course()
                {
                    Id = course.Id,
                    Name = course.Name,
                    Topic = course.Topic,
                    TotalDeg = course.TotalDeg,
                    minDeg = course.minDeg,
                    InsID = course.InsID,
                };
                await unitOfWork.Courses.AddAsync(crs);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            course.insList = await unitOfWork.Instructors.GetAllAsync();
            return View(course);
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Course course = await unitOfWork.Courses.GetByIdAsync(id);
            if(course == null) return NotFound();
            var crsVM = new CourseViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Topic = course.Topic,
                TotalDeg = course.TotalDeg,
                minDeg = course.minDeg,
                InsID = course.InsID,
                insList = await unitOfWork.Instructors.GetAllAsync()
            };
            return View(crsVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                var crsVM = new CourseViewModel()
                {
                    Id = course.Id,
                    Name = course.Name,
                    Topic = course.Topic,
                    TotalDeg = course.TotalDeg,
                    minDeg = course.minDeg,
                    InsID = course.InsID,
                };
                unitOfWork.Courses.Update(course);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            ViewBag.InsList = await unitOfWork.Instructors.GetAllAsync();
            return View(course);
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Course crs = await unitOfWork.Courses.GetByIdAsync(id);
            if (crs != null)
            {
                unitOfWork.Courses.Delete(crs);
                await unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(id);
        }
        public IActionResult CheckValid(int minDeg, int TotalDeg)
        {
            if (minDeg < TotalDeg)
                return Json(true);
            return Json(false);
        }

    }
}
