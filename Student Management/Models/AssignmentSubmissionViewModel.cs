using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class AssignmentSubmissionViewModel
    {
        public int AssignmentId { get; set; }

        public string AssignmentTitle { get; set; } // For displaying assignment title in the form

        [Required(ErrorMessage = "Please provide your answer.")]
        public string Answer { get; set; }
    }
}
