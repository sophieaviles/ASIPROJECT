using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class OITB_Groups : BaseModel
    {
        public OITB_Groups()
        {
            this.OITM_Articles = new List<OITM_Articles>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ItmsGrpCod { get; set; }
        public string ItmsGrpNam { get; set; }
        public System.DateTime LastSyncDateL { get; set; }
        public string Locked { get; set; }
        public string StateL { get; set; }
        public string MandatoryCount { get; set; }

        [JsonIgnore]
        public virtual List<OITM_Articles> OITM_Articles { get; set; }
    }
}
