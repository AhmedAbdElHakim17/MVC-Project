using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_PROJECT.Models;

namespace MVC_PROJECT.Controllers
{
    public class StateManagementController : Controller
    {

        public StateManagementController()
        {
            //    AppDbContext = new AppDbContext();
            //    var course = AppDbContext.courses.FirstOrDefault();
            //    TempData["CourseName"] = course.Name;
            //    TempData["CourseTopic"] = course.Topic;
            //    TempData["MinDegree"] = course.minDeg;
        }
        //public IActionResult SetSession()
        //{
            //HttpContext.Session.SetString("CourseName","das");
            //HttpContext.Session.SetString("CourseTopic", "fds");
            //HttpContext.Session.SetInt32("Min Degree", 21);
            //return Content("Data Session saved successfully");
        //}
        //public IActionResult GetSession()
        //{
            //string cName = HttpContext.Session.GetString("CourseName");
            //string cTopic = HttpContext.Session.GetString("CourseTopic");
            //int? min = HttpContext.Session.GetInt32("Min Degree");
            //return Content($"{cName} / {cTopic} / {min}");
        //}
        public IActionResult Hello()
        {
            return Content("Hello");
        }
    }
}
