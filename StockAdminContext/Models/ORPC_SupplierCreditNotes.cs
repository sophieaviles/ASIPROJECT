using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class ORPC_SupplierCreditNotes : BaseModel
    {
        public ORPC_SupplierCreditNotes()
        {
            this.RPC1_SupplierCreditNoteDetail = new List<RPC1_SupplierCreditNoteDetail>();
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;
            ModifiedByL = Config.CurrentUser;
            CreatedByL = Config.CurrentUser;
            WhsCode = Config.WhsCode;

        }
        [Key]
        public int IdSupplierCreditNote { get; set; }
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
        public bool HasToBeSync { get; set; }
 
        public virtual List<RPC1_SupplierCreditNoteDetail> RPC1_SupplierCreditNoteDetail { get; set; }

        [JsonIgnore]
        public virtual OWHS_Branch OWHS_Branch { get; set; }

        [NotMapped]
        public string SeriesTitle { get; set; }
    }
}
