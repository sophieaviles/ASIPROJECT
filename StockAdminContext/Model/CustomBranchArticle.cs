using System.Collections.Generic;
using System.Linq;
using SigiApi.Helpers;

namespace SigiApi.Model
{
   
     public class CustomBranchArticle : BaseModel
    {
        public string InvntryUom { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal? OnHand1 { get; set; }
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                if (value == _quantity) return;
                _quantity = value;                
                RaisePropertyChanged("Quantity");
                
                if (TransferRequestHelper.CustomBranchArticlesList == null)
                {
                    TransferRequestHelper.CustomBranchArticlesList = new List<CustomBranchArticle>();
                }

                if (TransferRequestHelper.CustomBranchArticlesList.Any(c => c.ItemCode == this.ItemCode)) {
                    var ca = TransferRequestHelper.CustomBranchArticlesList.FirstOrDefault(c => c.ItemCode == this.ItemCode);
                    TransferRequestHelper.CustomBranchArticlesList.Remove(ca);
                }

                TransferRequestHelper.CustomBranchArticlesList.Add(this);
               
            }
        }


        private decimal _quantity;
    }
   
}