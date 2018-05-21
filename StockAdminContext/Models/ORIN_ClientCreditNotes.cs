using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class ORIN_ClientCreditNotes : BaseModel
    {
        public ORIN_ClientCreditNotes()
        {
            this.RIN1_ClientCreditNoteDetail = new List<RIN1_ClientCreditNoteDetail>();
            CreatedByL = Config.CurrentUser;
            ModifiedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;
        }

        [Key]
        public int IdClientCreditNoteL { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public string ObjType { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public string CardCode { get; set; }
        public string Comments { get; set; }
        public string NumAtCard { get; set; }
        public decimal DocTotal { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public LocalStatus StateL { get; set; }
        public string JrnlMemo { get; set; }
        public decimal VatSum { get; set; }
        public short? Series { get; set; }
        public string WhsCode { get; set; }
        
        
        public bool ExentoL { get; set; }
        public bool WithHoldingL { get; set; }
        /// <summary>
        /// Para identificar si es nota de credito de venta o de anticipo ya q ambas usan la misma tabla
        /// </summary>
        public ClientCreditNoteType? CnTypeL { get; set; }

        //[JsonIgnore]
        //public virtual OWHS_Branch OWHS_Branch { get; set; }

        [NotMapped]
        public string SeriesTitle { get; set; }

        [JsonIgnore]
        [NotMapped]
        public PaymentType PaymentTypeL { get; set; }


        public List<RIN1_ClientCreditNoteDetail> RIN1_ClientCreditNoteDetail { get; set; }
    }
}
