using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management.Entities
{
    public class Assignment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        public int CourseId { get; set; }  // Foreign Key to Course

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }  // Mark as virtual for EF lazy loading / proxy support

        public virtual ICollection<AssignmentSubmission> Submissions { get; set; } = new List<AssignmentSubmission>(); // Initialize to avoid null issues
    }
}
