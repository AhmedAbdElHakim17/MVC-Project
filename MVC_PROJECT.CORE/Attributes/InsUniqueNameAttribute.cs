using Microsoft.Extensions.DependencyInjection;
using MVC_PROJECT.CORE.Interfaces;
namespace MVC_PROJECT.Models
{
    public class InsUniqueNameAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var validator = validationContext.GetService<IInstructorValidator>();
            InstructorViewModel I = (InstructorViewModel)validationContext.ObjectInstance;
            if(validator.IsUniqueName(I.fName,I.lName,(int)I.DeptId,I.Id))
                return new ValidationResult("Name must be Unique");
            return ValidationResult.Success;
        }
    }
}
