using Microsoft.AspNetCore.Identity;
using MVC_PROJECT.CORE.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MVC_PROJECT.CORE
{
    public interface IUnitOfWork:IDisposable
    {
        IStudentRepository Students { get; }
        IBaseRepository<Instructor> Instructors { get; }
        IBaseRepository<Department> Departments { get; }
        IBaseRepository<Course> Courses { get; }
        UserManager<AppUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        SignInManager<AppUser> SignInManager { get; }
        Task<int> CompleteAsync();
    }
}
