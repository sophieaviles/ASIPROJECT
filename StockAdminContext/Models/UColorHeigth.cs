using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public class UColor_Height: BaseModel
    {

        [Key]
        public string Code { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public UStyle Style { get; set; }
        public string IdStyle { get; set; }
        [JsonIgnore]
        public List<ORDR_SpecialOrder> SpecialOrder { get; set; }       

    }
}
