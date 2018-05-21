using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class CashMissing : BaseModel
    {
        public CashMissing()
        {
            CreatedByL = Config.CurrentUser;
            ModifiedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;
            Date = DateTime.Now;
        }

        [Key]
        public int IdCashMissing { get; set; }
        public decimal Amount { get; set; }
        
        public System.DateTime Date { get; set; }
        public string Comments { get; set; }
        public string UserAuthorization { get; set; }
        public LocalStatus StateL { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }

 
        public int? IdCashier { get; set; }
         

        [JsonIgnore]
        public LocalUser Cashier { get; set; }

        

    }
}
