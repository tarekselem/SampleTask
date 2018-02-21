using System.Collections.Generic;

namespace ViewModels
{
    public class OrdersList
    {
        public int Total { get; set; }
        public ICollection<Order> Items { get; set; }
    }
}
