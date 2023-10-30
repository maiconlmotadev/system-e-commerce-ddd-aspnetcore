using Entities.Entities.Enums;
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("TB_SHOPPING")]
    public class Shopping : Notifies
    {
        [Column("SHO_ID")]
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Column("SHO_STATE")]
        [Display(Name = "State")]
        public EnumBuyState State { get; set; }

        [Column("SHO_DATE_SHOPPING")]
        [Display(Name = "Purchase Date")]
        public DateTime? PurchaseDate { get; set; }

        [Display(Name = "User")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string? UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
