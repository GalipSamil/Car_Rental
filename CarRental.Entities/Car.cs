using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarRental.Entities;
using System.ComponentModel.DataAnnotations;


namespace CarRental.Entities
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }

        
        public int BrandID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Type of car can only contain letters.")]
        public string CarType { get; set; }

        [Required]
        [StringLength(50)]
        public string CarModel { get; set; }

        [Required]
        [StringLength(50)]
        public string CarTransmission { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Fuel type car can only contain letters.")]
        public string CarFuel { get; set; }
        public int CarMileage { get; set; }
        public decimal CarDailyPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string CarLicencePlate { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Brand name can only contain letters.")]
        public string BrandName { get; set; }



        // Navigation Property
        public virtual Brand Brand { get; set; } 
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        
    }
}
 