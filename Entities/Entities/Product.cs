using Entities.Notifications;
using Microsoft.AspNetCore.Http;
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
        [Display(Name = "Name")]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Column("PRD_DESCRIPTION")]
        [Display(Name = "Description")]
        [MaxLength(150)]
        public string? Description { get; set; }

        [Column("PRD_OBSERVATION")]
        [Display(Name = "Observation")]
        [MaxLength(20000)]
        public string? Observation { get; set; }

        [Column("PRO_PRICE")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Column("PRD_STOCK_QUANTITY")]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }

        [Display(Name = "USER")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [Column("PRO_STATE")]
        [Display(Name = "State")]
        public bool State { get; set; }

        [Column("PRD_REGISTRATION_DATE")]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Column("PRD_CHANGE_DATE")]
        [Display(Name = "Change Date")]
        public DateTime ChangeDate { get; set; }

        [NotMapped]
        public int IdCartProduct { get; set; }

        [NotMapped]
        public int BuyQuant { get; set; }


        [NotMapped]
        public IFormFile Image { get; set; }

        [Column("PRD_URL")]
        public string? Url { get; set; } 

    }
}
