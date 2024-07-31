using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModels
{
    public class CarViewModel
    {
        public int CarID { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Car Type")]
        public string CarType { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Transmission")]
        public string CarTransmission { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Fuel Type")]
        public string CarFuel { get; set; }

        [Display(Name = "Mileage")]
        public int CarMileage { get; set; }

        [Display(Name = "Daily Price")]
        public decimal CarDailyPrice { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Licence Plate")]
        public string CarLicencePlate { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}
