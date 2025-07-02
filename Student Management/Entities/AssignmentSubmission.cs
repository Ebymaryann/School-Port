using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Management.Entities
{
    public class AssignmentSubmission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AssignmentId { get; set; }

        [ForeignKey("AssignmentId")]
        public Assignment Assignment { get; set; }

        [Required]
        public string StudentId { get; set; } 

        [Required]
        public string Answer { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
