using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class RPC1_SupplierCreditNoteDetail : BaseModel
    {
        public RPC1_SupplierCreditNoteDetail()
        {
            WhsCode = Config.WhsCode;
        }

        [Key]
        public int IdSupplierCreditNoteDetail { get; set; }
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
        public int IdSupplierCreditNote { get; set; }
        public string TaxCode { get; set; }

        public string Dscription { get; set; } // Article Name 

        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }

        [JsonIgnore]
        public virtual ORPC_SupplierCreditNotes ORPC_SupplierCreditNotes { get; set; }

        //[JsonIgnore]
        //[NotMapped]
        //public string ArticleName
        //{
        //    get
        //    {
        //        if (OITM_Articles != null) return OITM_Articles.ItemName;
               
        //            try
        //            {
        //                OITM_Articles = ContextFactory.GetDBContext().OITM_Articles
        //                    .FirstOrDefault(a => a.ItemCode.Contains(ItemCode));

        //                return OITM_Articles != null ? OITM_Articles.ItemName : string.Empty;
        //            }
        //            catch (Exception)
        //            {
        //                //todo: cual es la razon de este catch?
        //                // ignore Exception. Web api Serialization.
        //                return string.Empty;
        //            }
                
        //    }
        //}
    }
}
