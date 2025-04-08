using MVC_PROJECT.EF.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_PROJECT.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string? fName { get; set; }
        public string? lName { get; set; }

        public string? Email { get; set; }
        public double? Salary { get; set; }
        public string? ImageUrl { get; set; }
        public int? Age { get; set; }
        public DateTime? HiringDate { get; set; }
        [ForeignKey("Department")]
        public int? DeptId { get; set; }
        public Department? Department { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<Course>? Courses { get; set; }
        public string fullName => $"{fName} {lName}";

    }
}
