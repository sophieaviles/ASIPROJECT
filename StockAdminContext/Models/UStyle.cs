using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class UStyle : BaseModel
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
               
        [JsonIgnore]
        public List<ORDR_SpecialOrder> SpecialOrder { get; set; }
        [JsonIgnore]
        public List<UCover> covert { get; set; }
        [JsonIgnore]
        public List<ULaza> laza { get; set; }
        [JsonIgnore]
        public List<UGuirnalda> guirnalda { get; set; }
        [JsonIgnore]
        public List<UColor_Covert> ColorCover { get; set; }
        [JsonIgnore]
        public List<UColor_Flower> colorFlower { get; set; }
        [JsonIgnore]
        public List<UFlower> colorflowerl { get; set; }
        [JsonIgnore]
        public List<UColor_top> colorTop { get; set; }
        [JsonIgnore]
        public List<UColor_Ribbon> colorRibbon { get; set; }
        [JsonIgnore]
        public List<UColor_Height> colorHeight { get; set; }
        [JsonIgnore]
        public List<Ufiller> filler { get; set; }
    }
}
