using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace StoreFront.DATA.EF.Models/*.Metadata*/
{
    public class WeaponMetadata
    {
        /*[Key]
        public int WeaponID { get; set; }*/

        [Required]
        [Display(Name = "Weapon")]
        [StringLength(50)]
        public string WeaponName { get; set; }

        [DisplayFormat(NullDisplayText ="No description given")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Display(Name = "Manufacturer")]
        public int ManufacturerID { get; set; }

        [Required]
        [Display(Name = "Universe")]
        public int UniverseID { get; set; }

        [Required]
        [Range(0, short.MaxValue)]
        [Display(Name = "In Stock")]
        public int InStock { get; set; }

        [Display(Name = "Discontinued")]
        public int Discontinued { get; set; }

        [Display(Name = "Image")]
        [StringLength(75)]
        public string? WeaponImage { get; set; }
    }

    public class UniverseMetadata
    {
        [Key]
        public int UniverseID { get; set; }

        [Required(ErrorMessage = "** Required **")]
        [Display(Name = "Universe")]
        [Unicode(false)]
        [StringLength(50)]
        public string UniverseName { get; set; }
    }

    public class OrderWeaponMetadata
    {
        [Key]
        public int OrderWeaponID { get; set; }

        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        [Required]
        [Display(Name = "Weapon ID")]
        public int WeaponID { get; set; }

        [Display(Name = "Amount")]
        [Range(0, short.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        public decimal TotalCost { get; set; }
    }

    public class OrdersMetadata
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "User")]
        public string UserId { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string ShipCity { get; set; } = null!;

        [StringLength(2, MinimumLength = 2)]
        [Display(Name = "State")]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? ShipState { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Zip")]
        [DataType(DataType.PostalCode)]
        public string? ShipZip { get; set; }
    }

    public class ManufacturerMetadata
    {
        [Key]
        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [Display(Name = "Company")]
        [StringLength(50)]
        public string CompanyName { get; set; } = null!;

        [Required]
        [Display(Name = "Address")]
        [StringLength(200)]
        public string Address1 { get; set; } = null!;

        [Required]
        [Display(Name = "City")]
        [StringLength(50)]
        public string? City { get; set; }

        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? State { get; set; }

        [Display(Name = "Zip")]
        [StringLength(5, MinimumLength = 5)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public int? Zip { get; set; }

        [Display(Name = "Country")]
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Country { get; set; }

        [Display(Name = "Sector")]
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Sector { get; set; }

        [Display(Name = "Territory")]
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Territory { get; set; }

        [Display(Name = "Planet")]
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Planet { get; set; }

        [Display(Name = "Coordinates")]
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Coordinates { get; set; }

        [Display(Name = "Address Name")]
        [StringLength(100)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? AddressName { get; set; }

        [Display(Name = "Temple")]
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Temples { get; set; }
    }

    public class CustomerDetailMetadata
    {
        [Key]
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [Display(Name = "Address")]
        [StringLength(50)]
        public string Address { get; set; } = null!;

        [Display(Name = "City")]
        [StringLength(50)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string City { get; set; } = null!;

        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? State { get; set; }

        [Display(Name = "Zip")]
        [StringLength(5, MinimumLength = 5)]
        [DisplayFormat(NullDisplayText = "[N/A]")]
        public string? Zip { get; set; }
    }
}
