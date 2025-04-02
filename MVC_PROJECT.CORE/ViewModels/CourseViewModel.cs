using Microsoft.AspNetCore.Mvc;
using MVC_PROJECT.EF.Attributes;
namespace MVC_PROJECT.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [UniqueName]
        public string Name { get; set; }
        [Required(ErrorMessage = "Topic is Required")]
        public string? Topic { get; set; }
        [Required(ErrorMessage = "Total Degree is Required")]
        [Remote(action: "CheckValid", controller: "Course", AdditionalFields = "minDeg", ErrorMessage = "Total Degree Must Be greater than Minimun Degree")]
        public int? TotalDeg { get; set; }
        [Required(ErrorMessage = "Minimum Degree is Required")]
        public int? minDeg { get; set; }
        [Required(ErrorMessage = "Instructor Name is Required")]
        public int? InsID { get; set; }
        public string? InsName { get; set; }
        public List<Instructor>? insList { get; set; }
    }
}
