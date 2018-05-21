using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class PCH1_PurchaseDetail : BaseModel
    {

        public PCH1_PurchaseDetail()
        {
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;
            ModifiedByL = Config.CurrentUser;
            CreatedByL = Config.CurrentUser;
            WhsCode = Config.WhsCode;
        }


        [Key]
        public int IdPurchaseDetail { get; set; }
        public int IdPurchase { get; set; }
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public decimal? Quantity { get; set; } 
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public string AcctCode { get; set; }
        public DateTime? DocDate { get; set; }
        public string UseBaseUn { get; set; }

        [Required(ErrorMessage = "El Campo es Requerido")]
        public string TaxCode { get; set; }
        public System.DateTime? CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime? ModifiedDateL { get; set; }

        public string Dscription { get; set; } // Article name 
        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }
        
        [JsonIgnore]
        public virtual OPCH_Purchase OPCH_Purchase { get; set; }

        

        //[JsonIgnore]
        //public string ArticleName {
        //    get
        //    {
        //        if (OITM_Articles == null)
        //        {
        //            try { 
        //                    OITM_Articles =
        //                ContextFactory.GetDBContext().OITM_Articles.FirstOrDefault(a => a.ItemCode.Contains(ItemCode));
        //                }catch(Exception ex)
        //                {
                            
        //                }
        //        }

        //        if (OITM_Articles != null) return OITM_Articles.ItemName;
        //        else return string.Empty;
        //    }
        //}
    }
}
