using MVC_PROJECT.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_PROJECT.EF.Validators
{
    public class CourseValidator : ICourseValidator
    {
        private readonly AppDbContext context;

        public CourseValidator(AppDbContext context)
        {
            this.context = context;
        }
        public bool IsValid(string name, int Id)
        {
            return !context.courses.Any(c => c.Id != Id && c.Name == name);
        }
    }
}
