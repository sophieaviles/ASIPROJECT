using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StockAdminContext.Models
{
    public partial class RIN1_ClientCreditNoteDetail : BaseModel
    {

        public RIN1_ClientCreditNoteDetail()
        {
            
        }

        [Key]
        public int IdClientCreditNoteDetailL { get; set; }
        public int IdClientCreditNoteL { get; set; }
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public int BaseDocNum { get; set; }
        public int BaseEntry { get; set; }
        public string ItemCode { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public DateTime? DocDate { get; set; }
        public string AcctCode { get; set; }
        public string OcrCode { get; set; }

        public string TaxCode { get; set; }

        public string Dscription { get; set; } // Article Name 

        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }

        [JsonIgnore]
        public virtual ORIN_ClientCreditNotes ORIN_ClientCreditNotes { get; set; }

    }
}
