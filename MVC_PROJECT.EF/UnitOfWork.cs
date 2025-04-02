using Microsoft.AspNetCore.Identity;
using MVC_PROJECT.CORE;
using MVC_PROJECT.CORE.Interfaces;
using MVC_PROJECT.EF.Repositories;
using MVC_PROJECT.EF.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_PROJECT.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public IStudentRepository Students { get; private set; }
        public IBaseRepository<Instructor> Instructors { get; private set; }
        public IBaseRepository<Department> Departments { get; private set; }
        public IBaseRepository<Course> Courses { get; private set; }

        public UserManager<AppUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public SignInManager<AppUser> SignInManager { get; }

        public UnitOfWork(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            this.context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            Students = new StudentRepository(context);
            Departments = new BaseRepository<Department>(context);
            Instructors = new BaseRepository<Instructor>(context);
            Courses = new BaseRepository<Course>(context);
        }
        public async Task<int> CompleteAsync() => await context.SaveChangesAsync();
        public void Dispose() { context.Dispose(); }
    }
}
