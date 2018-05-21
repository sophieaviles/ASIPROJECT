using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class PaymentType : BaseModel
    {
        public PaymentType()
        {
            this.OINV_Sales = new List<OINV_Sales>();
            this.OINV_RoyaltiesSales = new List<OINV_Sales>();
        }

        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPaymentType { get; set; }
        public string PaymentName { get; set; }
        public string AcctCode { get; set; }
        public bool IsRoyalty { get; set; }

        [JsonIgnore]
        public virtual List<OINV_Sales> OINV_Sales { get; set; }

        [JsonIgnore]
        public virtual List<OINV_Sales> OINV_RoyaltiesSales { get; set; }
         
        
    }
}
