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
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Display(Name = "Product")]
        [ForeignKey("TB_PRODUCT")]
        //[Column(Order = 1)]
        public int IdProduct { get; set; }
        public virtual Product? Product { get; set; }

        [Column("CUS_STATE")]
        [Display(Name = "State")]
        public EnumBuyState State { get; set; }

        [Column("CSU_QTY")]
        [Display(Name = "Quantity")]
        public int BuyQuantity { get; set; }

        [Display(Name = "User")]
        [ForeignKey("ApplicationUser")]
        //[Column(Order = 1)]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }


        // Apenas para exibir alguns dados na tela
        [NotMapped]
        [Display(Name = "Total Quantity")]
        public int ProductsQuantity { get; set; }

        [NotMapped]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }


        [NotMapped]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [NotMapped]
        public List<Product> ProductsList { get; set;}


        [Display(Name = "Shopping")]
        [ForeignKey("TB_SHOPPING")]
        [Column(Order = 1)]
        public int ShoppingId { get; set; }
        public virtual Shopping Shopping { get; set; }

    }
}
