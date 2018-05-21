 using StockAdminContext.Helpers;
using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SigiFluent.ViewModels
{
    public class ArticleChooserForGoodsReceiptVM : ArticleChooserViewModel
    {
        private string movementId;
        private short groupId;
        public ArticleChooserForGoodsReceiptVM(string movementId, short groupId):base()
        {
            this.movementId = movementId;
            this.groupId = groupId;

        }

        public override bool IsGoodReceiptView
        {
            get
            {
                return false;
            }
        }

        public override ObservableCollection<OITM_Articles> ArticlesCollelction
        {
            get
            {
                //if (articles != null && articles.Count > 0) return articles;

                //articles = ArticlesHelper.GetInventarioArticles(movementId,groupId);

                //return articles; 
                // para mostrar el inventario actual
                return ArticlesHelper.GetInventarioArticles(movementId, groupId);
            }
            set
            {
                articles = value; 
                RaisePropertyChanged("Articles");
            }
        }
    }
}
