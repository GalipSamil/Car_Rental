using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities
{

    public class Brand
    {
       
        public int BrandID { get; set; }
        public string BrandName { get; set; } = string.Empty;
        
        // relation with Car
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }

    
    
}
