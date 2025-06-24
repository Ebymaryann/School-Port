using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management.Entities;
using Student_Management.Models;

namespace Student_Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(AppDbContext appDbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Dashboard()
        {
            var studentCount = _context.UserAccounts.Count(); 
            var courseCount = _context.Courses.Count();
            var deptCount = _context.Departments.Count();
            var lecturerCount = _context.Lecturers.Count();

            ViewBag.StudentCount = studentCount;
            ViewBag.CourseCount = courseCount;
            ViewBag.DeptCount = deptCount;
            ViewBag.LecturerCount = lecturerCount;

            return View();
        }

        public IActionResult Index()
        {
            var students = _context.UserAccounts.ToList();
            return View(students);
            
        }

        [HttpPost]


        public async Task<IActionResult> UpdateStatus(int id, ApplicationStatus status)
        {
            var user = await _context.UserAccounts.FindAsync(id);
            if (user == null) return NotFound();

            user.AdmissionStatus = status;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.UserAccounts.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserAccount updatedStudent)
        {
            if (id != updatedStudent.Id)
            {
                return BadRequest();
            }

            var student = await _context.UserAccounts.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Email = updatedStudent.Email;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                model.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = userRoles
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var model = new List<RoleViewModel>();
            var roles = await _roleManager.Roles.ToListAsync(); // ✅ Materialize the data first

            foreach (var role in roles)
            {
                model.Add(new RoleViewModel
                {
                    RoleName = role.Name,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                });
            }

            ViewBag.UserId = userId;
            ViewBag.Email = user.Email;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoles(List<RoleViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove existing roles.");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(x => x.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add new roles.");
                return View(model);
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View(new AddDepartmentViewModel());
        }
        [HttpPost]
        public IActionResult AddDepartment(AddDepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department { Name = model.Name };
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("AddDepartment"); 
            }

            return View(model);
        }

        public IActionResult AddCourse()
        {
            var model = new AddCourseViewModel
            {
                Departments = _context.Departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.Name
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate the dropdown list
                model.Departments = _context.Departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.Name
                    })
                    .ToList();

                return View(model);
            }

            try
            {
                var course = new Course
                {
                    Code = model.Code,
                    Title = model.Title,
                    DepartmentId = model.DepartmentId
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync(); 

                return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while adding the course. Please try again.");

                // Repopulate dropdown again before returning
                model.Departments = _context.Departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.Name
                    })
                    .ToList();

                return View(model);
            }
        }



        [HttpGet]
        public IActionResult RegisterLecturer()
        {
            var model = new LecturerRegistrationViewModel
            {
                Departments = _context.Departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.Name
                    }).ToList(),

                Courses = _context.Courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.Title
                    }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterLecturer(LecturerRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _context.Departments
                    .Select(d => new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.Name
                    }).ToList();

                model.Courses = _context.Courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.Title
                    }).ToList();

                return View(model);
            }

            // Create Identity user
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Create lecturer profile
                var lecturer = new Lecturer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    DepartmentId = model.DepartmentId,
                    CourseId = model.CourseId,
                    IdentityUserId = user.Id
                };

                _context.Lecturers.Add(lecturer);
                await _context.SaveChangesAsync();

                // Optional: Assign role to the user
                await _userManager.AddToRoleAsync(user, "Lecturer");

                TempData["Success"] = "Lecturer registered successfully!";
                return RedirectToAction("Index"); 
            }

            // Handle errors from Identity creation
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // Re-populate dropdowns before returning to view
            model.Departments = _context.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                }).ToList();

            model.Courses = _context.Courses
                .Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.Title
                }).ToList();

            return View(model);
        }

    }
}
