using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;
using System.Windows;
//using SigiFluent.Views.Reports;
using SigiFluent.Views.UserControls;
using System.Data.Entity;
namespace SigiFluent.ViewModels
{
    public class GoodsIssuesViewModel:BaseViewModel
   {
        
        public bool IsEnabled 
        {
            get 
            {
                return isEnabled;
            }
            set 
            {
                isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }
       
        public ObservableCollection<OIGE_GoodsIssues> GoodsIssueses
        {
            get
            {
                IsBusy = true;
                if (InternalGoodsIssueses.Count == 0 || ForceRefresh)
                {
                    var groupId =ABSelectedGroup == null ? (short)-1 : ABSelectedGroup.ItmsGrpCod;

                    InternalGoodsIssueses = GoodIssuesHelper.GetGoodsIssues(StartDate, 
                                                                                LastDate,
                                                                                groupId, Keyword, localStatus);
                    ForceRefresh = false;
                    IsBusy = false;                    
                }
                IsBusy = false;
                return InternalGoodsIssueses;
            }
        }
        
        public OITB_Groups SelectedGroup
        {
            get
            {
                return InternalSelectedGroup;
            }
            set
            {
                InternalSelectedGroup = value;
                RaisePropertyChanged("SelectedGroup");
            }
        }

        public UMovement SelectedMovement
        {
            get
            {
                return InternalSelectedMovement;                
            }
            set
            {                
                InternalSelectedMovement = value;
                RaisePropertyChanged("SelectedMovement");
            }
        }

        public DateTime DocDueDate
        {
            get { return SelectedGoodsIssues != null &&
                         SelectedGoodsIssues.DocDueDate.HasValue
                         ? SelectedGoodsIssues.DocDueDate.Value : DateTime.Now;
            }
            set 
            {
                if (SelectedGoodsIssues != null)
                    SelectedGoodsIssues.DocDueDate = value;

                RaisePropertyChanged("DocDueDate"); 
            }
        }

        public string DocComments
        {
            get
            {
                return SelectedGoodsIssues != null
                       ? SelectedGoodsIssues.Comments : "";
            }
            set
            {
                if (SelectedGoodsIssues != null)
                    SelectedGoodsIssues.Comments = value;

                RaisePropertyChanged("DocComments");
            }
        }

        private ObservableCollection<OITB_Groups> InternalGroups;

        public ObservableCollection<OITB_Groups> Groups
        {
            get
            {
                if (InternalGroups != null) return InternalGroups;

                InternalGroups = GroupHelper.GetGroups();

                return InternalGroups;
            }

            set
            {
                InternalGroups = value;
                RaisePropertyChanged("Groups");
            }
             
        }

        private ObservableCollection<UMovement> InternalMovementTypes;

        public ObservableCollection<UMovement> MovementTypes
        {
            get 
            {
                if (InternalMovementTypes != null) return InternalMovementTypes;

                InternalMovementTypes = UMovementHelper.GetMovementsCollection("S");
                
                return InternalMovementTypes;
            }

            set 
            {
                InternalMovementTypes = value;
                RaisePropertyChanged("MovementTypes");
            }
   

        }

        public ObservableCollection<IGE1_GoodsIssueDetail> GoodsIssuesDetails
        {
            get
            {
                return _internalGoodsIssuesDetailsCollection;
            }
            set
            {
                _internalGoodsIssuesDetailsCollection = value;
                RaisePropertyChanged("GoodsIssuesDetails");
            }
        }

        public OIGE_GoodsIssues SelectedGoodsIssues
        {
            get { return InternalGoodsIssues; }
            set 
            { 
                InternalGoodsIssues = value;

                RaisePropertyChanged("SelectedGoodsIssues"); 
                
            }
        }

        public IGE1_GoodsIssueDetail SelectedArticleDetail { get; set; }

        public bool IsReadOnly
        {
            get { return SelectedGoodsIssues != null && SelectedGoodsIssues.StateL != LocalStatus.Guardado; }
        }

        
        public GoodsIssuesViewModel()
        {
                
        }

        #region ToolBarz
        public override void ExecuteNewCommand()
        {            
            FormTitle = "Nueva Salida";

            SelectedGoodsIssues = new OIGE_GoodsIssues();
            SelectedGroup = null;
            SelectedMovement = null;
            GoodsIssuesDetails = new ObservableCollection<IGE1_GoodsIssueDetail>();
            IsEnabled = true;
            ShowDialog(new GoodIssuesView(), this, resizeMode: ResizeMode.CanResize);
        }

        public override void ExecuteEdit()
        {
            FormTitle = "Salida " + SelectedGoodsIssues.DocNum.ToString();

            GoodsIssuesDetails = new ObservableCollection<IGE1_GoodsIssueDetail>(SelectedGoodsIssues.IGE1_GoodsIssueDetail);

            IsEnabled = GoodsIssuesDetails.Count == 0;

            if(!string.IsNullOrEmpty( SelectedGoodsIssues.IdMovement)) SelectedMovement = MovementTypes.First(m => m.Code == SelectedGoodsIssues.IdMovement);

           if(SelectedGoodsIssues.ItmsGrpCod.HasValue)  SelectedGroup = Groups.FirstOrDefault(g => g.ItmsGrpCod == SelectedGoodsIssues.ItmsGrpCod);

            // actualizar inventario actual.
            var inventory =
                ArticlesHelper.GetArticlesFor(SelectedGoodsIssues.IGE1_GoodsIssueDetail.Select(p => p.ItemCode).ToList());


            GoodsIssuesDetails.ToList().ForEach(d =>
                                                {
                                                    var product = inventory.FirstOrDefault(p => p.ItemCode == d.ItemCode);
                                                    if (product != null && product.OnHand1.HasValue) d.OnHand = product.OnHand1.Value;
                                                            //GetOnHandFromProduct(product);
                                                });

            ShowDialog(new GoodIssuesView(), this);
        }

        public override bool CanExecuteEdit()
        {
            return SelectedGoodsIssues != null && !IsBusy;
        }

        public override void ExecuteDelete()
        {
            if (SelectedGoodsIssues != null && ConfirmDelete())
            {
                GoodIssuesHelper.DeleteGoodsReceipt(SelectedGoodsIssues);
                SaveChanges();
                GoodsIssueses.Remove(SelectedGoodsIssues);
            }
        }

        public override bool CanExecteDelete()
        {
            return !IsReadOnly &&!IsBusy;
        }

        public override bool CanExecuteDoProcess()
        {
            return !IsReadOnly &&! IsBusy ;
        }

        public override void ExecuteDoProcess()
        {
            Process();
        }

        public override bool CanViewReport()
        {
            return SelectedGoodsIssues != null && SelectedGoodsIssues.IdGoodIssueL!=0;
        }

        public override void ViewReport()
        {
            var param = new Dictionary<string, string>();

            param.Add("@id", SelectedGoodsIssues.IdGoodIssueL.ToString());

            var data = StoredCallbackProcessor.CallDataSet("SP_INV_REP_AJUSTES_S", param);
            // var report = new ReportContainer("rptAjustedeInventarios.rpt", data);
           // ShowDialog(report, this);
        }

        #endregion

        #region GoodReceiptViewDialog

        public ICommand SaveDetailsCommand
        {
            get
            {
                return new RelayCommand(()=> SaveDetailsChanges());
            }
        }

        public ICommand ShowArticleChooserCommand
        {
            
                get { 
                
                    return new RelayCommand(() =>
                                            {

                                                if (SelectedMovement == null || SelectedGroup == null) 
                                                {
                                                    MessageBox.Show("Debes seleccionar un tipo de movimiento y un grupo");
                                                    return;
                                                }

                                                if (ArticleChooserViewModel != null) 
                                                {
                                                    ArticleChooserViewModel = null;                                                    
                                                }
                                                
                                                ArticleChooserViewModel = new ArticleChooserForGoodsReceiptVM(SelectedMovement.Code, SelectedGroup.ItmsGrpCod);
                                                ArticleChooserViewModel.OnSelect += articleChooserViewModel_OnSelect;
                                                ArticleChooserViewModel.OnClose += () => IsModalVisible = false;

                                                ArticleChooserViewModel.IsFocusedAutoComplete = true;
                                                ShowModal(new ArticleChooser()
                                                          {
                                                              DataContext = ArticleChooserViewModel
                                                          });

                                                
                                            },()=>!IsReadOnly);
                }
            
        }
 

        void articleChooserViewModel_OnSelect(OITM_Articles obj)
        {
            IsModalVisible = false;
             
            var detail = new IGE1_GoodsIssueDetail()
            {
                ItemCode = obj.ItemCode,                
                Quantity = ArticleChooserViewModel.Quantity,       
                UnitMsr = obj.InvntryUom,
                AcctCode = obj.AccCount,
               // OITM_Articles = obj
               OnHand = ArticlesHelper.GetOnHandFor(obj.ItemCode),
               Dscription = obj.ItemName,
            };


            ArticleChooserViewModel.CleanFields();

            
            GoodsIssuesDetails.Add(detail);

            if(SelectedGoodsIssues!=null) SelectedGoodsIssues.IGE1_GoodsIssueDetail.Add(detail);

            RaisePropertyChanged("GoodsIssuesDetails");

            IsEnabled = GoodsIssuesDetails.Count == 0;
        }

        public ICommand DeleteSelectedDetailsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SelectedArticleDetail == null) 
                    {
                        MessageBox.Show("Debes seleccionar un articulo");
                    }

