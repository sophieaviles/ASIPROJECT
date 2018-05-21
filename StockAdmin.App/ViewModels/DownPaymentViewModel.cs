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

namespace SigiFluent.ViewModels
{
    public class DownPaymentViewModel : BaseViewModel
    {

        public DownPaymentViewModel()
        {
            
        }

        public DownPaymentViewModel(bool asNotrack)
        {
            this.IsAsNoTrackingForDownPayment = asNotrack;
        }



        #region PUBLIC PROPERTIES
        public event Action OnClose;
        public event Action<ODPI_DownPayment> OnSelect;

        public ObservableCollection<ODPI_DownPayment> DownPaymentCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || _DownPayments == null)
                {
                    _DownPayments = DownPaymentHelper.GetDownPayments(SelectedSerie, StartDate, LastDate, Keyword, localStatus, IsAsNoTrackingForDownPayment);
                    ForceRefresh = false;
                }
                IsBusy = false;

                return _DownPayments;
            }
            set { _DownPayments = value; RaisePropertyChanged("DownPaymentColletion"); }
        }

        public ObservableCollection<ODPI_DownPayment> DownPaymentProcessedCollection
        {
            get
            {
                IsBusy = true;
                
                    _DownPaymentsProcessed = DownPaymentHelper.GetDownPaymentsProcessed(SelectedPartner,Serie);
                   
                IsBusy = false;

                return _DownPaymentsProcessed;
            }
            set { _DownPaymentsProcessed = value; RaisePropertyChanged("DownPaymentProcessedCollection"); }
        }

        public DPI1_DownPaymentDetail DownPaymentDetail
        {
            get { return _downpaumentdetail; }
            set
            {
                _downpaumentdetail = value ?? new DPI1_DownPaymentDetail();
                RaisePropertyChanged("DownPaymentDetail");
            }
        }

        public ODPI_DownPayment SelectedDownPayment
        {
            get { return _selectedDownPayment; }
            set
            {
                _selectedDownPayment = value;
                RaisePropertyChanged("SelectedDownPayment");

                //recupera el detalle
                if (_selectedDownPayment != null)
                {
                    DownPaymentDetail = _selectedDownPayment.DPI1_DownPaymentDetail.Any()
                        ? _selectedDownPayment.DPI1_DownPaymentDetail.FirstOrDefault()
                        : null;

                    //fecha
                    if (SelectedDownPayment.DocDate == null) SelectedDownPayment.DocDate = DateTime.Now;
                }
                else DownPaymentDetail = null;


            }
        }

        public ObservableCollection<DPI1_DownPaymentDetail> DownPaymentDetailsCollection
        {
            get
            {
                if (SelectedDownPayment != null && detailscollection == null)
                {
                    IsDetailsBusy = true;
                    detailscollection = new ObservableCollection<DPI1_DownPaymentDetail>(SelectedDownPayment.DPI1_DownPaymentDetail);
                }
                IsDetailsBusy = false;
                return detailscollection;
            }
            set { detailscollection = value; RaisePropertyChanged("DownPaymentDetailsCollection"); }
        }



        public ObservableCollection<PaymentType> PaymentTypes
        {
            get { return paymenttypes ?? (paymenttypes = SalesHelper.GetOnlyPaymentTypes()); }
        }

        public ObservableCollection<NNM1_Series> Series
        {
            get { return series ?? (series = SeriesHelper.GetSeries("203")); }
            set { series = value; RaisePropertyChanged("Series"); }
        }

        public NNM1_Series Serie
        {
            get { return serie; }
            set
            {
                serie = value;
                if (serie == null) return;
                if (SelectedDownPayment != null) SelectedDownPayment.Series = serie.Series;

                if (serie.SeriesName == "ANC")
                {
                    HidePartnerSelection = true;
                    SelectedPartner = BusinessPartnerHelper.GetBusinessPartner(Config.BusinessPartner);
                }
                else
                {
                    HidePartnerSelection = false;
                    SelectedPartner = null;
                }
                
                
                RaisePropertyChanged("Serie");
                CheckErrorInBookNumber();
            }
        }
        
        public bool HidePartnerSelection
        {
            get { return _hidepartnerselection; }
            set
            {
                _hidepartnerselection = value;
                RaisePropertyChanged("HidePartnerSelection");
            }
        }

        public OCRD_BusinessPartner SelectedPartner
        {
            get { return selectedPartner; }
            set { selectedPartner = value; RaisePropertyChanged("SelectedPartner"); }
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

        public bool IsRoyaltiesSeriesVisible
        {
            get { return isroyalityserievisible; }
            set
            {
                isroyalityserievisible = value;
                RaisePropertyChanged("IsRoyaltiesSeriesVisible");
            }
        }

        public bool IsReadOnly
        {
            get { return SelectedDownPayment != null && SelectedDownPayment.StateL != LocalStatus.Guardado; }
        }

        public bool IsEnabled
        {
            get { return !IsReadOnly; }
        }

        public bool IsFocusedAddButton
        {
            get { return isfocusedAddButton; }
            set { isfocusedAddButton = value; RaisePropertyChanged("IsFocusedAddButton"); }
        }

        #endregion  PUBLIC PROPERTIES
        
        #region COMMANDS
        public ICommand SelectDownPaymentCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (OnSelect != null) OnSelect(SelectedDownPayment);
                    IsModalVisible = false;
                });
            }
        }


        public ICommand ProcessCommand
        {
            get { return new RelayCommand(NewProcess,CanExecuteDoProcess); }
        }


        #region BUSSINES PARTNER CHOOSER
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
                return new RelayCommand<System.Windows.Controls.TextBox>((txt) =>
                {
                    CreatePartnerChooserInstanceIfNotExists();
                    partnerChooserViewModel.OnSearchTextChanged(txt);
                    ShowBussinessPartnerChooser();

                }, (txt) => !IsReadOnly);
            }
        }


        #endregion BUSSINES PARTNER CHOOSER


        public ICommand SaveDetailsCommand
        {
            get { return new RelayCommand(SaveNewDetails, () => !IsReadOnly); }
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

        #endregion COMMANDS

        #region OVERRIDES
        #region NOTA DE CREDITO

        public override void ExecuteNewCreditNote()
        {

            var cn = new ORIN_ClientCreditNotes()
            {
                CardCode = SelectedDownPayment.CardCode,
                DocTotal = DownPaymentDetail.Price??0,
                DocDate = SelectedDownPayment.DocDate,
                Comments = DownPaymentDetail.Dscription,
                DocDueDate = SelectedDownPayment.DocDueDate,
                Series = 3, //serie correspondiente a nota de credito
                NumAtCard = SelectedDownPayment.NumAtCard,
                WhsCode = SelectedDownPayment.WhsCode,
                PaymentTypeL = SelectedDownPayment.PaymentType,
                CnTypeL = ClientCreditNoteType.DownPayment
            };

            
            var vm = new ClientCreditNoteViewModel
            {
                CreditNote = cn,
                FormTitle = "Nueva Nota de Crédito de Anticipo",
               
            };
            ShowDialog(new DownPaymentCreditNoteView(), vm);
        }

        public override bool CanExecuteNewCreditNote()
        {
            return true;
            //return SelectedDownPayment != null && !IsReadOnly&& SelectedDownPayment.StateL == LocalStatus.Procesado;
        }

        #endregion NOTA DE CREDITO

        public override void OnCloseModal()
        {
            if (OnClose != null) OnClose();
        }

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("DownPaymentCollection");
        }

        public override void ExecuteNewCommand()
        {
            SelectedDownPayment = null;
            SelectedPartner = null;
            SelectedDownPayment = new ODPI_DownPayment();
            Serie = null;
            DownPaymentDetailsCollection = null;
            FormTitle = "Nuevo Detalle de Anticipo";
            ShowDialog(new DownPaymentView(), this, resizeMode: ResizeMode.CanResize);
        }

        public override void ExecuteEdit()
        {
            DownPaymentDetailsCollection = null;
            FormTitle = "Detalle de Anticipo " + SelectedDownPayment.DocNum.ToString();
            ShowDialog(new DownPaymentView(), this, ResizeMode.CanResize);
            RaisePropertyChanged("SelectedDownPayment");

            if (SelectedDownPayment.IdDownPayment <= 0) return;
            SelectedPartner = BusinessPartnerHelper.GetBusinessPartner(SelectedDownPayment.CardCode);
            serie = SeriesHelper.GetSerie(SelectedDownPayment.Series);
            RaisePropertyChanged("Serie");
            IsRoyality();
        }

        public override bool CanExecuteEdit()
        {
            return SelectedDownPayment != null &&!IsBusy;
        }


        public override void ExecuteDelete()
        {
            if (SelectedDownPayment == null || !ConfirmDelete()) return;
            DownPaymentHelper.Delete(SelectedDownPayment);
            SaveChanges();
            DownPaymentCollection.Remove(SelectedDownPayment);
        }

        public override bool CanExecteDelete()
        {
            return SelectedDownPayment != null && !IsReadOnly &&!IsBusy;
        }

        public override void ExecuteDoProcess()
        {
            Process();
        }

        public override bool CanExecuteDoProcess()
        {
            return !IsReadOnly && !IsBusy;
        }
        

        #endregion OVERRIDES

        #region PRIVATE METHODS

        private void CheckErrorInBookNumber()
        {
            CheckBookItem = CheckBookHelper.GetCheckBookNumber(serie.Series);

            switch (CheckBookItem.ErrorCode)
            {
                case 0:
                    ErrorMessage = string.Empty;
                    if (SelectedDownPayment == null) return;
                    SelectedDownPayment.NumAtCard = checkBookItem.Number.ToString(CultureInfo.InvariantCulture);
                    break;

                case 1:
                    if (SelectedDownPayment == null) return;
                    ErrorMessage = CheckBookItem.ErrorMessage;
                    SelectedDownPayment.NumAtCard = string.Empty;
                    break;
                default:
                    //todo: hacer el error máximo
                    break;
            }
            RaisePropertyChanged("IsReadOnlyCheckBookNumber");
        }

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
            SelectedDownPayment.CardCode = selectedPartner.CardCode;
            RaisePropertyChanged("SelectedDownPayment");
        }

        private void IsRoyality()
        {
            if (SelectedDownPayment != null && SelectedDownPayment.PaymentType != null)
            {
                SelectedDownPayment.PaymentAcc = SelectedDownPayment.PaymentType.AcctCode;
                // IsRoyaltiesSeriesVisible = SelectedDownPayment .PaymentType.IdPaymentType == 5;
            }
            else IsRoyaltiesSeriesVisible = false;
        }

        private void SaveNewDetails()
        {
            if (ConfirmDialog("Desea Guardar los Cambios", "Guardar"))
            {
                // issue resolved Factura consumidor final no tiene cuenta contable.
                if (serie.SeriesName == "ANC")
                {
                    SelectedDownPayment.CardCode = SelectedPartner.CardCode;
                }

                if (SelectedDownPayment.IdDownPayment == 0) DownPaymentHelper.Add(SelectedDownPayment);

                DownPaymentDetail.WhsCode = SelectedDownPayment.WhsCode;
                DownPaymentDetail.AcctCode = Config.DownPaymentAcc;
                DownPaymentDetail.TaxCode = SelectedDownPayment != null && SelectedDownPayment.Series == 31 ? Config.IVACOF : "IVACRF";
                SelectedDownPayment.DocTotal = (decimal)DownPaymentDetail.Price;
                SelectedDownPayment.PaymentAcc = SelectedDownPayment.PaymentType.AcctCode;
                if (DownPaymentDetail.IdDownPaymentL == 0) SelectedDownPayment.DPI1_DownPaymentDetail.Add(DownPaymentDetail);
                SaveChanges();
            }
            else
            {
                UndoChanges();
            }
            CheckBookHelper.SetNextCheckBookNumber(SelectedDownPayment.Series,SelectedDownPayment.NumAtCard,onErrorAction: ShowErrorMessageBox);
            ForceRefresh = true;
            RaisePropertyChanged("DownPaymentCollection");
            ViewModelManager.CloseModal();
        }

        private void NewProcess()
        {
            DownPaymentHelper.Add(SelectedDownPayment);
            IsBusy = true;
            var confirmed = ConfirmDialog("Desea guardar y procesar los Cambios", "Procesar");

            ShowProcessLoader(this);
            
            AsyncHelper.DoAsync(() =>
            {
                if (confirmed)
                {
                    if (SelectedDownPayment.IdDownPayment == 0)
                    {
                        // issue resolved Factura consumidor final no tiene cuenta contable.
                        if (serie.SeriesName == "ANC")
                        {
                            SelectedDownPayment.CardCode = SelectedPartner.CardCode;
                        }

                        //SelectedDownPayment.StateL = LocalStatus.Pendiente;
                        DownPaymentDetail.WhsCode = SelectedDownPayment.WhsCode;
                        DownPaymentDetail.AcctCode = Config.DownPaymentAcc;
                        DownPaymentDetail.TaxCode = SelectedDownPayment != null && SelectedDownPayment.Series == 31 ? Config.IVACOF : "IVACRF";
                        if (DownPaymentDetail != null && DownPaymentDetail.Price.HasValue)
                            SelectedDownPayment.DocTotal = (decimal) DownPaymentDetail.Price;
                        SelectedDownPayment.DPI1_DownPaymentDetail.Add(DownPaymentDetail);

                        SaveChanges();
                        CheckBookHelper.SetNextCheckBookNumber((int)SelectedDownPayment.Series,
                        Convert.ToInt32(SelectedDownPayment.NumAtCard));

                        SaveChanges();
                    }
                }
                else
                {
                    UndoChanges();
                } 
                Synchronization.Synchronize(SelectedDownPayment);
                IsBusy = false;
                SaveChanges();
                
                RefreshItemSource();
                
            },ViewModelManager.CloseProcessLoader);
            

        }

        private void Process()
        {
            IsBusy = true;
            if (SelectedDownPayment == null || !ConfirmDialog("Desea procesar seleccionado", "Confimar"))
            {
                UndoChanges();
                IsBusy = false;
                return;
            }
            ViewModelManager.CloseModal();
            ShowProcessLoader(this);
            AsyncHelper.DoAsync(() =>
                                {
                                   // SelectedDownPayment.StateL = LocalStatus.Procesado;

                                    // issue resolved Factura consumidor final no tiene cuenta contable.
                                    if (serie.SeriesName == "ANC")
                                    {
                                        SelectedDownPayment.CardCode = SelectedPartner.CardCode;
                                    }
                                    SaveChanges();
                                    
                                    Synchronization.Synchronize(SelectedDownPayment);
                                    SaveChanges();
                                    IsDetailsBusy= IsBusy = false;
                                    ForceRefresh = true;
                                    RaisePropertyChanged("DownPaymentCollection");
                                },ViewModelManager.CloseProcessLoader);
        }

        #endregion PRIVATE METHODS

        #region PRIVATE PROPERTIES

        private ODPI_DownPayment _selectedDownPayment;
        public ObservableCollection<ODPI_DownPayment> _DownPayments;
        private ObservableCollection<PaymentType> paymenttypes;

        private ObservableCollection<DPI1_DownPaymentDetail> detailscollection;
        private DPI1_DownPaymentDetail _selectedDownPaymentDetail;

        private ObservableCollection<NNM1_Series> series;
        private NNM1_Series serie;

        private OCRD_BusinessPartner selectedPartner;

        private ArticleChooserViewModel articleChooserViewModel;
        private BusinessPartnerChooserViewModel partnerChooserViewModel;
        private bool isfocusedAddButton;
        public static event Action OnSelectedArticle;
        public bool isroyalityserievisible;
        private CheckBookNumberItem checkBookItem;
        private string _downpaymentdetaildescription;
        private decimal? _downpaymentdetailprice;
        private DPI1_DownPaymentDetail _downpaumentdetail;
        private ObservableCollection<ODPI_DownPayment> _DownPaymentsProcessed;
        private bool _hidepartnerselection;
        private bool IsAsNoTrackingForDownPayment = false;

        #endregion PRIVATE PROPERTIES

    }
}
