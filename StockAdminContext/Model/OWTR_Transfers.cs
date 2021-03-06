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
    
    public partial class OWTR_Transfers : BaseModel
    {
        public OWTR_Transfers()
        {
            this.WTR1_TransferDetails1 = new HashSet<WTR1_TransferDetails>();
        }
    
        public Nullable<int> DocEntry { get; set; }
        public Nullable<int> DocNum { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public Nullable<System.DateTime> DocDate { get; set; }
        public Nullable<System.DateTime> DocDueDate { get; set; }
        public string CardCode { get; set; }
        public string Comments { get; set; }
        public string JrnlMemo { get; set; }
        public Nullable<short> Series { get; set; }
        public Nullable<System.DateTime> TaxDate { get; set; }
        public string Filler { get; set; }
        public int StateL { get; set; }
        public Nullable<System.DateTime> DeliveryDateL { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public Nullable<System.DateTime> LastSyncDateL { get; set; }
        public string ObjType { get; set; }
        public decimal DocTotal { get; set; }
        public System.DateTime ReceptionDateL { get; set; }
        public Nullable<System.DateTime> ModifiedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string WhsCode { get; set; }
        public Nullable<bool> HasToBeSync { get; set; }
        public int IdTransferL { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual OWHS_Branch OWHS_Branch { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<WTR1_TransferDetails> WTR1_TransferDetails1 { get; set; }
    }
}
