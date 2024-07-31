using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Entities
{
    public class Profile
    {
        [Key]
        public int ProfileID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "First name can only contain letters.")]
        public string  FullName { get; set; } 
  
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Driver Licence No must be numeric.")]
        public string DriverLicenceNo { get; set; } 

        // Navigation Property
        public virtual Customer Customer { get; set; }
    }
}
