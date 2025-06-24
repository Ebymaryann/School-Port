using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Management.Entities
{
    public class Course
    {
        public int CourseId { get; set; } // Primary Key

        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        // Navigation Property
        public Department Department { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
