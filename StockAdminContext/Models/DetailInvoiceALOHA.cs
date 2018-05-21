using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class DetailInvoiceALOHA : BaseModel
    {
        [Key]
        public int IdDetailInvoiceALOHA { get; set; }
        public int IdInvoiceALOHA { get; set; }
        public int IdALOHA_Articles { get; set; }
        public int Category { get; set; }
        public int check { get; set; }
        public int Mode { get; set; }
        public int period { get; set; }
        public DateTime DOB { get; set; }
        public int EntryId { get; set; }
        public int  Hour { get; set; }
        public int Minute { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Sysdate { get; set; }
        public int TermId { get; set; }
        public decimal discpric { get; set; }
        public int Type { get; set; }

        public virtual ALOHA_Articles ALOHA_Articles { get; set; }
        public virtual InvoiceALOHA InvoiceALOHA { get; set; }
    }
}
