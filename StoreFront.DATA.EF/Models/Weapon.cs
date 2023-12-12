using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Weapon
    {
        public Weapon()
        {
            OrderWeapons = new HashSet<OrderWeapon>();
        }

        public int WeaponId { get; set; }
        public string WeaponName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int ManufacturerId { get; set; }
        public int UniverseId { get; set; }
        public int InStock { get; set; }
        public bool Discontinued { get; set; }
        public string? WeaponImage { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual Universe? Universe { get; set; }
        public virtual ICollection<OrderWeapon> OrderWeapons { get; set; }
    }
}
