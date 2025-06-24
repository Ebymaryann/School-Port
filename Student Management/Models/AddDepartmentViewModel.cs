using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class AddDepartmentViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