                    if (SelectedArticleDetail != null && ConfirmDelete() && SelectedGoodsIssues.StateL != LocalStatus.Procesado) 
                    {
                       

                        if (SelectedGoodsIssues != null)
                        {
                            GoodIssuesHelper.DeleteDetail(SelectedArticleDetail);
                            SelectedGoodsIssues.IGE1_GoodsIssueDetail.Remove(SelectedArticleDetail);
                            GoodsIssuesDetails.Remove(SelectedArticleDetail);
                        }

                        //RaisePropertyChanged("GoodsIssuesDetails");

                        IsEnabled = GoodsIssuesDetails.Count == 0;
                    }                    
                },()=>!IsReadOnly);
            }
        }

        public ICommand ProcessCommand
        {
            get { return new RelayCommand(Process, CanExecuteDoProcess); }
        }

        private void Process()
        {
            IsDetailsBusy= IsBusy = true;
             
            if (!SaveDetailsChanges(forceRefreshItemsource:false))
            {
                IsDetailsBusy = IsBusy = false;
                return;
            }
            ViewModelManager.CloseModal();
            ShowProcessLoader(this);

            AsyncHelper.DoAsync(() =>
                                {

                                    Synchronization.Synchronize(SelectedGoodsIssues);
                                    SaveChanges();
                                    ForceRefresh = true;
                                    RaisePropertyChanged("GoodsIssueses");
                                    IsDetailsBusy = IsBusy = false;
                                },ViewModelManager.CloseProcessLoader);                              
        }

        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }
        #endregion

        private bool SaveDetailsChanges(bool forceRefreshItemsource= true)
        {
            
                    if (!ConfirmDialog("Desea Guardar Los Cambios", "Guardar"))
                    {
                        UndoChanges();
                        return false;
                    }

                    var isnotValid = GoodsIssuesDetails.ToList().Any(d =>
                    {
                        if (d.Quantity > d.OnHand)
                        {
                            ErrorMessage =
                                string.Format(
                                    "El Articulo : {0} Codigo {1} ,Quedara en Negativo",
                                    d.Dscription, d.ItemCode);
                            ShowErrorMessageBox(ErrorMessage);
                            return true;
                        }
                        else
                        {
                            ErrorMessage = string.Empty;
                            return false;
                        }
                    });
                    if (isnotValid) return false;
                    if(SelectedGoodsIssues!=null && SelectedGroup !=null&& SelectedMovement !=null)
                        {
                        var needUpdate = GoodIssuesHelper.SaveDetails(SelectedGoodsIssues, SelectedGroup, SelectedMovement);

                        if (needUpdate)GoodsIssuesDetails.ToList().ForEach(d => SelectedGoodsIssues.IGE1_GoodsIssueDetail.Add(d));

                        }

            ViewModelManager.CloseModal();
            if (forceRefreshItemsource)
            {
                ForceRefresh = true;
                RaisePropertyChanged("GoodsIssueses");
            }
            return true;
        }

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("GoodsIssueses");
        }

        private bool isEnabled = true;

        private OITB_Groups InternalSelectedGroup;

        private UMovement InternalSelectedMovement;    
    
        private ArticleChooserViewModel ArticleChooserViewModel; 
       
        private OIGE_GoodsIssues InternalGoodsIssues = new OIGE_GoodsIssues();

        private ObservableCollection<OIGE_GoodsIssues> InternalGoodsIssueses = new ObservableCollection<OIGE_GoodsIssues>();

        private ObservableCollection<IGE1_GoodsIssueDetail> _internalGoodsIssuesDetailsCollection = new ObservableCollection<IGE1_GoodsIssueDetail>();

    }
}
