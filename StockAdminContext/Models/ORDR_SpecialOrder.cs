using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using Newtonsoft.Json;
using StockAdminContext.Helpers;

namespace StockAdminContext.Models
{
    public partial class ORDR_SpecialOrder : BaseModel
    {
        public ORDR_SpecialOrder()
        {
            this.ATC1_Attachments = new List<ATC1_Attachments>();
            this.RDR1_SpecialOrderDetail = new List<RDR1_SpecialOrderDetail>();
            CreatedByL = Config.CurrentUser;
            ModifiedByL = Config.CurrentUser;
            CreatedDateL = DateTime.Now;
            ModifiedDateL = DateTime.Now;
            WhsCode = Config.WhsCode;
            DocDate = DateTime.Now.Date;
            Series = 73;
            CardCode = Config.BusinessPartner;
        }

        [Key]
        public int IdSpecialOrder { get; set; }
        public int DocEntry { get; set; }
        public int AtcEntry { get; set; } 
        public int DocNum { get; set; }
        public string DocType { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public string ObjType { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string CardCode { get; set; }
        public string NumAtCard { get; set; }
        public decimal DocTotal { get; set; }
        public string Comments { get; set; }
        public string JrnlMemo { get; set; }
        public short? Series { get; set; }
        public string Ref2 { get; set; }
        public string Ref1 { get; set; }
        public System.DateTime CreatedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string ModifiedByL { get; set; }
        public System.DateTime ModifiedDateL { get; set; }
        public LocalStatus StateL { get; set; }
        public string WhsCode { get; set; }
        public bool HasToBeSync { get; set; }
        public virtual List<ATC1_Attachments> ATC1_Attachments { get; set; }
        public virtual List<RDR1_SpecialOrderDetail> RDR1_SpecialOrderDetail { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerPhone { get; set; }

        public string DownPaymentCode { get; set; }

        public string Dedication { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string ReportFileName
        {
           
            get
            {
                return string.Format("Especial_{0}_{1}.PDF", IdSpecialOrder, Config.WhsCode);                 
            }
        }

        [NotMapped]
        public string FileName { get; set; }

        [JsonIgnore]
        public virtual OWHS_Branch OWHS_Branch { get; set; }

        [JsonIgnore]
        public UCake Cake { get; set; }

        public string IdCake { get; set; }

        [JsonIgnore]
        public UCategory Category { get; set; }

        public string IdCategory { get; set; }

        [JsonIgnore]
        public UColor_Covert ColorCovert { get; set; }

        public string IdColorCovert { get; set; }

        [JsonIgnore]
        public UColor_Flower ColorFlower { get; set; }

        public string IdColorFlower { get; set; }

        [JsonIgnore]
        public UColor_Ribbon ColorRibbon { get; set; }

        public string IdRibbon { get; set; }

        [JsonIgnore]
        public UColor_top ColorTop { get; set; }

        public string IdColorTop { get; set; }

        [JsonIgnore]
        public UCover Cover { get; set; }
        
        public string IdCover { get; set; }
        
        [JsonIgnore]        
        public ULaza Laza { get; set; }
        
        public string IdLaza { get; set; }
        
        [JsonIgnore]
        public UGuirnalda Guirnalda { get; set; }

        public string IdGuirnalda { get; set; }

        [JsonIgnore]
        public UEspecialty Especiality { get; set; }

        public string IdEspeciality { get; set; }

        [JsonIgnore]
        public UFlower Flower { get; set; }

        public string IdFlower { get; set; }

        [JsonIgnore]
        public UStyle Style { get; set; }

        public string IdStyle { get; set; }

        [JsonIgnore]
        public Ufiller filler { get; set; }

        public string IdFiller { get; set; }


        [JsonIgnore]
        public UColor_Height ColorHeight { get; set; }
        public string IdColorHeight { get; set; } 
    }
}
