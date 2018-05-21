using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;
using System.Windows;
//using SigiFluent.Views.Reports;
using SigiFluent.Views.UserControls;

namespace SigiFluent.ViewModels
{    
    public class GoodsReceiptViewModel : BaseViewModel
    {
        
        public bool IsEnabled 
        {
            get 
            {
                return _isEnabledFirstSelect;
            }
            set 
            {
                _isEnabledFirstSelect = value;
                RaisePropertyChanged("IsEnabled");
            }
        }
        public ObservableCollection<OIGN_GoodsReceipt> GoodsReceipts
        {
            get
            {
                IsBusy = true;
                if (InternalGoodsReceipts.Count == 0 || ForceRefresh)
                {
                    var groupId =ABSelectedGroup == null ? (short)-1 : ABSelectedGroup.ItmsGrpCod;

                    InternalGoodsReceipts = GoodsReceiptHelper.GetGoodsReceipts(StartDate, 
                                                                                LastDate,
                                                                                groupId, Keyword, localStatus);
                    ForceRefresh = false;
                    IsBusy = false;                    
                }
                IsBusy = false;
                return InternalGoodsReceipts;
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
            get { return SelectedGoodsReceipt != null &&
                         SelectedGoodsReceipt.DocDueDate.HasValue
                         ? SelectedGoodsReceipt.DocDueDate.Value : DateTime.Now;
            }
            set 
            {
                if (SelectedGoodsReceipt != null)
                    SelectedGoodsReceipt.DocDueDate = value;

                RaisePropertyChanged("DocDueDate"); 
            }
        }

        public string DocComments
        {
            get
            {
                return SelectedGoodsReceipt != null
                       ? SelectedGoodsReceipt.Comments : "";
            }
            set
            {
                if (SelectedGoodsReceipt != null)
                    SelectedGoodsReceipt.Comments = value;

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

                InternalMovementTypes = UMovementHelper.GetMovementsCollection("E");
                
                return InternalMovementTypes;
            }

            set 
            {
                InternalMovementTypes = value;
                RaisePropertyChanged("MovementTypes");
            }
   

        }

        public ObservableCollection<IGN1_GoodsReceiptDetail> GoodsReceiptsDetails
        {
            get
            {
                //if (SelectedGoodsReceipt != null)
                //{
                //    IsDetailsBusy = true;
                //    InternalGoodsReceiptsDetailsCollection = new ObservableCollection<IGN1_GoodsReceiptDetail>(SelectedGoodsReceipt.IGN1_GoodsReceiptDetail);
                //}
                //IsDetailsBusy = false;
                return InternalGoodsReceiptsDetailsCollection;
            }
            set
            {
                InternalGoodsReceiptsDetailsCollection = value;
                RaisePropertyChanged("GoodsReceiptsDetails");
            }
        }

        public OIGN_GoodsReceipt SelectedGoodsReceipt
        {
            get { return InternalGoodsReceipt; }
            set 
            { 
                InternalGoodsReceipt = value;

                RaisePropertyChanged("SelectedGoodsReceipt"); 
                
                //if (SelectedGoodsReceipt.Movement != null)
                //    SelectedMovement = SelectedGoodsReceipt.Movement;

                //if (SelectedGoodsReceipt.Group != null)
                //    SelectedGroup = SelectedGoodsReceipt.Group;

                
            }
        }

        public IGN1_GoodsReceiptDetail SelectedArticleDetail { get; set; }

        public bool IsReadOnly
        {
            get { return SelectedGoodsReceipt != null && SelectedGoodsReceipt.StateL != LocalStatus.Guardado; }
        }

        public bool IsEnabledForReadOnly
        {
            get { return !IsReadOnly; }
        }

      
        public GoodsReceiptViewModel()
        {
        
        }

        #region ToolBarz
        public override void ExecuteNewCommand()
        {            
            FormTitle = "Nueva Entrada";

            SelectedGoodsReceipt = new OIGN_GoodsReceipt();
            SelectedGroup = null;
            SelectedMovement = null;
            GoodsReceiptsDetails = new ObservableCollection<IGN1_GoodsReceiptDetail>();
            IsEnabled = true;
            ShowDialog(new GoodsReceiptView(), this, resizeMode: ResizeMode.CanResize);
        }

        public override void ExecuteEdit()
        {
            FormTitle = "Entrada " + SelectedGoodsReceipt.DocNum.ToString();

            GoodsReceiptsDetails = null;
            GoodsReceiptsDetails = new ObservableCollection<IGN1_GoodsReceiptDetail>(SelectedGoodsReceipt.IGN1_GoodsReceiptDetail);

            IsEnabled = GoodsReceiptsDetails.Count == 0;

            if(!string.IsNullOrEmpty(SelectedGoodsReceipt.IdMovement)) SelectedMovement = MovementTypes.FirstOrDefault(m => m.Code == SelectedGoodsReceipt.IdMovement );

            if(SelectedGoodsReceipt.ItmsGrpCod.HasValue) SelectedGroup = Groups.FirstOrDefault(g => g.ItmsGrpCod == SelectedGoodsReceipt.ItmsGrpCod);

            ShowDialog(new GoodsReceiptView(), this);
        }

        public override bool CanExecuteEdit()
        {
            return SelectedGoodsReceipt!=null &&!IsBusy;
        }

        public override void ExecuteDelete()
        {
            if (SelectedGoodsReceipt != null && ConfirmDelete())
            {
                GoodsReceiptHelper.DeleteGoodsReceipt(SelectedGoodsReceipt);
                SaveChanges();
                GoodsReceipts.Remove(SelectedGoodsReceipt);
            }
        }

        public override bool CanExecteDelete()
        {
            return !IsReadOnly && !IsBusy;
        }

        public override bool CanExecuteDoProcess()
        {
            return !IsReadOnly && !IsBusy;
        }

        public override bool CanViewReport()
        {
            return SelectedGoodsReceipt != null && SelectedGoodsReceipt.IdGoodReceiptL!=0;
        }

        public override void ExecuteDoProcess()
        {
            Process();
        }

        public override void ViewReport()
        {
            var param = new Dictionary<string,string>();

            param.Add("@id",SelectedGoodsReceipt.IdGoodReceiptL.ToString());

            var data = StoredCallbackProcessor.CallDataSet("SP_INV_REP_AJUSTES_E",param);
            //var report = new ReportContainer("rptAjustedeInventarios.rpt", data);
            //ShowDialog (report,this);
        }

        #endregion

        #region GoodReceiptViewDialog

        public ICommand SaveDetailsCommand
        {
            get
            {
                return new RelayCommand(()=>SaveNewChanges() );
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

                                                
                                            },()=> !IsReadOnly);
                }
            
        }

