using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockAdminContext.Models
{
    public partial class UMovements_Acc : BaseModel
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string U_ITEMCODE { get; set; }
        public string U_IDMOVIMIENTO { get; set; }
        public string U_CUENTA { get; set; }
    }
}
