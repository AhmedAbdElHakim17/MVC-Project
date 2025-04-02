using Microsoft.Extensions.DependencyInjection;
using MVC_PROJECT.CORE.Interfaces;
namespace MVC_PROJECT.EF.Attributes
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string val = value.ToString();
            var validator = validationContext.GetService<ICourseValidator>();
            var course = (CourseViewModel)validationContext.ObjectInstance;
            if(!validator.IsValid(course.Name, course.Id))
            {
                return new ValidationResult("Name Must Be Unique");
            }
            return ValidationResult.Success;
        }
    }
}
