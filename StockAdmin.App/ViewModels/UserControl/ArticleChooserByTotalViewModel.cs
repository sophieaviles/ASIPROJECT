using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;

namespace SigiFluent.ViewModels
{
    public class ArticleChooserByTotalViewModel:BaseViewModel
    {
        public ArticleChooserByTotalViewModel()
        {
            IsFocusedAutoComplete = true;
        }

        public ProductsFor? ProductsSources
        {
            get { return _productsSoruces; }
            set
            {
                _productsSoruces = value;
                RaisePropertyChanged("ProductsSources");
            }
        }

        public ObservableCollection<OITM_Articles> ArticlesCollection
        {
            get
            {
                if (articles != null && articles.Count > 0) return articles;

                switch (ProductsSources)
                {
                    case ProductsFor.Purchase:
                        articles =   ArticlesHelper.GetPurchaseArticles();
                        break;
                    case ProductsFor.Inventory:
                        //todo: metodo para recuperar articulos por inventario
                        break;
                    default:
                        articles = ArticlesHelper.GetSalesArticles();
                        break;
                }
                return articles;
            }
            set { articles = value; RaisePropertyChanged("Articles"); }
        }

        public OITM_Articles SelectedArticle
        {
            get { return selectedArticle; }
            set 
            {
                selectedArticle = value; 
                RaisePropertyChanged("SelectedArticle"); 
                RaisePropertyChanged("TotalPrice");
                RaisePropertyChanged("ProductPrice");
                Quantity = null;
                IsFocusedQuantity = selectedArticle != null;
            }
        }

        public double? ProductPrice
        {
            get
            {
                return  ((totalPrice != null && SelectedArticle != null && quantity != null) ? 
                    Math.Round((totalPrice.Value /(double) quantity.Value), 4)
                    : 0);
            }
           
        }

        public decimal? ProductPriceDecimal
        {
            get { return ProductPrice.HasValue ? Convert.ToDecimal(ProductPrice) : (decimal?) null; }
        }

        public decimal? Quantity
        {
            get
            {
                return quantity;
            }
            set 
            {
                quantity = value; 
                RaisePropertyChanged("Quantity"); 
                RaisePropertyChanged("TotalPrice");
            }
        }

        public double? TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                RaisePropertyChanged("TotalPrice");
                RaisePropertyChanged("ProductPrice");
            }
        }

        public string TotalValue
        {
            get { return totalValue; }
            set
            {
                totalValue = value;
                double val;
                            var isnumber = Double.TryParse(value, out val);
                            if(isnumber) TotalPrice = val;
                RaisePropertyChanged("TotalValue");
            }
        }


        public decimal? TotalPriceToDecimal
        {
            get { return TotalPrice.HasValue ? Convert.ToDecimal(TotalPrice) : (decimal?)null; }
        }

        public bool IsFocusedQuantity
        {
            get { return isFocusedQuantity;  }
            set { isFocusedQuantity = value; RaisePropertyChanged("IsFocusedQuantity"); }
        }

        public bool IsFocusedAutoComplete
        {
            get { return isFocusedAtucomplete; }
            set { isFocusedAtucomplete = value; RaisePropertyChanged("IsFocusedAtuComplete"); }
        }

        #region Commands
        public ICommand SelectArticleCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            IsModalVisible = false;
                                            if (OnSelect != null) OnSelect(SelectedArticle);
                                        }, () => SelectedArticle != null);
            }
        }

        public ICommand OnQuantityChangedCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Controls.TextBox>((qtytxt) =>
                {
                    var val = new String(qtytxt.Text.Where(char.IsNumber).ToArray());

                    if (!string.IsNullOrEmpty(val))
                    {
                        var qty = Convert.ToDecimal(new string(qtytxt.Text.Where(Char.IsNumber).ToArray()));
                        Quantity = qty;
                        RaisePropertyChanged("ProductPrice");
                    }
                    else
                    {
                        quantity = 0;
                        RaisePropertyChanged("ProductPrice");
                    }
                });
            }
        }

       
        #endregion

        public override void OnCloseModal()
        {
            if (OnClose != null) OnClose();
        }

        public void CleanFields()
        {
            SelectedArticle = null;
            Quantity = null;
            TotalPrice = null;
            TotalValue = null;
            IsFocusedAutoComplete = true;
            RaisePropertyChanged("TotalPrice");
            RaisePropertyChanged("ProductPrice");
            
        }

        public   event Action OnClose;
        public   event Action<OITM_Articles> OnSelect;

        #region Private Properties

        private ObservableCollection<OITM_Articles> articles;
        private OITM_Articles selectedArticle;
        private decimal? quantity = null;
        private bool isFocusedQuantity;
        private bool isFocusedAtucomplete;
        private ProductsFor? _productsSoruces;
        private double? totalPrice;
        private string totalValue;

        #endregion
    }
}
