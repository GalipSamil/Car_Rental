using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services
{
    public  class CustomerDto
    {
        public int CustomerID { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }
    }
}
