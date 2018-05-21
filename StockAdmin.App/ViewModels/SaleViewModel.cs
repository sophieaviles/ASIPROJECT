using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;
using SigiFluent.Views.UserControls;
using TextBox = System.Windows.Controls.TextBox;

namespace SigiFluent.ViewModels
{
    public class SaleViewModel : BaseViewModel
    {

        #region PROPERTIES
        public ObservableCollection<OINV_Sales> SalesCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || sales == null)
                {
                    sales = SalesHelper.GetSales(SelectedSerie, StartDate, LastDate, Keyword, localStatus);
                    IsBusy = false;
                    ForceRefresh = false;
                }
                IsBusy = false;
                return sales;
            }

        }

        public OINV_Sales SelectedSale
        {
            get { return selectedSale; }
            set { selectedSale = value; RaisePropertyChanged("SelectedSale"); }
        }

        public ObservableCollection<INV1_SalesDetail> SalesDetailsCollection
        {
            get
            {
                if (SelectedSale != null && detailsCollection == null)
                {
                    IsDetailsBusy = true;
                    detailsCollection = new ObservableCollection<INV1_SalesDetail>(SelectedSale.INV1_SalesDetail);

                }
                
                IsDetailsBusy = false;
                total = (decimal)(detailsCollection.Where(d => d.LineTotal != null).Sum(d => d.LineTotal));
                if (total <= Limite && Exento) Exento = false;
                else RefreshPriceTotal();
                return detailsCollection;
            }
            set { detailsCollection = value; RaisePropertyChanged("SalesDetailsCollection"); }
        }

        public INV1_SalesDetail SelectedSaleDetail
        {
            get { return selectedSaledetail; }
            set { selectedSaledetail = value; RaisePropertyChanged("SelectedSaleDetail"); }

        }

        public ObservableCollection<PaymentType> PaymentTypes
        {
            get { return paymenttypes ?? (paymenttypes = SalesHelper.GetPaymentTypes()); }
        }

        public ObservableCollection<NNM1_Series> Series
        {
            get { return series ?? (series = SeriesHelper.GetSeries("13")); }
            set { series = value; RaisePropertyChanged("Series"); }
        }

        public NNM1_Series Serie
        {
            get
            {
                return serie;
            }
            set
            {
                serie = value;
                RaisePropertyChanged("Serie");

                if (serie == null) return;
                if (SelectedSale != null) SelectedSale.Series = serie.Series;


                CheckBookItem = CheckBookHelper.GetCheckBookNumber(serie.Series);

                switch (CheckBookItem.ErrorCode)
                {
                    case 0:
                        ErrorMessage = string.Empty;
                        if (SelectedSale == null) return;
                        SelectedSale.NumAtCard = checkBookItem.Number.ToString(CultureInfo.InvariantCulture);
                        break;

                    case 1:
                        ErrorMessage = CheckBookItem.ErrorMessage;
                        if (SelectedSale == null) return;
                        SelectedSale.NumAtCard = string.Empty;
                        break;
                    default:
                        //todo: hacer el error máximo
                        break;
                }
                // Add to Downpayment Chooser
                //CreateDownPartnerChooserIfNotExists();
                //downPaymentViewModel.Serie = value;

                RaisePropertyChanged("SelectedSale");
                RaisePropertyChanged("IsReadOnlyCheckBookNumber");
            }
        }

        public CheckBookNumberItem CheckBookItem
        {
            get
            {
                return checkBookItem;
            }
            set
            {
                checkBookItem = value;
                RaisePropertyChanged("CheckBookItem");
            }
        }

        public bool IsReadOnlyCheckBookNumber
        {
            get { return CheckBookItem != null && CheckBookItem.ErrorCode == 0; }
        }

        public ObservableCollection<PaymentType> RoyaltiesSeries
        {
            get { return royaltiesSeries ?? (royaltiesSeries = SalesHelper.GetPaymentTypes(isRoyality: true)); }
        }

        public bool IsRoyaltiesSeriesVisible
        {
            get { return isroyalitiesseriesvisible; }
            set
            {
                isroyalitiesseriesvisible = value;
                RaisePropertyChanged("IsRoyaltiesSeriesVisible");
            }
        }

        public ODPI_DownPayment SelectedDownPayment
        {
            get { return _selecteddownpayment; }
            set
            {
                _selecteddownpayment = value;
                if (value != null && SelectedSale != null) SelectedSale.dpEntry = value.DocEntry;
                RaisePropertyChanged("SelectedDownPayment");
                RaisePropertyChanged("SelectedDownPaymentDetail");
                RefreshPriceTotal();
            }
        }

        public string SelectedDownPaymentDetail
        {
            get { return (SelectedDownPayment != null && SelectedDownPayment.DPI1_DownPaymentDetail.Any()) ? 
                SelectedDownPayment.DPI1_DownPaymentDetail.FirstOrDefault().Dscription: 
                string.Empty;
            }
        }

        public OCRD_BusinessPartner SelectedPartner
        {
            get { return selectedPartner; }
            set { selectedPartner = value; RaisePropertyChanged("SelectedPartner"); RaisePropertyChanged("HasSelectedPartner"); }
        }

        public bool HasSelectedPartner
        {
            get { return SelectedPartner != null;  }
        }

        public bool IsReadOnly
        {
            get { return SelectedSale != null && 
                SelectedSale.StateL != LocalStatus.Guardado
                ;
            }
        }

        public bool IsEnabled
        {
            get { return !IsReadOnly; }
        }

        public bool IsCommandsEnbaled
        {
            get
            {
               
                return SelectedSale != null && (!IsReadOnly && Total >= 0 &&
                                                SelectedSale.NumAtCard != null &&
                                                SelectedSale.Series != null &&
                                                SelectedSale.PaymentType != null &&
                                                SelectedSale.DocDate != null &&
                                                SelectedPartner != null &&
                                                SelectedSale.INV1_SalesDetail.Any() &&!IsBusy);
            }
        }

        public decimal Summary
        {
            get { return total - Taxes; }
        }

        public decimal Taxes
        {
            get
            {
                var tax = (decimal)0.00;
                if (Exento) return tax;

                if (detailsCollection != null)
                {
                    if (WithHolding)
                    {
                        tax = ((decimal)Config.IVARETValue * Total) / ((decimal)Config.IVARETValue + 1); 
                    }
                    else
                    {
                        tax = ((decimal)Config.IVACOMValue * Total) / ((decimal)Config.IVACOMValue + 1);
                    }
                }
                return tax;
            }
        }

        public decimal Total
        {
            get
            {
                decimal ttal;
                if (detailsCollection == null) ttal = 0;
                else ttal = (decimal)(detailsCollection.Where(d => d.LineTotal != null).Sum(d => d.LineTotal));

                if (HasDownPayment && SelectedDownPayment != null) ttal = ttal - SelectedDownPayment.DocTotal; 
                return ttal;
            }
        }
        
        public bool Exento
        {
            get
            {
                return _exento;

            }
            set
            {
                //CheckIVAEXE(_excento = value);
                _exento = value;
                RaisePropertyChanged("Exento");
                if (WithHolding) WithHolding = false;
                RefreshPriceTotal();
            }
        }

        public bool WithHolding {
            get
            {

                return _whitHolding;
            }
            set
            {
                _whitHolding = value;
                //CheckIVARET(_whitHolding = value);
                RaisePropertyChanged("WithHolding");
                RefreshPriceTotal();
            }
        }

        public bool EnableExcento
        {
            get
            {
                return  selectedSale != null && SelectedSale.INV1_SalesDetail.Any() && total > Limite;
            }
        }

        public bool IsFocusedAddButton
        {
            get { return isfocusedAddButton; }
            set { isfocusedAddButton = value; RaisePropertyChanged("IsFocusedAddButton"); }
        }
        
        public bool HasDownPayment {
            get { return _hasdownpayment; }
            set
            {
                _hasdownpayment = value;
                if (!_hasdownpayment)
                {
                    SelectedDownPayment = null;}
                RaisePropertyChanged("HasDownPayment");

                RefreshPriceTotal();
            }
        }

