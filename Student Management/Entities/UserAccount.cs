using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Student_Management.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    //[Index(nameof(Username), IsUnique = true)]
    public class UserAccount
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        public string Email { get; set; }


        

        [Required]
        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }


        [Required]
        [StringLength(50)]
        public string State { get; set; }

        public string LGA { get; set; }


        public string ApplicationNumber { get; set; } 

        public ApplicationStatus AdmissionStatus { get; set; }

        public DateTime DateOfApplication { get; set; } 

        public bool IsDeleted { get; set; }

       
    }

    

}
