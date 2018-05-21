using System;
using System.Activities.Debugger;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using StockAdminContext.Enums;

namespace StockAdminContext.Models
{
    public partial class InventoryCount : BaseModel
    {

        public InventoryCount()
        {
            this.InventoryCountDetail = new List<InventoryCountDetail>();
            this.CreatedDateL = DateTime.Now;
            this.ModifiedDateL = DateTime.Now;
            DocDate = DateTime.Now.Date;
            ModifiedByL = Config.CurrentUser;
            CreatedByL = Config.CurrentUser;

        }

        [Key]
        public int IdInventoryCountL { get; set; }
        
        public int DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public decimal DocTotal { get; set; }
        public string Comments { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public string CreatedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public short? ItmsGrpCod { get; set; }
        public LocalStatus StateL { get; set; }
        public virtual List<InventoryCountDetail> InventoryCountDetail { get; set; }
        
        [NotMapped]
        public string GroupName { get; set; }
    }
}
