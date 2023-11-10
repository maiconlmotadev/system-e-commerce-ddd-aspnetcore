using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("TB_LOGSYSTEM")]
    public class LogSystem : Base
    {
        [Column("LOG_JSONINFORMATION")]
        [Display(Name = "Json Information")]
        public string JsonInformation { get; set; }

        [Column("LOG_TYPELOG")]
        [Display(Name = "Log Type")]
        public EnumLogType LogType { get; set; }

        [Column("LOG_CONTROLLER")]
        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Column("LOG_ACTION")]
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }

        [Display(Name = "User")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
