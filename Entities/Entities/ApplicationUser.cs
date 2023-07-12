

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
        [Display(Name="NIF")]
        public string NIF { get; set; }

        [Column("USR_AGE")]
        [Display(Name = "AGE")]
        public int Age { get; set; }

        [Column("USR_NAME")]
        [MaxLength(255)]
        [Display(Name = "NAME")]
        public string Name { get; set; }

        [Column("USR_CPOST")]
        [MaxLength(15)]
        [Display(Name = "CPOST")]
        public string CPost { get; set; }

        [Column("USR_ADDRESS")]
        [MaxLength(255)]
        [Display(Name = "ADDRESS")]
        public string Address { get; set; }

        [Column("USR_ADDRESS_COMPLEMENT")]
        [MaxLength(450)]
        [Display(Name = "ADDRESS COMPLEMENT")]
        public string AddressComplement { get; set; }

        [Column("USR_CELL_PHONE")]
        [MaxLength(20)]
        [Display(Name = "CELL PHONE")]
        public string CellPhone { get; set; }

        [Column("USR_TELEPHONE")]
        [MaxLength(20)]
        [Display(Name = "TELEPHONE")]
        public string Telefhone { get; set; }

        [Column("USR_STATE")]
        [Display(Name = "STATE")]
        public bool State { get; set; }

        [Column("USR_TYPE")]
        [Display(Name = "TYPE")]
        public UserType? Type { get; set; }

    }

}
