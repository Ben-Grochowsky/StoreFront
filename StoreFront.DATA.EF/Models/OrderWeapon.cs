using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class OrderWeapon
    {
        public int OrderWeaponsId { get; set; }
        public int OrderId { get; set; }
        public int WeaponId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }

        public virtual Orders Order { get; set; } = null!;
        public virtual Weapon Weapon { get; set; } = null!;
    }
}
