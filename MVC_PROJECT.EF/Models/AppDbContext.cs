using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MVC_PROJECT.Models
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseStd>()
                        .HasKey(e => new { e.StdId, e.CrsId });
        }
        public DbSet<Course> courses { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<CourseStd> courseStds { get; set; }
    }
}
