using System.ComponentModel.DataAnnotations;

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
    }
}
