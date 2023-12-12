using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class CustomerDetail
    {
        public CustomerDetail()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? State { get; set; }
        public string? Zip { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
