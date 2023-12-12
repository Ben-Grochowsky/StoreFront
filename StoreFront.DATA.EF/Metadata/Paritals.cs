using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models
{/*.Metadata*/

    [ModelMetadataType(typeof(WeaponMetadata))]
    public partial class Weapon 
    {
        [NotMapped]
        public IFormFile? Image { get; set; }
    }

    [ModelMetadataType(typeof(UniverseMetadata))]
    public partial class Universe { }

    [ModelMetadataType(typeof(OrderWeaponMetadata))]
    public partial class OrderWeapon { }

    [ModelMetadataType(typeof(OrdersMetadata))]
    public partial class Orders { }

    [ModelMetadataType(typeof(ManufacturerMetadata))]
    public partial class Manufacturer { }

    [ModelMetadataType(typeof(CustomerDetailMetadata))]
    public partial class CustomerDetail 
    {
        [Display(Name = "Full Name")]
        public string FullName => FirstName + " " + LastName;
    }
}