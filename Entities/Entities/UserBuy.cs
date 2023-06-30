﻿using Entities.Entities.Enums;
using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Entities
{
    [Table("TB_USER_BUY")]
    public class UserBuy : Notifies
    {
        [Column("CUS_ID")]
        [Display(Name = "CODE")]
        public int Id { get; set; }

        [Display(Name = "PRODUCT")]
        [ForeignKey("TB_PRODUCT")]
        //[Column(Order = 1)]
        public int IdProduct { get; set; }
        public virtual Product Product { get; set; }

        [Column("CUS_STATE")]
        [Display(Name = "STATE")]
        public EnumBuyState State { get; set; }

        [Column("CSU_QTY")]
        [Display(Name = "QUANTITY")]
        public int BuyQuantity { get; set; }

        [Display(Name = "USER")]
        [ForeignKey("ApplicationUser")]
        //[Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
