using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace StockAdminContext.Models
{
    public partial class WTQ1_TransferRequestDetails :BaseModel
    {
        public int? DocEntry { get; set; }
        public int? LineNum { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? OpenQty { get; set; }
        public decimal? Price { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public DateTime? DocDate { get; set; }
        public string LineStatus { get; set; }
        public string UseBaseUn { get; set; }
        public string UnitMsr { get; set; }
        public System.DateTime? CreatedDateL { get; set; }
        public DateTime? ModifiedDateL { get; set; }
        public string ModifiedByL { get; set; }
        public string CreatedByL { get; set; }
        public int IdTransferRequestL { get; set; }

        [Key]
        public int IdTransferRequestDetailL { get; set; }



        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InvntryUom { get; set; }
        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ItemName { get; set; }
        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal OnHand1 { get; set; }


        [NotMapped]
        public string Line { get; set; }

        [NotMapped]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LineCode { get; set; }


        [NotMapped]
        [JsonIgnore]
        public decimal? IQuantity
        {
            get
            {
                
                return Quantity;
            }
            set
            {
                Quantity = value;

                RaisePropertyChanged("IQuantity");
                RaisePropertyChanged("Quantity");
                RaisePropertyChanged("IsEnabledSave");
                
               
            }
        }

        [JsonIgnore]
        public virtual OITM_Articles OITM_Articles { get; set; }

        [JsonIgnore]
        public virtual OWTQ_TransferRequest OWTQ_TransferRequest { get; set; }
    }
}
