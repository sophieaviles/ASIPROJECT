using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class INV1_SalesDetail : BaseModel
    {

        public INV1_SalesDetail ()
        {
            WhsCode = Config.WhsCode;
        }
        
        [Key]
        public int IdSaleDetail { get; set; }
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public string AcctCode { get; set; }
        public string OcrCode { get; set; }
        public int IdSaleL { get; set; }

        public string TaxStatus { get; set; }

        // exento
        public string TaxCode { get; set; }

        [NotMapped]
        public decimal SaleSubTotal { get { return (decimal) (Price*Quantity); } }

        [NotMapped]
        public decimal OnHand { get; set; }

        [NotMapped]
        public bool PriceEdited { get; set; }

        [JsonIgnore]
        public virtual OINV_Sales OINV_Sales { get; set; }

        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }


         
    }
}
