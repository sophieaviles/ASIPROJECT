using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using StockAdminContext.Helpers;

namespace StockAdminContext.Models
{
    public partial class WTR1_TransferDetails : BaseModel
    {
        public WTR1_TransferDetails()
        {
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;
        }
        public int? DocEntry { get; set; }
        public int? LineNum { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? OpenQty { get; set; }
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public DateTime? DocDate { get; set; }
        public string LineStatus { get; set; }
        public string UseBaseUn { get; set; }
        public string UnitMsr { get; set; }
        public DateTime CreatedDateL { get; set; }
        public DateTime? ModifiedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public string CreatedByL { get; set; }
        public int IdTransferL { get; set; }

        [Key]
        public int IdTransferDetailL { get; set; }

        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }

        [JsonIgnore]
        public virtual OWTR_Transfers OWTR_Transfers { get; set; }

        [NotMapped]
        [JsonIgnore]
        public UCategory Category {
            get; set;

        }

        [NotMapped]
        [JsonIgnore]
        public string LineCode {
            get { return Category.Code;  }
        }

    }
}
