using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class PaymentViewModel
    {
        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 digits")]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be 3 digits")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        public string ExpiryDate { get; set; }
    }
}
