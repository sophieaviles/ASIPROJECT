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
    
    public partial class RPC1_SupplierCreditNoteDetail : BaseModel
    {
        public int IdSupplierCreditNoteDetail { get; set; }
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public int BaseDocNum { get; set; }
        public int BaseEntry { get; set; }
        public string ItemCode { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> LineTotal { get; set; }
        public string WhsCode { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public string AcctCode { get; set; }
        public string OcrCode { get; set; }
        public int IdSupplierCreditNote { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ORPC_SupplierCreditNotes ORPC_SupplierCreditNotes { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual OITM_Articles OITM_Articles { get; set; }
    }
}