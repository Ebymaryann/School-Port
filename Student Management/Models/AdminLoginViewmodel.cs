using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class AdminLoginViewmodel
    {
        [Required(ErrorMessage = "Input your Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Input your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
