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
    
    public partial class OWHS_Branch : BaseModel
    {
        public OWHS_Branch()
        {
            this.OITW_BranchArticles = new HashSet<OITW_BranchArticles>();
            this.ORIN_ClientCreditNotes = new HashSet<ORIN_ClientCreditNotes>();
            this.ODPI_DownPayment = new HashSet<ODPI_DownPayment>();
            this.OWTR_Transfers = new HashSet<OWTR_Transfers>();
            this.OPCH_Purchase = new HashSet<OPCH_Purchase>();
            this.ORPC_SupplierCreditNotes = new HashSet<ORPC_SupplierCreditNotes>();
            this.OIGN_GoodsReceipt = new HashSet<OIGN_GoodsReceipt>();
            this.OIGE_GoodsIssues = new HashSet<OIGE_GoodsIssues>();
            this.OINV_Sales = new HashSet<OINV_Sales>();
            this.ORDR_SpecialOrder = new HashSet<ORDR_SpecialOrder>();
            this.OWTQ_TransferRequest = new HashSet<OWTQ_TransferRequest>();
        }
    
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public string Grp_Code { get; set; }
        public string Locked { get; set; }
        public bool LockedForOrderL { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OITW_BranchArticles> OITW_BranchArticles { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<ORIN_ClientCreditNotes> ORIN_ClientCreditNotes { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<ODPI_DownPayment> ODPI_DownPayment { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OWTR_Transfers> OWTR_Transfers { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OPCH_Purchase> OPCH_Purchase { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<ORPC_SupplierCreditNotes> ORPC_SupplierCreditNotes { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OIGN_GoodsReceipt> OIGN_GoodsReceipt { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OIGE_GoodsIssues> OIGE_GoodsIssues { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OINV_Sales> OINV_Sales { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<ORDR_SpecialOrder> ORDR_SpecialOrder { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OWTQ_TransferRequest> OWTQ_TransferRequest { get; set; }
    }
}
