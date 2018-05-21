using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
   public partial class UWarehouseOrder:BaseModel
    {
        [Key]
        public string Code { get; set; }
        public string Whscode { get; set; }
        public string IdCategory { get; set; }       
    }
}
