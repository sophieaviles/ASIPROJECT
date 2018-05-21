using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAdminContext.Models
{
    public partial class InventoryCountDetail:BaseModel
    {
        public InventoryCountDetail()
        {
            
        }

        [Key]
        public int IdInventoryCountDetail { get; set; }       
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string Unit { get; set; }        
        public int IdInventoryCountL { get; set; }
        public InventoryCount InventoryCount { get; set; }
        public OITM_Articles OITM_Articles { get; set; }

        [NotMapped]
        public decimal OnHand { get; set; }

        [NotMapped]
        public string Category { get; set; }
    }
}
