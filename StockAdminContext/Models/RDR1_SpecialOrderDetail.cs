using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class RDR1_SpecialOrderDetail : BaseModel
    {
        public RDR1_SpecialOrderDetail()
        {
            
        }
        [Key]
        public int IdSpecialOrderDetail { get; set; }
        public string AcctCode { get; set; }
        public DateTime? DocDate { get; set; }
        public int DocEntry { get; set; }
        public string Dscription { get; set; }
        public string ItemCode { get; set; }
        public int LineNum { get; set; }
        public decimal? LineTotal { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public string OcrCode { get; set; }
        public string WhsCode { get; set; }
        public int IdSpecialOrder { get; set; }

        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }

        [JsonIgnore]
        public virtual ORDR_SpecialOrder ORDR_SpecialOrder { get; set; }
    }
}
