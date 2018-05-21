using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class ODPI_DownPayment : BaseModel
    {
        public ODPI_DownPayment()
        {
            this.DPI1_DownPaymentDetail = new List<DPI1_DownPaymentDetail>();
            WhsCode = Config.WhsCode;
            CardCode = Config.BusinessPartner;
            DocDueDate = DateTime.Now;
            HasToBeSync = false;
            StateL= LocalStatus.Guardado;
            CreatedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedByL = Config.CurrentUser;
            ModifiedDateL = DateTime.Now;

        }

        [Key]
        public int IdDownPayment { get; set; }
        public int DocEntry { get; set; }
        public int? IdSaleL { get; set; }
        public int? BaseDocEntry { get; set; }
        public int DocNum { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public string ObjType { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public string CardCode { get; set; }
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
        public string ObjType1 { get; set; }
        public string DocType { get; set; }
        public string WhsCode { get; set; }
        public bool HasToBeSync { get; set; }
        public int IdPaymentType { get; set; }
        public int IdRoyalty { get; set; }
        public string PaymentAcc { get; set; }


        public virtual List<DPI1_DownPaymentDetail> DPI1_DownPaymentDetail { get; set; }

        [JsonIgnore]
        public virtual OWHS_Branch OWHS_Branch { get; set; }

        [JsonIgnore]
        public PaymentType PaymentType { get; set; }

        [JsonIgnore]
        public PaymentType RoyaltyPaymentType { get; set; }

        [NotMapped]
        public string SeriesTitle { get; set; }

        //[JsonIgnore]
        // public virtual PaymentType PaymentType { get; set; }

         
    }
}
