using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var assignments = _context.Assignments.ToList();
            return View(assignments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assignment model)
        {
            if (!ModelState.IsValid)
                return View(model);

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

            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Assignment model)
        {
            if (!ModelState.IsValid)
                return View(model);

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
    }
}
