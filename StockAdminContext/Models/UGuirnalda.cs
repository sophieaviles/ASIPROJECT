using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class UGuirnalda:BaseModel
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
