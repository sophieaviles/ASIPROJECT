using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class OPCH_Purchase : BaseModel
    {
        

        public OPCH_Purchase()
        {
            this.PCH1_PurchaseDetail = new List<PCH1_PurchaseDetail>();
            CreatedDateL = DateTime.Now;
            ModifiedByL = Config.CurrentUser;
            CreatedByL = Config.CurrentUser;
            ModifiedDateL = DateTime.Now;

        }

        [Key]
        public int IdPurchase { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public string ObjType { get; set; }

        //[Required(ErrorMessage = "El campo Fecha es Requerido")]
        public DateTime?  DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }

        //[Required(ErrorMessage =  "El campo es requerido"), MinLength(4)]
        public string CardCode { get; set; }
        public string Comments { get; set; }

        //[Required(ErrorMessage = "El campos es Requerido")]
        public short Series { get; set; }
        public System.DateTime? CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime? ModifiedDateL { get; set; }
        public LocalStatus StateL { get; set; }

        //[Required, MinLength(1)]
        public string NumAtCard { get; set; }
        public decimal DocTotal { get; set; }
        public string WhsCode { get; set; }
        public string CashVoucher { get; set; }
        public DateTime? ReinstatementDate { get; set; }
        public bool HasToBeSync { get; set; }
        public  List<PCH1_PurchaseDetail> PCH1_PurchaseDetail { get; set; }
        
        [JsonIgnore]
        public virtual OWHS_Branch OWHS_Branch { get; set; }

        [NotMapped]
        public string SeriesTitle { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool HasCashVoucher {
            get; set;
        }


    }
   
}
