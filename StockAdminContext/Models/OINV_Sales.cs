using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class OINV_Sales : BaseModel
    {
        public OINV_Sales()
        {
            this.INV1_SalesDetail = new List<INV1_SalesDetail>();
            WhsCode = Config.WhsCode;
            DocDate = DateTime.Now.Date;
            HasToBeSync = true;
            StateL = LocalStatus.Guardado;
            CreatedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;
            HasToBeSync = true;
            ModifiedByL = Config.CurrentUser;
            CreatedByL = Config.CurrentUser;
            Comments = string.Empty;
        }

        [Key]
        public int IdSaleL { get; set; }
        public int DocEntry { get; set; }
        public int dpEntry { get; set; }
        public int DocNum { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public string ObjType { get; set; }
         
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }

        [Required]
        public string CardCode { get; set; } // Cliente.

        // Numero de factura.
        public string NumAtCard { get; set; }
        public decimal DocTotal { get; set; }
        public string Comments { get; set; }
        public string JrnlMemo { get; set; }
       
        public short? Series { get; set; }
        public DateTime? TaxDate { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public LocalStatus StateL { get; set; }
        
        public string DocType { get; set; }
        public string WhsCode { get; set; }

        public int? IdPaymentType { get; set; }

        public int? IdRoyalityPayementType { get; set; }

        [NotMapped]
        public string SeriesTitle { get; set; }

        [NotMapped]
        public bool IsExt { get; set; } 
        public bool HasToBeSync { get; set; }

        public List<INV1_SalesDetail> INV1_SalesDetail { get; set; }
        
        [JsonIgnore]
        public virtual OWHS_Branch OWHS_Branch { get; set; }

        public PaymentType PaymentType { get; set; }

        public PaymentType RoyaltyPaymentType { get; set; }

        [NotMapped]
        public List<InvoicePayment> paymentsAloha { get; set; }
    }
}
