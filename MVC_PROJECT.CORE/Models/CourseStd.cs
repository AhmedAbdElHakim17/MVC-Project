using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_PROJECT.Models
{
    public class CourseStd
    {
        [ForeignKey("Student")]
        public int StdId { get; set; }
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public double Degree { get; set; }
    }
}
