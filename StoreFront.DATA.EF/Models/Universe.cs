using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Universe
    {
        public Universe()
        {
            Weapons = new HashSet<Weapon>();
        }

        public int UniverseId { get; set; }
        public string UniverseName { get; set; } = null!;

        public virtual ICollection<Weapon> Weapons { get; set; }
    }
}
