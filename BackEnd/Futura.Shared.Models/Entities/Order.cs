using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Order : BaseEntity
    {
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freight { get; set; }
        [Required]
        public string ShipName { get; set; }
        [Required]
        public string ShipAddress { get; set; }
        [Required]
        public string ShipCity { get; set; }
        [Required]
        public string ShipRegion { get; set; }
        [Required]
        public string ShipPostalCode { get; set; }
        [Required]
        public string ShipCountry { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}
