using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class AddCourseViewModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [BindNever]
        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
    }

}
