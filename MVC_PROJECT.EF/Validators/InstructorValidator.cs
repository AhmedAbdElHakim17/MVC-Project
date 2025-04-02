using MVC_PROJECT.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_PROJECT.EF.Validators
{
    public class InstructorValidator : IInstructorValidator
    {
        private readonly AppDbContext context;

        public InstructorValidator(AppDbContext context)
        {
            this.context = context;
        }
        public bool IsUniqueName(string fname, string lname, int deptId, int id)
        {
            var ins = context.instructors.Any(i =>
                i.fName == fname && i.lName == lname && i.DeptId == deptId && i.Id != id);
            return ins;
        }
    }
}
