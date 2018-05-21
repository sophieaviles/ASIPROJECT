using System;
using System.Activities.Debugger;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using StockAdminContext.Enums;

namespace StockAdminContext.Models
{
    public partial class OWTQ_TransferRequest : BaseModel
    {
        public OWTQ_TransferRequest()
        {
            this.WTQ1_TransferRequestDetails = new List<WTQ1_TransferRequestDetails>();
            DocDate = DateTime.Now.Date;
            DocDueDate = DateTime.Now.AddDays(1);
        }

        [Key]
        public int IdTransferRequestL { get; set; }
        public int? DocEntry { get; set; }
        public int? DocNum { get; set; }
        public string CANCELED { get; set; }
        public string DocStatus { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public string CardCode { get; set; }
        public string Comments { get; set; }
        public string JrnlMemo { get; set; }
        public short? Series { get; set; }
        public DateTime? TaxDate { get; set; }
        public string Filler { get; set; }
        public LocalStatus StateL { get; set; }
        public System.DateTime? DeliveryDateL { get; set; }
        public System.DateTime? CreatedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public DateTime? LastSyncDateL { get; set; }
        public string ObjType { get; set; }
        public DateTime? ModifiedDateL { get; set; }
        public string CreatedByL { get; set; }
        public string WhsCode { get; set; }
        public bool HasToBeSync { get; set; }

        
        public string FillerTitle
        {
            get
            {
               return  string.Format("{0} {1}", Filler,FillerName);
            }
        }
        [NotMapped]
        public string FillerName { get; set; }

        [JsonIgnore]
        public virtual OWHS_Branch OWHS_Branch { get; set; }

         
        public virtual List<WTQ1_TransferRequestDetails> WTQ1_TransferRequestDetails { get; set; }
    }

 
}
