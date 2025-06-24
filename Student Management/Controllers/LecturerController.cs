using Microsoft.AspNetCore.Mvc;

namespace Student_Management.Controllers
{
    public class LecturerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
