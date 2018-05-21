using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class OIGE_GoodsIssues : BaseModel
    {
        public OIGE_GoodsIssues()
        {
            this.IGE1_GoodsIssueDetail = new List<IGE1_GoodsIssueDetail>();

            WhsCode = Config.WhsCode;
            DocDate = DateTime.Now.Date;
            Series = 19; //Valor por defecto para las salidas
            DocDueDate = DateTime.Now;
            HasToBeSync = true;
            StateL = LocalStatus.Guardado;
            CreatedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedByL = Config.CurrentUser;
            ModifiedDateL = DateTime.Now;
        }

        [Key]
        public int IdGoodIssueL { get; set; }
        public int? DocEntry { get; set; } 
        public int? DocNum { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public string ObjType { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public decimal? DocTotal { get; set; }
        public string Comments { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public string CreatedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public string Ref2 { get; set; }
        public short? Series { get; set; }
        public LocalStatus StateL { get; set; }
        public string WhsCode { get; set; }
        public string IdMovement { get; set; }
        public short? ItmsGrpCod { get; set; }
        public bool HasToBeSync { get; set; }

       
        public virtual List<IGE1_GoodsIssueDetail> IGE1_GoodsIssueDetail { get; set; }

        [JsonIgnore]
        public virtual OWHS_Branch OWHS_Branch { get; set; }
        [JsonIgnore]
        public UMovement Movement { get; set; }
        [JsonIgnore]
        public OITB_Groups Group { get; set; }
        [NotMapped]
        public string FillerTitle { get; set; }
    }
}
