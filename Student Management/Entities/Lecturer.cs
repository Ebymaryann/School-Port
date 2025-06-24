using Microsoft.AspNetCore.Identity;

namespace Student_Management.Entities
{
    public class Lecturer
    {
        public int LecturerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        // Link to Identity user
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
