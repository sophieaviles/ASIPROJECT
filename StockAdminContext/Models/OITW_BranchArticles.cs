using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace StockAdminContext.Models
{
    public partial class OITW_BranchArticles : BaseModel
    {
        public OITW_BranchArticles()
        {
            LastSyncDateL = DateTime.Now;
        }

        [Required]
        [Key, Column(Order = 0)]
        public string ItemCode { get; set; }
        [Required]
        [Key, Column(Order = 1)]
        public string WhsCode { get; set; }
        public decimal? OnHand { get; set; }
        public decimal? OnHand1 { get; set; }
        public decimal? IsCommited { get; set; }
        public decimal? OnOrder { get; set; }
        public decimal? MinStock { get; set; }
        public decimal? MaxStock { get; set; }
        public decimal? MinOrder { get; set; }
        public string Locked { get; set; }
        public System.DateTime LastSyncDateL { get; set; }
        public virtual OITM_Articles OITM_Articles { get; set; }
        public virtual OWHS_Branch OWHS_Branch { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
