//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SigiApi.Model
{
    using System;
    using System.Collections.Generic;using Newtonsoft.Json;
    
    public partial class PCH1_PurchaseDetail : BaseModel
    {
        public int IdPurchaseDetail { get; set; }
        public int IdPurchase { get; set; }
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> LineTotal { get; set; }
        public string WhsCode { get; set; }
        public string AcctCode { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public string UseBaseUn { get; set; }
        public string TaxCode { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public string StateL { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual OPCH_Purchase OPCH_Purchase { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual OITM_Articles OITM_Articles { get; set; }
    }
}
