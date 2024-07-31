using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRental.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [Required(ErrorMessage = "CustomerID is required")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "CarID is required")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "PickupLocationID is required")]
        public int PickupLocationID { get; set; }

        [Required(ErrorMessage = "DropoffLocationID is required")]
        public int DropoffLocationID { get; set; }
        public string Status { get; set; } = string.Empty; 

        // Navigation Properties
        public virtual Customer Customer { get; set; } = new Customer(); 
        public virtual Car Car { get; set; } = new Car(); 
        public virtual Location PickupLocation { get; set; } = new Location();
        public virtual Location DropoffLocation { get; set; } = new Location(); 
    }

}

