namespace CarRental.DTOs
{
    public class CarDTO
    {
        public int CarID { get; set; }
        public string BrandName { get; set; }
        public string CarType { get; set; }
        public string CarModel { get; set; }
        public string CarTransmission { get; set; }
        public string CarFuel { get; set; }
        public int CarMileage { get; set; }
        public decimal CarDailyPrice { get; set; }
        public string CarLicencePlate { get; set; }
    }
}
