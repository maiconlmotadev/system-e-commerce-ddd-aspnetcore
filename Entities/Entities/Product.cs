using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("TB_PRODUCT")]
    public class Product : Notifies
    {
        [Column("PRO_ID")]
        [Display(Name ="Code")]
        public int Id { get; set; }

        [Column("PRO_NAME")]
        [Display(Name = "NAME")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("PRD_DESCRIPTION")]
        [Display(Name = "DESCRIPTION")]
        [MaxLength(150)]
        public string Description { get; set; }

        [Column("PRD_OBSERVATION")]
        [Display(Name = "OBSERVATION")]
        [MaxLength(20000)]
        public string Observation { get; set; }

        [Column("PRO_PRICE")]
        [Display(Name = "PRICE")]
        public decimal Price { get; set; }

        [Column("PRD_STOCK_QUANTITY")]
        [Display(Name = "STOCK QUANTITY")]
        public int StockQuantity { get; set; }

        [Display(Name = "USER")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Column("PRO_STATE")]
        [Display(Name = "STATE")]
        public bool State { get; set; }

        [Column("PRD_REGISTRATION_DATE")]
        [Display(Name = "REGISTRATION DATE")]
        public DateTime RegistrationDate { get; set; }

        [Column("PRD_CHANGE_DATE")]
        [Display(Name = "CHANGE DATE")]
        public DateTime ChangeDate { get; set; }

    }
}
