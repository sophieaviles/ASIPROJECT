using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class ALOHA_Articles : BaseModel
    {
        public ALOHA_Articles()
        {
            this.OITM_ALOHA = new List<OITM_ALOHA>();
            this.DetailInvoiceALOHAs = new List<DetailInvoiceALOHA>();
        }

        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdALOHA_Articles { get; set; }
        public string Categories { get; set; }
        public string code { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
       

        [JsonIgnore]
        public virtual ICollection<OITM_ALOHA> OITM_ALOHA { get; set; }
        [JsonIgnore]
        public virtual ICollection<DetailInvoiceALOHA> DetailInvoiceALOHAs { get; set; }
    }
}
