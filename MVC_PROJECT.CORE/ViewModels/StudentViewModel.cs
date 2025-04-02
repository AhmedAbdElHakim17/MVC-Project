using System.ComponentModel.DataAnnotations.Schema;
namespace MVC_PROJECT.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Image Url is Required")]
        [RegularExpression(@"\w+\.(jpg|png|svg)", ErrorMessage = "Image Format Must be jpg of png")]
        public string ImgUrl { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        [Range(18, 40, ErrorMessage = "Age Must be between 18 and 40")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        [RegularExpression("(Giza|Menofia|Cairo)", ErrorMessage = "Address Must be Giza or Menofia or Cairo")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        public int? DeptId { get; set; }
        public string? DeptName { get; set; }
        public List<Department>? Departments { get; set; }
        public List<Course>? Courses { get; set; }
        public List<CourseStd>? CourseStds { get; set; }
        [Required(ErrorMessage = "Enter at least one course")]
        public List<int>? SelectedListIds { get; set; } = new List<int>();
        [Required(ErrorMessage ="Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
