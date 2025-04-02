using MVC_PROJECT.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_PROJECT.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description is Required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Location is Required")]
        public string? Location { get; set; }
        public List<Student>? StdList { get; set; }
        public List<Instructor>? InsList { get; set; }
    }
}
