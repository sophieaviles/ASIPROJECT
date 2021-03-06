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
    
    public partial class OITM_Articles : BaseModel
    {
        public OITM_Articles()
        {
            this.OITW_BranchArticles = new HashSet<OITW_BranchArticles>();
            this.OITM_ALOHA = new HashSet<OITM_ALOHA>();
            this.RDR1_SpecialOrderDetail = new HashSet<RDR1_SpecialOrderDetail>();
            this.INV1_SalesDetail = new HashSet<INV1_SalesDetail>();
            this.IGE1_GoodsIssueDetail = new HashSet<IGE1_GoodsIssueDetail>();
            this.IGN1_GoodsReceiptDetail = new HashSet<IGN1_GoodsReceiptDetail>();
            this.WTQ1_TransferRequestDetails = new HashSet<WTQ1_TransferRequestDetails>();
            this.RPC1_SupplierCreditNoteDetail = new HashSet<RPC1_SupplierCreditNoteDetail>();
            this.PCH1_PurchaseDetail = new HashSet<PCH1_PurchaseDetail>();
            this.WTR1_TransferDetails = new HashSet<WTR1_TransferDetails>();
            this.DPI1_DownPaymentDetail1 = new HashSet<DPI1_DownPaymentDetail>();
            this.RIN1_ClientCreditNoteDetail = new HashSet<RIN1_ClientCreditNoteDetail>();
        }
    
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Nullable<short> ItmsGrpCod { get; set; }
        public string CodeBars { get; set; }
        public string PrchseItem { get; set; }
        public string SellItem { get; set; }
        public string InvntItem { get; set; }
        public string CardCode { get; set; }
        public string BuyUnitMsr { get; set; }
        public Nullable<decimal> NumInBuy { get; set; }
        public string SalUnitMsr { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> PurPackUn { get; set; }
        public Nullable<decimal> SalPackUn { get; set; }
        public string validFor { get; set; }
        public Nullable<System.DateTime> validFrom { get; set; }
        public Nullable<System.DateTime> validTo { get; set; }
        public string ItemType { get; set; }
        public string InvntryUom { get; set; }
        public decimal NumInSale { get; set; }
        public string AssetItem { get; set; }
        public Nullable<System.DateTime> LastSyncDateL { get; set; }
        public string TemplateL { get; set; }
        public string OWHS_BranchWhsCode { get; set; }
        public string U_LINEA { get; set; }
        public string U_SUBLINEA { get; set; }
    
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OITW_BranchArticles> OITW_BranchArticles { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<OITM_ALOHA> OITM_ALOHA { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual OITB_Groups OITB_Groups { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<RDR1_SpecialOrderDetail> RDR1_SpecialOrderDetail { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<INV1_SalesDetail> INV1_SalesDetail { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<IGE1_GoodsIssueDetail> IGE1_GoodsIssueDetail { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<IGN1_GoodsReceiptDetail> IGN1_GoodsReceiptDetail { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<WTQ1_TransferRequestDetails> WTQ1_TransferRequestDetails { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<RPC1_SupplierCreditNoteDetail> RPC1_SupplierCreditNoteDetail { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<PCH1_PurchaseDetail> PCH1_PurchaseDetail { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<WTR1_TransferDetails> WTR1_TransferDetails { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<DPI1_DownPaymentDetail> DPI1_DownPaymentDetail1 { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public virtual ICollection<RIN1_ClientCreditNoteDetail> RIN1_ClientCreditNoteDetail { get; set; }
    }
}
