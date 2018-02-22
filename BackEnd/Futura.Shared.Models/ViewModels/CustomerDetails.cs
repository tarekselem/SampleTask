using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class CustomerDetails
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTile { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int OrdersCount { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
