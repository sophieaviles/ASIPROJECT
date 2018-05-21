using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StockAdminContext.Models
{
    public partial class PaymentInvoiceALOHA:BaseModel
    {
        [Key]
        public int IdPaymentInvoiceALOHA { get; set; }
        public int TYPE { get; set; }
        public int TYPEID { get; set; }
        public int check { get; set; }
        public int period { get; set; }
        public DateTime DOB { get; set; }
        public decimal AMOUNT { get; set; }
    }
}
