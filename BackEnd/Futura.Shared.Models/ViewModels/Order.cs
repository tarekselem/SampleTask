using System;

namespace ViewModels
{
    public class Order
    {
        public Guid Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
    }
}
