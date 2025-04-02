using System.ComponentModel.DataAnnotations.Schema;
namespace MVC_PROJECT.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Topic { get; set; }
        public int? TotalDeg { get; set; }
        public int? minDeg { get; set; }
        [ForeignKey("Instructor")]
        public int? InsID { get; set; }
        public Instructor? Instructor { get; set; }
        public List<CourseStd>? CourseStds { get; set; }
    }
}