#region USUARIOS CON PERMISO PARA CAMBIAR PRECIOS
       

        public Operations CurrentOperation
        {
            get; set;
        }

        public bool UserIsValid
        {
            get { return _userIsValid; }
            set
            {
                _userIsValid = value;
                RaisePropertyChanged("UserIsValid");
            }
        }

        public void FinishCurrentOperation()
        {
            switch (CurrentOperation)
            {
                case Operations.Process:
                    NewProcess();
                    break;
                case Operations.Save:
                    SaveNewDetails();
                    break;
            }
        }

        private void ValidateUser(bool obj)
        {
            IsModalVisible = false;
            UserIsValid = obj;
            if (UserIsValid)
            {
                
                FinishCurrentOperation();
                UserIsValid = false;
            }
            else
            {
                ErrorMessage = "Operación no puede continuar";
            }
        }


#endregion USUARIOS CON PERMISO PARA CAMBIAR PRECIOS


        #endregion PROPERTIES

        #region Articles Picker
        //private decimal GetOnHandFromProduct(OITM_Articles product)
        //{
        //    var branchArticle = product.OITW_BranchArticles.Where(p => p.ItemCode == product.ItemCode)
        //                .Select(p => p.OnHand1);
        //    var onHand = branchArticle.Any() ? branchArticle.FirstOrDefault().Value : (decimal)0;
        //    return onHand;
        //}
        
        private void GetSelectedArticle(OITM_Articles article)
        {
            IsModalVisible = false;

            if (SelectedSale == null) SelectedSale = new OINV_Sales();

            if (SelectedSale == null) return;

            var detail = new INV1_SalesDetail()
            {
                ItemCode = article.ItemCode,
                Price = articleChooserViewModel.ProductPriceDecimal,
                Quantity = articleChooserViewModel.Quantity,
                LineTotal = articleChooserViewModel.TotalPriceToDecimal,
                //todo:    OITM_Articles = article
                OnHand = ArticlesHelper.GetOnHandFor(article.ItemCode), // GetOnHandFromProduct(article),
                PriceEdited = article.PriceEdited,
                //TaxCode =  SelectedSerie!=null && SelectedSerie.Series==43 ? "IVACOF":"IVACRF",// quemado por juan
                Dscription =  article.ItemName,
                
            };

            SelectedSale.INV1_SalesDetail.Add(detail);
            articleChooserViewModel.CleanFields();

            detailsCollection.Add(detail);
            RaisePropertyChanged("SalesDetailsCollection");
            IsFocusedAddButton = true;

            if (OnSelectedArticle != null) OnSelectedArticle();
        }
        
        #endregion

        #region COMMANDS

        public ICommand SearchDownPaymentCommand
        {
            get
            {
                return new RelayCommand(ShowDownPaymentChooser, ()=> !IsReadOnly);
            }
        }

        public ICommand OnChangeDownPaymentCommand
        {
            get
            {
                return new RelayCommand<TextBox>((txt) =>
                {
                    ShowDownPaymentChooser();

                }, (txt) => !IsReadOnly);
            }
        }

        public ICommand SearchCustomerCommand
        {
            get
            {
                return new RelayCommand(ShowBussinessPartnerChooser, () => !IsReadOnly);
            }
        }

        public ICommand OnChangeCustomerNameCommand
        {
            get
            {
                return new RelayCommand<TextBox>((txt) =>
                                        {
                                            CreatePartnerChooserInstanceIfNotExists();
                                            partnerChooserViewModel.OnSearchTextChanged(txt);
                                            ShowBussinessPartnerChooser();

                                        }, (txt) => !IsReadOnly );
            }
        }

        public ICommand ShowArticleChooserCommand
        {
            get
            {

                return new RelayCommand(() =>
                                        {
                                            if (articleChooserViewModel == null)
                                            {
                                                articleChooserViewModel = new ArticleChooserViewModel()
                                                                          {
                                                                              ProductsSources = ProductsFor.Sell
                                                                          };
                                                // conect to actions 
                                                articleChooserViewModel.OnSelect += GetSelectedArticle;
                                                articleChooserViewModel.OnClose += () => IsModalVisible = false;

                                            }
                                            articleChooserViewModel.IsFocusedAutoComplete = true;
                                            ShowModal(new ArticleChooser()
                                                      {
                                                          DataContext = articleChooserViewModel
                                                      });


                                        }, () => !IsReadOnly);
            }
        }

        public ICommand SaveDetailsCommand
        {
            get { return new RelayCommand(SaveNewDetails, () => IsCommandsEnbaled); }
        }

        public ICommand DeleteSelectedDetailsCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            if (SelectedSaleDetail == null || !ConfirmDelete() || SelectedSale.StateL == LocalStatus.Procesado) return;
                                            if (SelectedSaleDetail.IdSaleL > 0) SalesHelper.DeleteDetail(SelectedSaleDetail);
                                            SelectedSale.INV1_SalesDetail.Remove(SelectedSaleDetail);
                                            detailsCollection.Remove(SelectedSaleDetail);
                                            RaisePropertyChanged("SalesDetailsCollection");
                                           
                                        }, () => !IsReadOnly && SelectedSaleDetail != null);
            }
        }

        public ICommand OnChangePaymentType
        {
            get { return new RelayCommand(IsRoyality, () => !IsReadOnly); }
        }

        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }

        public ICommand DeleteCurrentComand
        {
            get { return new RelayCommand(ExecuteDelete, CanExecteDelete); }
        }

        public ICommand ProcessCommand
        {
            get { return new RelayCommand(NewProcess, ()=>IsCommandsEnbaled); }
        }

        #endregion

        #region OVERRIDES

        #region NOTA DE CREDITO

        public override void ExecuteNewCreditNote()
        {

            var cn = new ORIN_ClientCreditNotes()
            {
                CardCode = SelectedSale.CardCode,
                DocDate = SelectedSale.DocDate,
                Comments = SelectedSale.Comments,
                Series = 3, //serie correspondiente a nota de credito
                NumAtCard = SelectedSale.NumAtCard,
                DocTotal = SelectedSale.DocTotal,
                WhsCode = SelectedSale.WhsCode,
                PaymentTypeL = SelectedSale.PaymentType,
                CnTypeL = ClientCreditNoteType.Sale
            };

            SalesDetailsCollection.ToList()
                .ForEach(pdc =>
                         {
                             var article = ArticlesHelper.GetArticle(pdc.ItemCode);
                             cn.RIN1_ClientCreditNoteDetail.Add(

                                 new RIN1_ClientCreditNoteDetail()
                                 {
                                     ItemCode = pdc.ItemCode,
                                     Quantity = pdc.Quantity,
                                     Price = pdc.Price,
                                     LineTotal = pdc.LineTotal,
                                     OITM_Articles = article,
                                     BaseDocNum = selectedSale.DocNum,
                                     BaseEntry = selectedSale.DocEntry,
                                     Dscription = article.ItemName,
                                     TaxCode = pdc.TaxCode,
                                 });
                         }
                );


            var vm = new ClientCreditNoteViewModel
                     {
                        CreditNote = cn, FormTitle = "Nueva Nota de Crédito de Venta",
                        //todo: reemplazar esto
                        Exento = SelectedSale.INV1_SalesDetail.Any(d => d.TaxCode == Config.IVACOM),
                        WithHolding = !Exento && SelectedSale.INV1_SalesDetail.Any(d => d.TaxCode == Config.IVARET),
                     };
            ShowDialog(new NewClientCreditNote(), vm);
        }

        public override bool CanExecuteNewCreditNote()
        {
            return SelectedSale != null && SelectedSale.StateL == LocalStatus.Procesado && !ClientCreditNoteHelper.ExistCreditNote(SelectedSale);
        }
        #endregion NOTA DE CREDITO

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("SalesCollection");
        }

        public override void ExecuteNewCommand()
        {
            ErrorMessage = string.Empty;
            SelectedSale = null;
            SelectedPartner = null;
            SelectedDownPayment = null;
            HasDownPayment = false;
            Serie = null;
            UserIsValid = false;
            SelectedSale = new OINV_Sales();
            selectedSale.DocDate = DateTime.Now;
            SalesDetailsCollection = null;
            FormTitle = "Detalle de Nueva Venta";
            ShowDialog(new SaleDetailsView(), this, resizeMode: ResizeMode.CanResize);
        }

        public override void ExecuteEdit()
        {
            ErrorMessage = string.Empty;
            SalesDetailsCollection = null;
            RaisePropertyChanged("SelectedSale");
            if (SelectedSale != null)
            {
                UserIsValid = false;
                SelectedPartner = BusinessPartnerHelper.GetBusinessPartner(SelectedSale.CardCode);
                SelectedDownPayment = DownPaymentHelper.GetDownPaymentInSale(SelectedSale,asNotrack: true);
                HasDownPayment = SelectedDownPayment != null;
                Exento = SelectedSale.INV1_SalesDetail.Any(d => d.TaxCode == Config.IVAEXE);
                WithHolding = !Exento && SelectedSale.INV1_SalesDetail.Any(d => d.TaxCode == Config.IVARET);
                
                serie = SeriesHelper.GetSerie(selectedSale.Series);
                RaisePropertyChanged("Serie");
                IsRoyality();

                // Para actualizar existencias.
                var itemCodes = SelectedSale.INV1_SalesDetail.Select(d => d.ItemCode).ToList();
                var inventory = ArticlesHelper.GetSalesArticles(itemCodes);

                SelectedSale.INV1_SalesDetail.ToList().ForEach(d =>
                {
                    var product = inventory.FirstOrDefault(p => p.ItemCode == d.ItemCode);
                    if (product != null)
                        d.OnHand = ArticlesHelper.GetOnHandFor(product.ItemCode); // GetOnHandFromProduct(product);
                });
            }
            FormTitle = "Detalle de Venta " + SelectedSale.DocNum;
            ShowDialog(new SaleDetailsView(), this, resizeMode: ResizeMode.CanResize);

        }

        public override bool CanExecuteEdit()
        {
            return SelectedSale != null &&!IsBusy;
        }

        public override void ExecuteDelete()
        {
            if (SelectedSale == null || !ConfirmDelete()) return;
            SalesHelper.DeleteSale(SelectedSale);
            SaveChanges();
            SalesCollection.Remove(SelectedSale);
        }

        public override bool CanExecteDelete()
        {
            return !IsReadOnly && !IsBusy;
        }

        public override bool CanExecuteDoProcess()
        {
            return !IsReadOnly && !IsBusy;
        }

        public override void ExecuteDoProcess()
        {
            Process();
        }

        #endregion

        #region PRIVATE METODS

        private void ShowUserWithRightsChooser()
        {

        }

        #region DOWN PAYMENT CHOOSER
        
        private void ShowDownPaymentChooser()
        {
            if (SelectedPartner == null || Serie == null)
            {
                ShowErrorMessageBox("Debe seleccionar un tipo y un cliente primero");
                return;
            }
           var downPaymentViewModel = new DownPaymentViewModel(asNotrack: true)
           {
               Serie = serie,
              
           };

            downPaymentViewModel.OnSelect += GetSelectedDownPayment;
            downPaymentViewModel.OnClose += () => IsModalVisible = false;

            downPaymentViewModel.SelectedPartner = selectedPartner; // nota el orden afecta Selected partner verificar que el view model inicializa estas propiedades bien.
             
            ShowModal(new DownPaymentChooser() { DataContext = downPaymentViewModel });

        }

        //private void CreateDownPartnerChooserIfNotExists()
        //{
        //    //if (downPaymentViewModel != null) return;
        //    downPaymentViewModel = new DownPaymentViewModel(asNotrack : true);

        //    downPaymentViewModel.OnSelect += GetSelectedDownPayment;
        //    downPaymentViewModel.OnClose += () => IsModalVisible = false;
        //}
        
        private void GetSelectedDownPayment(ODPI_DownPayment selectedDownPayment)
        {
            IsModalVisible = false;
            //Customer Name.
            SelectedDownPayment = selectedDownPayment;
            
            RaisePropertyChanged("SelectedSale");
        }

        #endregion DOWN PAYMENT CHOOSER


        #region BUSSINES PARTNER CHOOSER
        private void ShowBussinessPartnerChooser()
        {
            CreatePartnerChooserInstanceIfNotExists();
            ShowModal(new BusinessPartnerChooser() { DataContext = partnerChooserViewModel });
        }

        private void CreatePartnerChooserInstanceIfNotExists()
        {
            if (partnerChooserViewModel != null) return;
            partnerChooserViewModel = new BusinessPartnerChooserViewModel();

            partnerChooserViewModel.OnSelect += GetSelectedPartner;
            partnerChooserViewModel.OnClose += () => IsModalVisible = false;
        }

        private void GetSelectedPartner(OCRD_BusinessPartner selectedPartner)
        {
            IsModalVisible = false;
            //Customer Name.
            SelectedPartner = selectedPartner;
            SelectedSale.CardCode = selectedPartner.CardCode;
            RaisePropertyChanged("SelectedSale");
        }
        #endregion BUSSINES PARTNER CHOOSER

        private void IsRoyality()
        {
            if (SelectedSale != null && SelectedSale.PaymentType != null)
            {
                IsRoyaltiesSeriesVisible = SelectedSale.PaymentType.IdPaymentType == 5;
            }
            else IsRoyaltiesSeriesVisible = false;
        }

        private void SaveNewDetails()
        {
            CurrentOperation = Operations.Save;
            var autorizacion = SalesDetailsCollection.ToList().Any(dc => dc.PriceEdited);
            if (!UserIsValid && autorizacion)
            {
                ShowUserValidatorPicker();
                return;
            }

            if (SalesHelper.VerifyNumAtCard(SelectedSale))
            {
                if (ShowWarningMessage("Numero de Factura Repetido, Desea continuar de todas formas ?")) return;
                IsBusy = IsDetailsBusy = false;
                return;
            } 


            if (!ConfirmDialog("Desea Guardar Los Cambios", "Guardar"))
            {
                UndoChanges();
                return;
            }

            // Excluir articulos no inventariables. 
            var productsToExclude = ArticlesHelper.GetProductsOnInventory( SalesDetailsCollection.Select(d=> d.ItemCode).ToList());

            //.Where(p=> p.OITM_Articles.InvntItem.Contains("Y"))
            var isnotValid = SalesDetailsCollection.Where(p=> !productsToExclude.Contains(p.ItemCode))
                
                .Any(d =>
            {
                if (d.Quantity > d.OnHand)
                {
                    ErrorMessage = string.Format( "El Articulo : {0} Codigo {1} ,Quedara en Negativo", d.Dscription, d.ItemCode);
                    ShowErrorMessageBox(ErrorMessage);
                    return true;
                }
               
                    ErrorMessage = string.Empty;
                    return false;
                
            });
            if (isnotValid) return;

            if (!Exento && !WithHolding)
            {
                CheckIVACOM(true);
            }
            else
            {
                CheckIVAEXE(Exento);
                CheckIVARET(WithHolding);
            }
            if (!HasDownPayment) SelectedSale.dpEntry = 0;
            SalesHelper.AddSale(SelectedSale);
            //if (SelectedDownPayment != null)
            //{
            //    if (HasDownPayment) SelectedDownPayment.IdSaleL = SelectedSale.IdSaleL; else SelectedDownPayment.IdSaleL = null;
            //}


            SaveChanges();

            // TODO validar los valores Series y NumAtCard  son diferentes de null , 
            //y considerar el caso en que los valores son 0

            if (SelectedSale == null) return; // TODO Show error Message

            var serieNumber =   (int) (SelectedSale.Series.HasValue? SelectedSale.Series : 0);
            var numAtcoard = !string.IsNullOrEmpty(SelectedSale.NumAtCard)
                            ? Convert.ToInt32(SelectedSale.NumAtCard)
                            : 0;

            CheckBookHelper.SetNextCheckBookNumber(serieNumber, numAtcoard);
            ViewModelManager.CloseModal();
            RefreshItemSource();
        }

        private void ShowUserValidatorPicker()
        {
            var membership = new MembershipViewModel() {ErrorMessage = "Necesita Autorización para continuar"};
            membership.OnSelect += ValidateUser;
            membership.OnClose += () => IsModalVisible = false;

            ShowModal(new UsersWithRightsPicker()
                      {
                          DataContext = membership,
                      });
        }

        private void NewProcess()
        {
            CurrentOperation = Operations.Process;
            var autorizacion = SalesDetailsCollection.ToList().Any(dc => dc.PriceEdited);
            if (!UserIsValid && autorizacion)
            {
                ShowUserValidatorPicker();
                return;
            }

            IsBusy = IsDetailsBusy = true;
            if (SalesHelper.VerifyNumAtCard(SelectedSale))
            {
                if (!ShowWarningMessage("Numero de Factura Repetido, Desea continuar de todas formas ?"))
                {
                    IsBusy = IsDetailsBusy = false;
                    return;
                }
                
            } 

            if (!ConfirmDialog("Desea procesar la venta?", "procesar"))
            {
                UndoChanges();
                IsBusy = IsDetailsBusy = false;
                return;
            }
            ViewModelManager.CloseModal();
            ShowProcessLoader(this);
            AsyncHelper.DoAsync(() =>
                                {
                                    

                                        if (!Exento && !WithHolding)
                                        {
                                            CheckIVACOM(true);
                                        }
                                        else
                                        {
                                            CheckIVAEXE(Exento);
                                            CheckIVARET(WithHolding);
                                        }

                                        //SelectedSale.StateL = LocalStatus.Pendiente;
                                        SalesHelper.AddSale(SelectedSale);
                                        if (HasDownPayment && SelectedDownPayment != null)
                                        {
                                            SelectedDownPayment = SelectedDownPayment.ForceUpdateToDataBase();
                                            SelectedDownPayment.IdSaleL = SelectedSale.IdSaleL;
                                        }
                                        else if (SelectedDownPayment != null) SelectedDownPayment.IdSaleL = null;

                                        SaveChanges();

                                        CheckBookHelper.SetNextCheckBookNumber((int) SelectedSale.Series,
                                            Convert.ToInt32(selectedSale.NumAtCard));
                                        
                                    
                                        Synchronization.Synchronize(SelectedSale);
                                        
                                     
                                        IsBusy = IsDetailsBusy = false;
                                        
                                        SaveChanges();
                                    RefreshItemSource();
                                },ViewModelManager.CloseProcessLoader);
            
        }

        private void RefreshPriceTotal()
        {
            RaisePropertyChanged("Total");
            RaisePropertyChanged("Taxes");
            RaisePropertyChanged("Summary");
            RaisePropertyChanged("EnableExcento");
        }

        private void CheckIVACOM(bool IsIvaCom)
        {
            if (selectedSale == null || !IsIvaCom) return;

            if (SelectedSale.INV1_SalesDetail.Any() )
            {
                SelectedSale.INV1_SalesDetail.ForEach(pd => pd.TaxCode = SelectedSale.Series == 43 ? "IVACOF" : "IVACRF");
            }
        }

        private void CheckIVAEXE(bool IsIvaExe)
        {
            if (selectedSale == null || !IsIvaExe) return;

            if (SelectedSale.INV1_SalesDetail.Any())
            {
                SelectedSale.INV1_SalesDetail.ForEach(pd => pd.TaxCode = "IVA_EXE");
            }
        }

        private void CheckIVARET(bool IsIvaRet)
        {
            if (selectedSale == null || !IsIvaRet) return;

            if (SelectedSale.INV1_SalesDetail.Any())
            {
                SelectedSale.INV1_SalesDetail.ForEach(pd => pd.TaxCode = Config.IVARET);
            }
        }

        private void Process()
        {
            
            IsDetailsBusy = IsBusy = true;
            if (SalesHelper.VerifyNumAtCard(SelectedSale))
            {
                if (!ShowWarningMessage("Numero de Factura Repetido, Desea continuar de todas formas ?"))
                {
                    IsBusy = IsDetailsBusy = false;
                    return;
                }
            } 

            if (SelectedSale == null || !ConfirmDialog("Desea procesar?", "Confimar"))
            {
                UndoChanges();
                IsBusy = IsDetailsBusy = false;
                return;
            }
            if (HasDownPayment && SelectedDownPayment != null)
            {
                SelectedDownPayment = SelectedDownPayment.ForceUpdateToDataBase();
              SelectedDownPayment.IdSaleL = SelectedSale.IdSaleL;

            }
            else if (SelectedDownPayment != null)
            {
                SelectedDownPayment = SelectedDownPayment.ForceUpdateToDataBase();
                SelectedDownPayment.IdSaleL = null;
            }
           
            ViewModelManager.CloseModal();
            ShowProcessLoader(this);

            AsyncHelper.DoAsync(() =>
                                {
                                       IsBusy = IsDetailsBusy = true;
                                        //SelectedSale.StateL = LocalStatus.Pendiente;
                                        SaveChanges();
                                        Synchronization.Synchronize(SelectedSale);
                                        SaveChanges();
                                        RefreshItemSource();
                                },ViewModelManager.CloseProcessLoader);
        }

        #endregion PRIVATE METHODS

        #region PRIVATE PROPERTIES

        private ObservableCollection<OINV_Sales> sales;
        private ObservableCollection<PaymentType> paymenttypes;
        private ObservableCollection<INV1_SalesDetail> detailsCollection;
        private INV1_SalesDetail selectedSaledetail;
        private OINV_Sales selectedSale;
        private ObservableCollection<NNM1_Series> series;
        private NNM1_Series serie;
        private ObservableCollection<PaymentType> royaltiesSeries;
        private bool isroyalitiesseriesvisible;
        private OCRD_BusinessPartner selectedPartner;

        private ArticleChooserViewModel articleChooserViewModel;
        private BusinessPartnerChooserViewModel partnerChooserViewModel;
        private bool isfocusedAddButton;
        public static event Action OnSelectedArticle;
        private CheckBookNumberItem checkBookItem;
        private bool _ivacomp;
        //private DownPaymentViewModel downPaymentViewModel;
        private ODPI_DownPayment _selecteddownpayment;
        private bool _hasdownpayment;
        private ObservableCollection<LocalUser> usrscollection;
        
        private decimal total;
        private bool _whitHolding;
        private bool _exento;
        private LocalUser _priceschanger;
        private bool _userIsValid;
        private const int Limite = 100;
        

        #endregion
    }
}
