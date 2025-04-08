using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_PROJECT.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ImgUrl { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        [ForeignKey("Department")]
        public int? DeptId { get; set; }
        public Department? Department { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<CourseStd>? CourseStds { get; set; }
    }
}
