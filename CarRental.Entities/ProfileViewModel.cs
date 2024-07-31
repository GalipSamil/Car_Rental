using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities
{
    public  class ProfileViewModel
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName {  get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone can only contain numbers.")]
        public string Phone { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Driver Licence No must be numeric.")]
        public string DriverLicenceNo { get; set; }
    }
}
