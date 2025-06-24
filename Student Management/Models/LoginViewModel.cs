using Student_Management.Entities;
using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Application number is required.")]
        public string ApplicationNumber { get; set; }

    }
    
}