        void articleChooserViewModel_OnSelect(OITM_Articles obj)
        {
            IsModalVisible = false;
            
            var branch = obj.OITW_BranchArticles.FirstOrDefault();
            //var OnHand1 = branch == null ? 0 : branch.OnHand1;
          
            var detail = new IGN1_GoodsReceiptDetail()
            {
                ItemCode = obj.ItemCode,                
                Quantity = ArticleChooserViewModel.Quantity,       
                UnitMsr = obj.InvntryUom,  
                AcctCode = obj.AccCount,
                //OITM_Articles = obj
                Dscription = obj.ItemName,
            };


            ArticleChooserViewModel.CleanFields();
            
            GoodsReceiptsDetails.Add(detail);

            if(SelectedGoodsReceipt!=null) SelectedGoodsReceipt.IGN1_GoodsReceiptDetail.Add(detail);
             
            IsEnabled = GoodsReceiptsDetails.Count == 0;
            RaisePropertyChanged("GoodsReceiptsDetails");
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

                    if (SelectedArticleDetail != null && ConfirmDelete() && SelectedGoodsReceipt.StateL != LocalStatus.Procesado) 
                    {
                        GoodsReceiptsDetails.Remove(SelectedArticleDetail);

                        if (SelectedGoodsReceipt != null)
                        {
                            GoodsReceiptHelper.DeleteDetail(SelectedArticleDetail);
                            //SelectedGoodsReceipt.IGN1_GoodsReceiptDetail.Remove(SelectedArticleDetail);
                        }

                        //RaisePropertyChanged("GoodsReceiptsDetails");

                        IsEnabled = GoodsReceiptsDetails.Count == 0;
                    }                    
                },()=> !IsReadOnly );
            }
        }

        public ICommand ProcessCommand
        {
            get { return new RelayCommand(Process, CanExecuteDoProcess); }
        }

        public void SaveNewChanges(bool isAsync=false)
        {
            if (!isAsync)
            {
                if (!ConfirmDialog("Desea Guardar Los Cambios", "Guardar"))
                {
                    UndoChanges();
                    return;
                }
            }

            var needUpdate = GoodsReceiptHelper.SaveNewDetailsCommand(SelectedGoodsReceipt, SelectedGroup, SelectedMovement);

            if (needUpdate)
            {
                GoodsReceiptsDetails.ToList().ForEach(d => SelectedGoodsReceipt.IGN1_GoodsReceiptDetail.Add(d));
            }

            if (!isAsync)
            {
                ViewModelManager.CloseModal();
                ForceRefresh = true;
                RaisePropertyChanged("GoodsReceipts");
            }
        }


        private void Process()
        {
            IsBusy = true;

            if (SelectedGoodsReceipt == null || !ConfirmDialog("Confirma que desea procesar", "Confimar"))
            {
                UndoChanges();
                return;
            }
            ViewModelManager.CloseModal();
            ShowProcessLoader(this);
            AsyncHelper.DoAsync(() =>
                                {
                                    GoodsReceiptHelper.SaveNewDetailsCommand(SelectedGoodsReceipt, SelectedGroup, SelectedMovement);
                                    SaveChanges();                                    
                                    Synchronization.Synchronize(SelectedGoodsReceipt);                                  
                                    ForceRefresh = true;
                                    RaisePropertyChanged("GoodsReceipts");
                                    IsBusy = false;
                                },ViewModelManager.CloseProcessLoader);
            
        }

        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }

        

        #endregion

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("GoodsReceipts");
        }

        private bool _isEnabledFirstSelect = true;

        private OITB_Groups InternalSelectedGroup;

        private UMovement InternalSelectedMovement;    
    
        private ArticleChooserViewModel ArticleChooserViewModel; 
       
        private OIGN_GoodsReceipt InternalGoodsReceipt = new OIGN_GoodsReceipt();

        private ObservableCollection<OIGN_GoodsReceipt> InternalGoodsReceipts = new ObservableCollection<OIGN_GoodsReceipt>();

        private ObservableCollection<IGN1_GoodsReceiptDetail> InternalGoodsReceiptsDetailsCollection = new ObservableCollection<IGN1_GoodsReceiptDetail>();

    }
}
