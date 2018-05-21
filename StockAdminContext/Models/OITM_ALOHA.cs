using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAdminContext.Models
{
    public partial class OITM_ALOHA : BaseModel
    {
       [Key, Column(Order = 0)]
        public int IdALOHA_Articles { get; set; }
        [Key, Column(Order = 1)]
        public string ItemCode { get; set; }
       
        public virtual ALOHA_Articles ALOHA_Articles { get; set; }
        public virtual OITM_Articles OITM_Articles { get; set; }
    }
}
