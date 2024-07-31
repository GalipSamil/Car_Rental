/*using CarRental.Entities;

namespace CarRental.Helpers
{
    public static class Mapper
    {
        public static ProfileViewModel ToProfileViewModel(this Customer customer)
        {
            return new ProfileViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                DriverLicenceNo = customer.Profil?.DriverLicenceNo // Profil bilgisi varsa ekleyin
            };
        }

        public static Customer UpdateCustomerFromViewModel(this Customer customer, ProfileViewModel model)
        {
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.Phone = model.Phone;

            if (customer.Profil == null)
            {
                customer.Profil = new Profil
                {
                    CustomerID = customer.CustomerID
                };
            }

            customer.Profil.Email = model.Email; // Profil email güncellemesi
            customer.Profil.FullName = $"{model.FirstName} {model.LastName}";
            customer.Profil.DriverLicenceNo = model.DriverLicenceNo;

            return customer;
        }
    }
}*/
