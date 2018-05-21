using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;

namespace SigiFluent.ViewModels
{
    public class ArticleChooserViewModel :BaseViewModel
    {
        public ArticleChooserViewModel()
        {
            IsFocusedAutoComplete = true;
        }

        public virtual bool IsGoodIssuesView { get { return true; } }

        public virtual bool IsGoodReceiptView { get { return true; } }
        
        public ProductsFor? ProductsSources
        {
            get { return _productsSoruces; }
            set
            {
                _productsSoruces = value;
                RaisePropertyChanged("ProductsSources");
            }
        }

        

        public virtual ObservableCollection<OITM_Articles> ArticlesCollelction
        {
            get
            {
                if (articles != null && articles.Count != 0 && !ForceRefresh) return articles;
                switch (ProductsSources)
                {
                    case ProductsFor.Purchase:
                        articles = ArticlesHelper.GetPurchaseArticles();
                        break;
                    case ProductsFor.Inventory:
                        //Implementado en la clase ArticleChooserViewModelForGoodsReceiptVM
                        break;
                    case ProductsFor.SpecialOrders:
                        articles = ArticlesHelper.GetSpecialOrdersProducts(SpecialOrder);
                        break;

                    default:
                        articles = ArticlesHelper.GetSalesArticles();
                        break;
                }
                ForceRefresh = false;
                return articles;
            }
            set { articles = value; RaisePropertyChanged("Articles"); }
        }

        public OITM_Articles SelectedArticle
        {
            get { return selectedArticle; }
            set 
            {
                selectedArticle = value; forceRefreshPrice = true;
                RaisePropertyChanged("SelectedArticle"); 
                RaisePropertyChanged("ProductPrice");
                RaisePropertyChanged("TotalPrice");
                RaisePropertyChanged("Goods");
                Quantity = null;
                IsFocusedQuantity = selectedArticle != null;
                
            }
        }

        public decimal? Goods 
        {
            get 
            {
                //if (selectedArticle != null && selectedArticle.OITW_BranchArticles.Count != 0)
                //    return selectedArticle.OITW_BranchArticles.First().OnHand1;
                if (selectedArticle == null) return 0;
                //return 0;
                return ArticlesHelper.GetOnHandFor(selectedArticle.ItemCode);
            }
            set { }
        }

        public double? ProductPrice
        {
            get
            {
                if (SelectedArticle == null || !forceRefreshPrice) return productprice;
                productprice = ArticlesHelper.GetAlohaPrice(SelectedArticle.ItemCode);
                forceRefreshPrice = false;
                return productprice;
            }
            set
            {
                productprice = value;
                RaisePropertyChanged("ProductPrice");
                RaisePropertyChanged("TotalPrice");
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

                if (productprice != null && SelectedArticle != null
                && quantity!=null) return (double?)  productprice.Value* (double) quantity.Value;
                return null;
            }
            set { }
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

        public bool SetEditablePrice
        {
            get
            {
                return SelectedArticle != null && SelectedArticle.PriceEdited;
            }
            set
            {
                if (SelectedArticle == null) return;
                SelectedArticle.PriceEdited = value;
                if (!value)
                {
                    forceRefreshPrice = true;
                    RaisePropertyChanged("ProductPrice");
                }
                RaisePropertyChanged("SetEditablePrice");
                RaisePropertyChanged("SelectedArticle"); 
            }
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
                                        }, CanSelectArticle);
            }
        }

        public bool CanSelectArticle()
        {
            // para los pedidos especiales 
            if (ProductsSources == ProductsFor.SpecialOrders)
            {
                return SelectedArticle!=null &&  quantity.HasValue && productprice.HasValue ;
            }

            return true;
        }

        //private static string FixDotsCad(string cad)
        //{
        //    if (cad == ".") cad = "0.00";
        //    var dots = cad.Count(x => x == '.');
        //    var dot = cad.IndexOf('.');
        //    if (dots <= 1) return cad;
        //    cad = cad.Replace(".", "");
        //    cad = cad.Insert(dot, ".");
        //    return cad;
        //}

        public ICommand OnQuantityChangedCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Controls.TextBox>((qtytxt) =>
                {
                    //test.Count(x => x == '&');
                    var cad = qtytxt.Text;
                    if (string.IsNullOrEmpty(cad)) return;
                 //   cad = FixDotsCad(cad);

                    var stringvalue = new string(cad.Where(c => char.IsNumber(c) || c == '.').ToArray());
                    var val = (decimal?)Convert.ToDouble(stringvalue,CultureInfo.InvariantCulture);

                    if (!(val > 0)) return;
                    if (Quantity == val) return;
                    Quantity = val;
                    RaisePropertyChanged("TotalPrice");
                    
                });
            }
        }

      

        public ICommand OnPriceChangedCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Controls.TextBox>((pricetxt) =>
                {
                    var cad = pricetxt.Text;
                    
                    if (string.IsNullOrEmpty(cad)) return;
            //        cad = FixDotsCad(cad);
                   
                    var val = Convert.ToDouble(cad,CultureInfo.InvariantCulture);

                    if (!(val > 0)) return;
                    if (ProductPrice == val) return;
                    forceRefreshPrice = false;
                    ProductPrice =val;
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
            ProductPrice = null;
            IsFocusedAutoComplete = true;
            RaisePropertyChanged("ProductPrice");
            RaisePropertyChanged("TotalPrice");
        }

        public   event Action OnClose;
        public   event Action<OITM_Articles> OnSelect;
        public bool ForceRefresh { get; set; }
        public ORDR_SpecialOrder SpecialOrder { get; set; }
        #region Private Properties

        protected ObservableCollection<OITM_Articles> articles;
        private OITM_Articles selectedArticle;
        private double? productprice;
        private bool forceRefreshPrice;
        private decimal? quantity = null;
        private bool isFocusedQuantity;
        private bool isFocusedAtucomplete;
        private ProductsFor? _productsSoruces;

        #endregion
    }
}
