using Microsoft.AspNetCore.Mvc.Rendering;
using Student_Management.Entities;
using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        public string Email { get; set; }


        [Required]
        [Phone]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } 


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        public string LGA { get; set; }

        public List<SelectListItem> LGAList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> StateList { get; set; } = new List<SelectListItem>();




    }
}
