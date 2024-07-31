namespace CarRental.ViewModels
{
    public class ReservationViewModel
    {
        public int CustomerID { get; set; }
        public int CarID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PickupLocationID { get; set; }
        public int DropoffLocationID { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
