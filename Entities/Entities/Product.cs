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
    [Table("Product")]
    public class Product : Notifies
    {
        [Column("PRO_ID")]
        [Display(Name ="Code")]
        public int ProductId { get; set; }

        [Column("PRO_NAME")]
        [Display(Name = "Name")]
        public int Name { get; set; }

        [Column("PRO_VALUE")]
        [Display(Name = "Value")]
        public int Value { get; set; }

        [Column("PRO_STATE")]
        [Display(Name = "State")]
        public int State { get; set; }
    }
}
