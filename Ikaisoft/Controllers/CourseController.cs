using Microsoft.AspNetCore.Mvc;

namespace Ikaisoft.Controllers
{
    public class CourseController : Controller
    {
       
        public IActionResult HTML()
        {
            return View();
        }
        public IActionResult Lesson(int id)
        {
            if (id < 1) id = 1; 
            return PartialView($"~/Views/HTML/_lesson{id}.cshtml");
        }
        public IActionResult CSS()
        {
            return View();
        }
        public IActionResult CssLesson(int id)
        {
            if (id < 1) id = 1;
            return PartialView($"~/Views/HTML/_lesson{id}.cshtml");
        }
    }
}
