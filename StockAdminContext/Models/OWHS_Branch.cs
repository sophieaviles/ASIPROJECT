using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class OWHS_Branch : BaseModel
    {
        public OWHS_Branch()
        {
            this.ODPI_DownPayment = new List<ODPI_DownPayment>();
            this.OIGE_GoodsIssues = new List<OIGE_GoodsIssues>();
            this.OIGN_GoodsReceipt = new List<OIGN_GoodsReceipt>();
            this.OINV_Sales = new List<OINV_Sales>();
            this.OITW_BranchArticles = new List<OITW_BranchArticles>();
            this.OPCH_Purchase = new List<OPCH_Purchase>();
            this.ORDR_SpecialOrder = new List<ORDR_SpecialOrder>();
            //this.ORIN_ClientCreditNotes = new List<ORIN_ClientCreditNotes>();
            this.ORPC_SupplierCreditNotes = new List<ORPC_SupplierCreditNotes>();
            this.OWTQ_TransferRequest = new List<OWTQ_TransferRequest>();
            this.OWTR_Transfers = new List<OWTR_Transfers>();
        }

        [Key] 
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public string Grp_Code { get; set; }
        public string Locked { get; set; }
        public bool LockedForOrderL { get; set; }

        [NotMapped]
        public string WhsTitle { get { return string.Format("{0} {1}", WhsCode, WhsName); }}

        [JsonIgnore]
        public virtual ICollection<ODPI_DownPayment> ODPI_DownPayment { get; set; }

        [JsonIgnore]
        public virtual ICollection<OIGE_GoodsIssues> OIGE_GoodsIssues { get; set; }

        [JsonIgnore]
        public virtual ICollection<OIGN_GoodsReceipt> OIGN_GoodsReceipt { get; set; }

        [JsonIgnore]
        public virtual ICollection<OINV_Sales> OINV_Sales { get; set; }


        [JsonIgnore]
        public virtual ICollection<OITW_BranchArticles> OITW_BranchArticles { get; set; }

        [JsonIgnore]
        public virtual ICollection<OPCH_Purchase> OPCH_Purchase { get; set; }

        [JsonIgnore]
        public virtual ICollection<ORDR_SpecialOrder> ORDR_SpecialOrder { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<ORIN_ClientCreditNotes> ORIN_ClientCreditNotes { get; set; }

        [JsonIgnore]
        public virtual ICollection<ORPC_SupplierCreditNotes> ORPC_SupplierCreditNotes { get; set; }

        [JsonIgnore]
        public virtual ICollection<OWTQ_TransferRequest> OWTQ_TransferRequest { get; set; }

        [JsonIgnore]
        public virtual ICollection<OWTR_Transfers> OWTR_Transfers { get; set; }
    }
}
