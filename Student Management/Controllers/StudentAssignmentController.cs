using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.Entities;
using Student_Management.Models;

namespace Student_Management.Controllers
{

    [Authorize(Roles = "Student")]
    public class StudentAssignmentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentAssignmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var assignments = _context.Assignments
            .Include(a => a.Course)
            .ToList();
            return View(assignments);
        }

        [HttpGet]
        public IActionResult Submit(int id)
        {
            var assignment = _context.Assignments.Find(id);
            if (assignment == null)
                return NotFound();

            var model = new AssignmentSubmissionViewModel
            {
                AssignmentId = id,
                AssignmentTitle = assignment.Title
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(AssignmentSubmissionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var submission = new AssignmentSubmission
            {
                AssignmentId = model.AssignmentId,
                StudentId = User.Identity.Name, 
                Answer = model.Answer,
                DateSubmitted = DateTime.Now
            };

            _context.AssignmentSubmissions.Add(submission);
            _context.SaveChanges();

            ViewBag.Message = "Assignment submitted successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult MySubmissions()
        {
            var studentId = User.Identity.Name;

            var submissions = _context.AssignmentSubmissions
                .Where(s => s.StudentId == studentId)
                .OrderByDescending(s => s.DateSubmitted)
                .ToList();

            return View(submissions);
        }


    }
}
