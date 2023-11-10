

using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_NIF")]
        [MaxLength(50)]
        [Display(Name="Nif")]
        public string? Nif { get; set; }

        [Column("USR_AGE")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Column("USR_NAME")]
        [MaxLength(255)]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Column("USR_CPOST")]
        [MaxLength(15)]
        [Display(Name = "CPost")]
        public string? CPost { get; set; }

        [Column("USR_ADDRESS")]
        [MaxLength(255)]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Column("USR_ADDRESS_COMPLEMENT")]
        [MaxLength(450)]
        [Display(Name = " Address Complement")]
        public string? AddressComplement { get; set; }

        [Column("USR_CELL_PHONE")]
        [MaxLength(20)]
        [Display(Name = "Cell Phone")]
        public string? CellPhone { get; set; }

        [Column("USR_TELEPHONE")]
        [MaxLength(20)]
        [Display(Name = "Telephone")]
        public string? Telephone { get; set; }

        [Column("USR_STATE")]
        [Display(Name = "State")]
        public bool State { get; set; }

        [Column("USR_TYPE")]
        [Display(Name = "Type")]
        public UserType? Type { get; set; }

    }

}
