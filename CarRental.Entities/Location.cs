using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;


namespace CarRental.Entities
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Location name can only contain letters.")]

        public string LocationName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [RegularExpression(@"^[a-zA-Z0-9\s,]*$", ErrorMessage = "Location address can only contain letters, numbers, spaces, and commas.")]
        public string LocationAddress { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Location city can only contain letters.")]
        public string LocationCity { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Location zip code must be a 5 digit number.")]
        public string LocationZipCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Location country can only contain letters.")]
        public string LocationCountry { get; set; } = string.Empty;

        public virtual ICollection<Reservation> PickupReservations { get; set; } = new List<Reservation>();
        public virtual  ICollection<Reservation> DropoffReservations { get; set; } = new List<Reservation>();
    }

}
