using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class Deposit : BaseModel
    {

        public Deposit()
        {
            CreatedByL = Config.CurrentUser;
            ModifiedByL = Config.CurrentUser;
            ModifiedDateL = DateTime.Now;
            CreatedDateL = DateTime.Now;
            Date = DateTime.Now;
        }

        [Key]
        public int IdDeposits { get; set; }

        public System.DateTime Date { get; set; }
        // public string Cashier { get; set; }
        public decimal Cash { get; set; }
        public decimal Cheque { get; set; }
        public string UserAuthorization { get; set; }
        public LocalStatus StateL { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public int Number { get; set; }

        public string Comments { get; set; }

        public int? IdCashier { get; set; }

        public int? IdShopManager { get; set; }


        public string CashierName { get; set; }
        public string ShopManagerName { get; set; }
   

        [JsonIgnore]
        public LocalUser Cashier { get; set; }

        [JsonIgnore]
        public LocalUser ShopManager { get; set; }
    }
}
