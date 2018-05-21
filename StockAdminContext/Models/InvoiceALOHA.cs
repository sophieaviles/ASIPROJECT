using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class InvoiceALOHA : BaseModel
    {
        public InvoiceALOHA()
        {
            this.DetailInvoiceALOHAs = new List<DetailInvoiceALOHA>();
        }

        [Key]
        public int IdInvoiceALOHA { get; set; }
        public System.DateTime Date { get; set; }
        public string State { get; set; }
        public double Credit { get; set; }
        public double Cash { get; set; }
        public string correlative { get; set; }

        [JsonIgnore]
        public virtual ICollection<DetailInvoiceALOHA> DetailInvoiceALOHAs { get; set; }
    }
}
