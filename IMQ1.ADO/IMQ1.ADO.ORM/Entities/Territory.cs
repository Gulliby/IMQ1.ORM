using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMQ1.ADO.ORM.Entities
{
    public class Territory
    {
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        [StringLength(20)]
        public string TerritoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        public int RegionID { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
