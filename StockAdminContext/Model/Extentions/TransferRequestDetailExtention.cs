using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigiApi.Model
{
    public partial class WTQ1_TransferRequestDetails: BaseModel
    {
        public string InvntryUom { get; set; }
        public string ItemName { get; set; }
        public decimal? OnHand1 { get; set; }
        public string Line { get; set; }

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
                RaisePropertyChanged("IsEnabledSave");
            }
        }
    }
}
