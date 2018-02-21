using System.Collections.Generic;


namespace ViewModels
{
    public class CustomersList
    {
        public int Total { get; set; }
        public ICollection<Customer> Items { get; set; }
    }
}
