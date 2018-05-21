using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class TransactionLog : BaseModel
    {
        [Key]
        public int IdTransactionLog { get; set; }
        public string Description { get; set; }
        public short VerificationCode { get; set; }
        public string Code { get; set; }
        public System.DateTime Date { get; set; }
        public string State { get; set; }
        public string CreatedBy { get; set; }
    }
}
