using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class UCake : BaseModel
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        
        [JsonIgnore]
        public List<ORDR_SpecialOrder> SpecialOrder { get; set; }
    }
}
