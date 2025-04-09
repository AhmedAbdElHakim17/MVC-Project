using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MVC_PROJECT.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var Users = await unitOfWork.UserManager.Users.ToListAsync();
            var Roles = await unitOfWork.RoleManager.Roles.ToListAsync();
            var adminVM = new AdminViewModel
            {
                Roles = Roles,
                Users = Users
            };
            return View("Admin_Index", adminVM);

        }
    }
}
