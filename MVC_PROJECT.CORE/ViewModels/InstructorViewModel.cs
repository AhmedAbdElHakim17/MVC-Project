using System.ComponentModel.DataAnnotations.Schema;
namespace MVC_PROJECT.ViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string ?fName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [InsUniqueName]
        public string ?lName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Salary is Required")]
        [Range(5000, 100000, ErrorMessage = "Salary Must be between 5000 and 100000")]
        public double? Salary { get; set; }

        [Required(ErrorMessage = "Image Url is Required")]
        [RegularExpression(@"\w+\.(jpg|png|svg)", ErrorMessage = "Image Format Must be Jpg of Png")]
        public string ?ImageUrl { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [Range(25, 65, ErrorMessage = "Age Must be between 25 and 65")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Hiring Date is Required")]
        [DataType(DataType.Date)]
        public DateTime? HiringDate { get; set; }

        [Required(ErrorMessage = "Department Name is Required")]
        public int? DeptId { get; set; }
        public string? DeptName { get; set; }
        public List<Course>? Courses { get; set; }
        public List<Course>? CourseList { get; set; }
        public List<Department>? Departments { get; set; }
        public string fullName => $"{fName} {lName}";

    }
}
