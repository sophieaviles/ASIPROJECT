using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using StockAdminContext.Migrations;

namespace StockAdminContext.Models
{
    public partial class IGE1_GoodsIssueDetail : BaseModel
    {
        public IGE1_GoodsIssueDetail()
        {
            WhsCode = Config.WhsCode;
            DocDate = DateTime.Now.Date;
            CreatedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedByL = Config.CurrentUser;
            ModifiedDateL = DateTime.Now;

        }
        [Key]
        public int IdGoodIssueDetail { get; set; }
        public int? DocEntry { get; set; }
        public int? LineNum { get; set; }
        public string LineStatus { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public string AcctCode { get; set; }
        public DateTime? DocDate { get; set; }
        public string UseBaseUn { get; set; }
        public string ObjType { get; set; }
        public string UnitMsr { get; set; }
        public string CodeBars { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public string CreatedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public int IdGoodIssueL { get; set; }

        [NotMapped]
        public decimal OnHand { get; set; }
          
        [JsonIgnore]
        public virtual OIGE_GoodsIssues OIGE_GoodsIssues { get; set; }

        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }
    }
}
