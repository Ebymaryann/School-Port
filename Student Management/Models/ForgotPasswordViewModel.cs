using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
