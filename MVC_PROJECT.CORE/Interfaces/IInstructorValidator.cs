using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_PROJECT.CORE.Interfaces
{
    public interface IInstructorValidator
    {
        bool IsUniqueName(string fName, string lName, int DeptId, int Id);
    }
}
