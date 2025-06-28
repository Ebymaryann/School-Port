using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Entities;

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
            var assignments = _context.Assignments.ToList();
            return View(assignments);
        }

        //[HttpGet]
        //public IActionResult Submit(int id)
        //{
        //    var assignment = _context.Assignments.Find(id);
        //    if (assignment == null)
        //        return NotFound();

        //    var model = new AssignmentSubmissionViewModel
        //    {
        //        AssignmentId = id,
        //        AssignmentTitle = assignment.Title
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Submit(AssignmentSubmissionViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var submission = new AssignmentSubmission
        //    {
        //        AssignmentId = model.AssignmentId,
        //        StudentId = User.Identity.Name, // Assuming student's username is used
        //        Answer = model.Answer,
        //        DateSubmitted = DateTime.Now
        //    };

        //    _context.AssignmentSubmissions.Add(submission);
        //    _context.SaveChanges();

        //    ViewBag.Message = "Assignment submitted successfully!";
        //    return RedirectToAction("Index");
        //}

    }
}
