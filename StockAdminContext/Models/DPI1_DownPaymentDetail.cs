using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class DPI1_DownPaymentDetail : BaseModel
    {
        [Key] 
        public int IdDownPaymentDetail { get; set; }
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
        public int IdDownPaymentL { get; set; }
        public string TaxCode { get; set; }

        [JsonIgnore]
        public virtual ODPI_DownPayment ODPI_DownPayment { get; set; }


     
       
    }
}
