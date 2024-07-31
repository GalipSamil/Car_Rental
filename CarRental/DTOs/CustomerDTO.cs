using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone can only contain numbers.")]
        public string Phone { get; set; }

        [StringLength(200)]
        [RegularExpression(@"^[a-zA-Z0-9\s,]*$", ErrorMessage = "Address can only contain letters, numbers, spaces, and commas.")]
        public string Address { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Z]).+$", ErrorMessage = "The password must contain at least one uppercase letter.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
