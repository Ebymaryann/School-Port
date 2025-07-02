using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.Entities;

namespace Student_Management.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class AssignmentController : Controller
    {
        private readonly AppDbContext _context;

        public AssignmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var assignments = _context.Assignments
                .Include(a => a.Course)
                .OrderByDescending(a => a.DateCreated)
                .ToList();

            return View(assignments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assignment model)
        {
            ModelState.Remove("Course");
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _context.Courses.ToList();
                return View(model);
            }

            // Defensive check for Course existence (optional but good practice)
            var courseExists = _context.Courses.Any(c => c.CourseId == model.CourseId);
            if (!courseExists)
            {
                ModelState.AddModelError("CourseId", "Selected course does not exist.");
                ViewBag.Courses = _context.Courses.ToList();
                return View(model);
            }

            model.DateCreated = DateTime.Now;
            _context.Assignments.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var assignment = _context.Assignments.Find(id);
            if (assignment == null)
                return NotFound();

            ViewBag.Courses = _context.Courses.ToList();
            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Assignment model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _context.Courses.ToList();
                return View(model);
            }

            _context.Assignments.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var assignment = _context.Assignments.Find(id);
            if (assignment == null)
                return NotFound();

            _context.Assignments.Remove(assignment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Submissions()
        {
            var submissions = _context.AssignmentSubmissions
                .Include(s => s.Assignment)
                .ThenInclude(a => a.Course)
                .OrderByDescending(s => s.DateSubmitted)
                .ToList();

            return View(submissions);
        }
    }

}
