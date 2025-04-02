using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_PROJECT.CORE.Interfaces
{
    public interface ICourseValidator
    {
        bool IsValid(string name, int Id);
    }
}
