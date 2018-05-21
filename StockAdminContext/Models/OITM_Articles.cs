using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using StockAdminContext.Helpers;

namespace StockAdminContext.Models
{
    public partial class OITM_Articles : BaseModel
    {
        public OITM_Articles()
        {
            this.DPI1_DownPaymentDetail = new List<DPI1_DownPaymentDetail>();
            this.IGE1_GoodsIssueDetail = new List<IGE1_GoodsIssueDetail>();
            this.IGN1_GoodsReceiptDetail = new List<IGN1_GoodsReceiptDetail>();
            this.INV1_SalesDetail = new List<INV1_SalesDetail>();
            this.OITM_ALOHA = new List<OITM_ALOHA>();
            this.OITW_BranchArticles = new List<OITW_BranchArticles>();
            this.PCH1_PurchaseDetail = new List<PCH1_PurchaseDetail>();
            this.RDR1_SpecialOrderDetail = new List<RDR1_SpecialOrderDetail>();
            this.RIN1_ClientCreditNoteDetail = new List<RIN1_ClientCreditNoteDetail>();
            this.RPC1_SupplierCreditNoteDetail = new List<RPC1_SupplierCreditNoteDetail>();
            this.WTQ1_TransferRequestDetails = new List<WTQ1_TransferRequestDetails>();
            this.WTR1_TransferDetails = new List<WTR1_TransferDetails>();
        }

        [Key]
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public short? ItmsGrpCod { get; set; }
        public string CodeBars { get; set; }
        public string PrchseItem { get; set; }
        public string SellItem { get; set; }
        public string InvntItem { get; set; }
        public string CardCode { get; set; }
        public string DfltWH { get; set; }
        public string BuyUnitMsr { get; set; }
        public decimal? NumInBuy { get; set; }

        public string SalUnitMsr { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? PurPackUn { get; set; }
        public decimal? SalPackUn { get; set; }
        public string validFor { get; set; }
        public DateTime? validFrom { get; set; }
        public DateTime? validTo { get; set; }
        public string ItemType { get; set; }
        public string InvntryUom { get; set; }
        public decimal NumInSale { get; set; }
        public string AssetItem { get; set; }
        public DateTime? LastSyncDateL { get; set; }
        public string TemplateL { get; set; }
        public string U_categoria { get; set; }
        public string U_estilo { get; set; }
        public string U_Especialidad { get; set; }
        public string U_torta { get; set; }


        [NotMapped]
        public string AccCount { get; set; }

        [NotMapped]
        public bool PriceEdited { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsTemplate { 
            get{
                return TemplateL!=null && TemplateL.Contains("Y");
        }
            set {
                    TemplateL = value ? "Y" : "N";
                    RaisePropertyChanged("IsTemplate");
                    RaisePropertyChanged("TemplateL");
                    
                    var list = BranchsHelper.TemplateList;
                    var itm = list.FirstOrDefault(i => i.ItemCode == this.ItemCode);
                    if (itm != null) { list.Remove(itm); }
                    list.Add(this);
            }
        }
      
        [JsonIgnore]
        public virtual ICollection<DPI1_DownPaymentDetail> DPI1_DownPaymentDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<IGE1_GoodsIssueDetail> IGE1_GoodsIssueDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<IGN1_GoodsReceiptDetail> IGN1_GoodsReceiptDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<INV1_SalesDetail> INV1_SalesDetail { get; set; }
        [JsonIgnore]
        public virtual OITB_Groups OITB_Groups { get; set; }
        [JsonIgnore]
        public virtual ICollection<OITM_ALOHA> OITM_ALOHA { get; set; }
       
        public virtual ICollection<OITW_BranchArticles> OITW_BranchArticles { get; set; }
        public virtual ICollection<PCH1_PurchaseDetail> PCH1_PurchaseDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<RDR1_SpecialOrderDetail> RDR1_SpecialOrderDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<RIN1_ClientCreditNoteDetail> RIN1_ClientCreditNoteDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<RPC1_SupplierCreditNoteDetail> RPC1_SupplierCreditNoteDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<WTQ1_TransferRequestDetails> WTQ1_TransferRequestDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<WTR1_TransferDetails> WTR1_TransferDetails { get; set; }
    }
}
