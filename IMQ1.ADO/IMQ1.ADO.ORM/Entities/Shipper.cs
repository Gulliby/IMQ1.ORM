using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMQ1.ADO.ORM.Entities
{
    public class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public int ShipperID { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
