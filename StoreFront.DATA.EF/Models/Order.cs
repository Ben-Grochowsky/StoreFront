using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderWeapons = new HashSet<OrderWeapon>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string ShipCity { get; set; } = null!;
        public string? ShipState { get; set; }
        public string? ShipZip { get; set; }

        public virtual CustomerDetail User { get; set; } = null!;
        public virtual ICollection<OrderWeapon> OrderWeapons { get; set; }
    }
}
