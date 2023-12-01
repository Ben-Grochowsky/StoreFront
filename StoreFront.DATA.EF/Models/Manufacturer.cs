using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Weapons = new HashSet<Weapon>();
        }

        public int ManufacturerId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Address1 { get; set; } = null!;
        public string? City { get; set; }
        public string? State { get; set; }
        public int? Zip { get; set; }
        public string? Country { get; set; }
        public string? Sector { get; set; }
        public string? Territory { get; set; }
        public string? Planet { get; set; }
        public string? Coordinates { get; set; }
        public string? AddressName { get; set; }
        public string? Temples { get; set; }

        public virtual ICollection<Weapon> Weapons { get; set; }
    }
}
