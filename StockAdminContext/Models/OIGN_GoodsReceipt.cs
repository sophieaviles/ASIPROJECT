using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAdminContext.Models
{
    public partial class OIGN_GoodsReceipt : BaseModel
    {
        public OIGN_GoodsReceipt()
        {
            this.IGN1_GoodsReceiptDetail = new List<IGN1_GoodsReceiptDetail>();
            
            WhsCode = Config.WhsCode;
            Series = 18; // Valor por defecto para las salidas
            DocDate = DateTime.Now.Date;
            DocDueDate = DateTime.Now;
            HasToBeSync = true;
            StateL = LocalStatus.Guardado;
            CreatedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedByL = Config.CurrentUser;
            ModifiedDateL = DateTime.Now;
        }

        [Key]
        public int IdGoodReceiptL { get; set; }
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
        public string IdMovement { get; set; }
        public short? ItmsGrpCod { get; set; }
        public LocalStatus StateL { get; set; }

        public string WhsCode { get; set; }
        public bool HasToBeSync { get; set; }
        public  List<IGN1_GoodsReceiptDetail> IGN1_GoodsReceiptDetail { get; set; }
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
