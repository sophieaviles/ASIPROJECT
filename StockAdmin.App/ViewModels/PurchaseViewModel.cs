using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;
using SigiFluent.Views.SubControls;
using SigiFluent.Views.UserControls;

namespace SigiFluent.ViewModels
{
    public class PurchaseViewModel : BaseViewModel
    {
        public static event Action OnSelectedArticle;

        public ObservableCollection<OPCH_Purchase> PurchaseCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || _purchase == null)
                {
                    _purchases = PurchaseHelper.GetPurchase(SelectedSerie, StartDate, LastDate, Keyword, localStatus);
                    ForceRefresh = false;
                }
                IsBusy = false;
                return _purchases;
            }
            set { _purchases = value; RaisePropertyChanged("PurchaseCollection"); }
        }

        public ObservableCollection<NNM1_Series> PurchaseSeries
        {
            get
            {
                if (_series != null) return _series;
                _series = _series = SeriesHelper.GetSeries("18");
                IsDetailsBusy = false;
                return _series;
            }
            set
            {
                _series = value;
                RaisePropertyChanged("PurchaseSeries");
            }
        }

        public ObservableCollection<PCH1_PurchaseDetail> PurchaseDetailsCollection
        {
            get
            {
                if (Purchase != null && _purchaseDetailsCollection == null)
                {
                    IsDetailsBusy = true;
                    _purchaseDetailsCollection = new ObservableCollection<PCH1_PurchaseDetail>(Purchase.PCH1_PurchaseDetail);
                }
                RefreshPriceTotal();
                IsDetailsBusy = false;
                return _purchaseDetailsCollection;
            }
            set
            {
                _purchaseDetailsCollection = value;
                RaisePropertyChanged("PurchaseDetailsCollection");

            }
        }

        public PCH1_PurchaseDetail SelectedtPurchaseDetail
        {
            get { return _selectedPurchaseDetail; }
            set
            {
                _selectedPurchaseDetail = value;
                RaisePropertyChanged("SelectedtPurchaseDetail");
            }
        }

        public OPCH_Purchase Purchase
        {
            get { return _purchase; }
            set
            {
                _purchase = value;
                RaisePropertyChanged("Purchase");

                if (_purchase == null)
                {
                    DocDueDate = DateTime.Now;
                    ReinstatementDate = null;
                }
                else
                {
                    DocDueDate = Purchase.DocDueDate ?? DateTime.Now;
                    ReinstatementDate = Purchase.ReinstatementDate ?? null;
                }
            }
        }

        public DateTime? DocDueDate
        {
            get
            {
                return _docduedate;
            }
            set
            {
                _docduedate = value;
                if (Purchase != null) Purchase.DocDueDate = _docduedate;
                RaisePropertyChanged("DocDueDate");
            }
        }

        public DateTime? ReinstatementDate
        {
            get
            {
                return _reinstatementdate;
            }
            set
            {
                _reinstatementdate = value;
                if (Purchase != null) Purchase.ReinstatementDate = _reinstatementdate;
                RaisePropertyChanged("ReinstatementDate");
            }
        }

        public OCRD_BusinessPartner SelectedPartner
        {
            get { return _selectedPartner; }
            set { _selectedPartner = value; RaisePropertyChanged("SelectedPartner"); }
        }

        public bool IsFocusedAddButton
        {
            get { return isfocusedAddButton; }
            set { isfocusedAddButton = value; RaisePropertyChanged("IsFocusedAddButton"); }
        }

        public decimal Summary
        {
            get { return _purchaseDetailsCollection != null ? (decimal)(_purchaseDetailsCollection.Where(d => d.LineTotal != null).Sum(d => d.LineTotal)) : 0; }
        }

        public decimal Taxes
        {
            get
            {
                if (IsSerieBranch) return 0;
                return (decimal)0.1300 * Summary; }
        }

        public decimal Total
        {
            get
            {
                return Summary + Taxes;
            }

        }

        public bool IsReadOnly
        {
            get { return Purchase != null && Purchase.StateL != LocalStatus.Guardado; }
        }

        public bool HasCashVoucher
        {
            get
            {
                return Purchase != null && Purchase.HasCashVoucher;
            }
            set
            {
                if (Purchase == null) return;
                Purchase.HasCashVoucher = value;
                RaisePropertyChanged("Purchase");
            }
        }

        //public NNM1_Series SelectedSerie
        //{
        //    get { return selectedserie; }
        //    set
        //    {
        //        selectedserie = value; RaisePropertyChanged("SelectedSerie");

        //        //FRV = 54
        //        if(selectedserie != null) IsSerieBranch = selectedserie.Series == 54;
        //    }
        //}

      
        public bool IsSerieBranch {
            get { return _isseriebranch; }
            set
            {
                _isseriebranch = value;
                RaisePropertyChanged("IsSerieBranch");
                if (_isseriebranch)
                {
                    SelectedPartner = BusinessPartnerHelper.GetBusinessPartner(Config.SupplierBusinessPartner);
                    Purchase.PCH1_PurchaseDetail.Select(pd => pd.TaxCode = _defaulTaxOnBranch);
                }
                else
                {
                    if (SelectedPartner == null || Purchase == null) return;
                    var defaulTax = SelectedPartner.VatGroup ?? "IVACOM";
                    Purchase.PCH1_PurchaseDetail.Select(pd => pd.TaxCode = defaulTax);
                }
                
               
            }
        }

        /// <summary>
        /// campos estaran habilitados cuando el estado sea guardado o nuevo
        /// </summary>
        
        public bool IsReadOnlyFields
        {
            get { return Purchase != null && Purchase.StateL == LocalStatus.Guardado; }
        }

        public bool IsEnabledSave
        {
            get
            {
                if (Purchase == null) return false;
                if (PurchaseDetailsCollection == null) return false;
                if (Purchase != null && Purchase.StateL != LocalStatus.Guardado) return false;
                return PurchaseDetailsCollection.Any(d => d.Quantity > 0) &&
                       Purchase.DocDueDate != null &&
                       SelectedSerie != null; //&& !string.IsNullOrEmpty(Purchase.Comments);

            }
        }

        public bool IsEnabled
        {
            get { return !IsReadOnly; }
        }

        #region COMMANDS

        #region SELECTOR DE CLIENTE
        public ICommand OnChangePurchaseCardNameCommand
        {
            get
            {
                return new RelayCommand<TextBox>((txt) =>
                {
                    CreatePartnerChooserInstanceIfNotExists();
                    partnerChooserViewModel.OnSearchTextChanged(txt);
                    ShowBussinessPartnerChooser();

                }, (txt) => !IsReadOnly);
            }
        }
        #endregion SELECTOR DE CLIENTE

        #region AGREGA DETALLE DE ARTICULO
        public ICommand ShowArticleChooserCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (articleChooserViewModel == null)
                    {
                        articleChooserViewModel = new ArticleChooserByTotalViewModel { ProductsSources = ProductsFor.Purchase };
                        // conect to actions 
                        articleChooserViewModel.OnSelect += GetSelectedArticle;
                        articleChooserViewModel.OnClose += () => IsModalVisible = false;
                    }
                    articleChooserViewModel.IsFocusedAutoComplete = true;
                    ShowModal(new ArticleChooserEditablePrice()
                    {
                        DataContext = articleChooserViewModel
                    });
                }, () => !IsReadOnly && SelectedPartner!=null);
            }
        }

        private void GetSelectedArticle(OITM_Articles article)
        {
            IsModalVisible = false;

            if (Purchase == null) Purchase = new OPCH_Purchase();
           
            if (Purchase == null) return;
           
            var defaulTax = SelectedPartner.VatGroup ?? "IVACOM";
            if (IsSerieBranch) defaulTax = _defaulTaxOnBranch;
            
            //todo Esta pendiente traer los valores desde la base
          
            var defaultAccCode = article.AccCount?? Config.DefaultAcc;
           
            var detail = new PCH1_PurchaseDetail()
            {
                ItemCode = article.ItemCode,
                Dscription = article.ItemName,
                Price = articleChooserViewModel.ProductPriceDecimal,
                Quantity = articleChooserViewModel.Quantity,
                LineTotal = articleChooserViewModel.TotalPriceToDecimal,
               // OITM_Articles = article,
                CreatedDateL = DateTime.Now,
                ModifiedDateL = DateTime.Now,
                AcctCode = defaultAccCode,
                TaxCode = defaulTax,    
            };
            Purchase.PCH1_PurchaseDetail.Add(detail);
            articleChooserViewModel.CleanFields();

            PurchaseDetailsCollection.Add(detail);

            IsFocusedAddButton = true;

            if (OnSelectedArticle != null) OnSelectedArticle();
        }
        #endregion AGREGA DETALLE DE ARTICULO

        #region BORRA DETALLE DE ARTICULO
        public ICommand DeleteSelectedDetailsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (Purchase == null || !ConfirmDelete() || Purchase.StateL == LocalStatus.Procesado) return;
                    if (Purchase.IdPurchase > 0) PurchaseHelper.DeleteDetail(SelectedtPurchaseDetail);
                    Purchase.PCH1_PurchaseDetail.Remove(SelectedtPurchaseDetail);
                    PurchaseDetailsCollection.Remove(SelectedtPurchaseDetail);
                }, () => !IsReadOnly&& !IsBusy);
            }
        }
        #endregion BORRA DETALLE DE ARTICULO

        #region NUEVA COMPRA
        public override void ExecuteNewCommand()
        {
            Purchase = null;
            SelectedSerie = null;
            SelectedPartner = null;
            DocDueDate = ReinstatementDate = null;
            PurchaseDetailsCollection = null;
            IsSerieBranch = false;
            Purchase = new OPCH_Purchase();

            base.FormTitle = "Nueva Compra";
            ShowDialog(new NewPurchaseView(), this);
        }
        #endregion NUEVA COMPRA

        #region GUARDA COMPRA
        public ICommand CmdSavePurchase
        {
            get { return new RelayCommand(SavePurchase, () => IsEnabledSave); }
        }

        private void SavePurchase()
        {
            if (PurchaseHelper.VerifyNumAtCard(Purchase))
            {
                if (!ShowWarningMessage("Numero de Factura Repetido, Desea continuar de todas formas ?"))
                {
                    IsBusy = IsDetailsBusy = false;
                    return;
                }
                
            } 
            if (Purchase.HasCashVoucher && Purchase.ReinstatementDate == null && ConfirmDialog("Desea Guardar los cambios","Guardar"))
            {
                ShowDialog("Debe indicar la fecha de reintegro", "Error");
                return;

            }

            Purchase.DocTotal = Total;
            Purchase.SeriesTitle = SelectedSerie.SeriesName + " " + SelectedSerie.Remark;
            Purchase.Series = SelectedSerie.Series;
            Purchase.CardCode = SelectedPartner.CardCode;
            if (Purchase.IdPurchase > 0)
            {

                Purchase.ModifiedDateL = DateTime.Now;
                Purchase.ModifiedByL = Config.CurrentUser;
                Purchase.StateL = LocalStatus.Guardado;
                SaveChanges();
                RaisePropertyChanged("PurchaseCollection");
            }
            else
            {
                FillPurchaseData();
                Purchase.StateL = LocalStatus.Guardado;
                PurchaseHelper.Add(Purchase);
                SaveChanges();
                PurchaseCollection.Add(Purchase);
            }

            ViewModelManager.CloseModal();
        }
        #endregion GUARDA COMPRA

        #region PROCESA COMPRA
        public ICommand CmdProcessPurchase
        {
            get { return new RelayCommand(ProcessPurchase, CanExecuteDoProcess); }
        }

        private void ProcessPurchase()
        {
            IsDetailsBusy=  IsBusy = true;

            if (PurchaseHelper.VerifyNumAtCard(Purchase))
            {
                if (!ShowWarningMessage("Numero de Factura Repetido, Desea continuar de todas formas ?"))
                {
                    IsBusy = IsDetailsBusy = false;
                    return;
                }
                
            } 

            if (!ConfirmDialog("Desea Procesar la Compra", "Procesar?")) return;

            ViewModelManager.CloseModal();
            ShowProcessLoader(this);
            
            AsyncHelper.DoAsync(() =>
                                {
                                    Purchase.DocTotal = Total;
                                    Purchase.SeriesTitle = SelectedSerie.SeriesName + " " + SelectedSerie.Remark;
                                    Purchase.Series = SelectedSerie.Series;
                                    Purchase.CardCode = SelectedPartner.CardCode;
                                    if (Purchase.IdPurchase > 0)
                                    {
                                        Purchase.HasToBeSync = true;
                                        Purchase.ModifiedDateL = DateTime.Now;
                                        Purchase.ModifiedByL = Config.CurrentUser;
                                        //Purchase.StateL = LocalStatus.Pendiente;
                                        SaveChanges();
                                    }
                                    else
                                    {
                                        FillPurchaseData();
                                        //Purchase.StateL = LocalStatus.Pendiente;
                                        PurchaseHelper.Add(Purchase);
                                        SaveChanges();
                                        PurchaseCollection.Add(Purchase);
                                        IsBusy = true;
                                    }
                                    Synchronization.Synchronize(Purchase);
                                    SaveChanges();
                                    IsDetailsBusy = IsBusy = false;
                                    ForceRefresh = true;
                                    RaisePropertyChanged("PurchaseCollection");
                                },ViewModelManager.CloseProcessLoader);
            
        }
        #endregion PROCESA COMPRA

        #region EDITAR COMPRA SELECCIONADA
        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }

        public override void ExecuteEdit()
        {
            PurchaseEditing();
        }

        public override bool CanExecuteEdit()
        {
            return Purchase != null &&!IsBusy;
        }

        private void PurchaseEditing()
        {
            PurchaseDetailsCollection = null;
            RaisePropertyChanged("Purchase");
            IsSerieBranch = false;
            if (Purchase != null)
            {
                SelectedSerie = Purchase.Series != null ? SeriesHelper.GetSerie(Purchase.Series) : null;
                SelectedPartner = Purchase.CardCode != null ? BusinessPartnerHelper.GetBusinessPartner(Purchase.CardCode) : null;
                Purchase.HasCashVoucher = !string.IsNullOrEmpty(Purchase.CashVoucher);
                RaisePropertyChanged("HasCashVoucher");//Thread Bug. on RaiseProperty Changed on model.
                DocDueDate = Purchase.DocDueDate ?? null;
                ReinstatementDate = Purchase.ReinstatementDate ?? null;
            }
            FormTitle = "Editar Compra  " +  Purchase.DocNum;
            ShowDialog(new NewPurchaseView(), this);

        }
        #endregion EDITAR COMPRA SELECCIONADA

        #region BORRA COMPRA SELECCIONADA
        public ICommand DeleteCurrentComand
        {
            get { return new RelayCommand(ExecuteDelete, CanExecteDelete); }
        }

        public override void ExecuteDelete()
        {
            TransferRequestDeleting();
        }

        public override bool CanExecteDelete()
        {
            return Purchase != null && !IsReadOnly &&!IsBusy;
        }

        private void TransferRequestDeleting()
        {
            if (!ConfirmDelete()) return;
            PurchaseHelper.Delete(Purchase);
            PurchaseCollection.Remove(Purchase);
        }
        #endregion BORRA COMPRA SELECCIONADA

        #region PROCESA COMPRA SELECCIONADA
        public override void ExecuteDoProcess()
        {
            IsBusy = true;
            if (PurchaseHelper.VerifyNumAtCard(Purchase))
            {
                if (!ShowWarningMessage("Numero de Factura Repetido, Desea continuar de todas formas ?"))
                {
                    IsBusy = IsDetailsBusy = false;
                   ExecuteEdit();
                    return;
                }
                
            } 

            if (!ConfirmDialog("Desea procesar los cambios", "Procesar"))
            {
                UndoChanges();
                return;
            }
            ViewModelManager.CloseModal();
            ShowProcessLoader(this);

            AsyncHelper.DoAsync(PurchaseProcessing, () =>
            {
                ViewModelManager.CloseProcessLoader();
                IsBusy = false;
                ForceRefresh = true;
                RaisePropertyChanged("PurchaseCollection");
            });
        }
        
        public override bool CanExecuteDoProcess()
        {
            return Purchase != null && !IsReadOnly && !IsBusy;
        }

        private void PurchaseProcessing()
        {
            
            //Purchase.StateL = LocalStatus.Pendiente;
            Purchase.HasToBeSync = true;
            SaveChanges();
             
            var currentPurchase = PurchaseHelper.GetPurchase(Purchase.IdPurchase);
            Synchronization.Synchronize(currentPurchase);
            SaveChanges();
            IsDetailsBusy = IsBusy = false;


        }
        #endregion  PROCESA COMPRA SELECCIONADA

        #region NOTA DE CREDITO
        public override void ExecuteNewCreditNote()
        {
            
            var cn = new ORPC_SupplierCreditNotes()
            {
                CardCode = Purchase.CardCode,
                Comments = Purchase.Comments,
                Series = 9, //serie correspondiente a nota de credito
                NumAtCard = Purchase.NumAtCard,
                DocTotal = Purchase.DocTotal,
                WhsCode = Purchase.WhsCode,        
                DocDate = DateTime.Now,
                DocDueDate = DateTime.Now,
            };

            PurchaseDetailsCollection.ToList()
                .ForEach(pdc => cn.RPC1_SupplierCreditNoteDetail.Add(new RPC1_SupplierCreditNoteDetail()
                {
                    ItemCode = pdc.ItemCode,
                    Quantity = pdc.Quantity,
                    Price = pdc.Price,
                    LineTotal = pdc.LineTotal,
                    AcctCode = pdc.AcctCode,
                    TaxCode = pdc.TaxCode,
                    WhsCode = pdc.WhsCode, 
                    OITM_Articles = ArticlesHelper.GetArticle(pdc.ItemCode),
                    Dscription = ArticlesHelper.GetArticle(pdc.ItemCode).ItemName,
                }));
            
            var vm = new SupplierCreditNoteViewModel {CreditNote = cn, FormTitle = "Nueva Nota de Crédito"};
            ShowDialog(new NewCreditNotes(), vm);
        }

        public override bool CanExecuteNewCreditNote()
        {
            return Purchase != null && Purchase.StateL == LocalStatus.Procesado;
        }
        #endregion NOTA DE CREDITO
       
        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("PurchaseCollection");
        }

        #endregion COMMANDS

        #region PRIVATE METHODS
        private void CreatePartnerChooserInstanceIfNotExists()
        {
            if (partnerChooserViewModel != null) return;
            partnerChooserViewModel = new BusinessPartnerChooserViewModel();

            partnerChooserViewModel.OnSelect += GetSelectedPartner;
            partnerChooserViewModel.OnClose += () => IsModalVisible = false;
        }

        private void GetSelectedPartner(OCRD_BusinessPartner selectedPartner)
        {
            if(selectedPartner==null) return;
            IsModalVisible = false;
            SelectedPartner = selectedPartner;
            Purchase.CardCode = selectedPartner.CardCode;
            RaisePropertyChanged("Purchase");
        }

        private void ShowBussinessPartnerChooser()
        {
            CreatePartnerChooserInstanceIfNotExists();
            ShowModal(new SupplierPartnerChooser() { DataContext = partnerChooserViewModel });
        }

        private void RefreshPriceTotal()
        {
            RaisePropertyChanged("Total");
            RaisePropertyChanged("Taxes");
            RaisePropertyChanged("Summary");
        }

        private void FillPurchaseData()
        {
            var warehouse = Config.WhsCode;
            Purchase.DocDate = DateTime.Now;
            Purchase.WhsCode = warehouse;
            Purchase.CreatedByL = Config.CurrentUser;
            Purchase.CreatedDateL = DateTime.Now;
            Purchase.ModifiedByL = Config.CurrentUser;
            Purchase.ModifiedDateL = DateTime.Now;
        }
        #endregion PRIVATE METHODS

        #region Private properties
        private OPCH_Purchase _purchase { get; set; }
        private ObservableCollection<OPCH_Purchase> _purchases { get; set; }
        private PCH1_PurchaseDetail _selectedPurchaseDetail;
        private ObservableCollection<NNM1_Series> _series;
        private ObservableCollection<PCH1_PurchaseDetail> _purchaseDetailsCollection;
        private BusinessPartnerChooserViewModel partnerChooserViewModel;
        ///Filter Properties
        private bool isEditModeDetail;
        private NNM1_Series _selectedSerie;
        private ArticleChooserByTotalViewModel articleChooserViewModel;
        private bool isfocusedAddButton;
        private DateTime? _docduedate;
        private DateTime? _reinstatementdate;
        private OCRD_BusinessPartner _selectedPartner;
        private bool _isseriebranch;
        private NNM1_Series selectedserie;
        private const string _defaulTaxOnBranch = "SIN_IVA";

        #endregion

    }
}
