using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Helpers;

namespace SigiFluent.ViewModels
{
    public class ActionBarViewModel :BaseViewModel
    {

        
        public ActionBarViewModel()
        {
             
            ViewModelManager.OnLoadViewToGrid += OnCurrentViewModelChange;
            StartDate = DateTime.Now.Date;
            LastDate = DateTime.Now.AddDays(1).Date.AddHours(23).AddMinutes(59);
            Synchronization.onSyncItem += RefreshItemSource;
            
        }

        public void OnCurrentViewModelChange(Type viewmodelType)
        {
            //IsvisibilityGroupsFilter = viewmodelType != typeof(GoodsReceiptViewModel);
            
            RefreshItemSource();
        }
       
        public CultureInfo Culture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        #region ACTIONBAR BUTTON COMMANDS

        public ICommand CleanFilterCommand
        {
            get
            {
                return new RelayCommand(()=> { IsCleanFilter = true; });

            }
        }

        private void limpiar()
        {
            SelectedSerie = null;
            SelectedStatus = null;
            SelectedBranch = null;
            selectedgroup = null;
            TextFilter = null;
            StartDate = DateTime.Now.Date;
            LastDate = DateTime.Now.AddDays(1).Date.AddHours(23).AddMinutes(59);
            SelectedStatus = null;
            RaisePropertyChanged("FilterStatus");
            RaisePropertyChanged("SelectedSerie");
            RaisePropertyChanged("FilterStatus");
            RaisePropertyChanged("SelectedBranch");
            RaisePropertyChanged("TextFilter");
            RaisePropertyChanged("StartDate");
            RaisePropertyChanged("LastDate");
            RaisePropertyChanged("SelectedGroup");

            RefreshItemSource();
        }

        public ICommand NewCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            var current = ViewModelManager.GetCurretBaseModel();

                                            if (current != null) current.ExecuteNewCommand();
                                        },
                    () =>
                    {
                        var current = ViewModelManager.GetCurretBaseModel();
                        return current == null || current.CanExecuteNewCommand();
                       
                    }
                                        );
            }
        } 
          
        public ICommand DoProcessCommannd
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            var current = ViewModelManager.GetCurretBaseModel();
                                            if(current!=null) current.ExecuteDoProcess();
                                        }, () =>
                                           {
                                               var current = ViewModelManager.GetCurretBaseModel();
                                               if (current != null) return current.CanExecuteDoProcess();
                                               return true;
                                           });
            }
        }
         
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            var current = ViewModelManager.GetCurretBaseModel();
                                            if(current!=null) current.ExecuteDelete();
                                        }, () =>
                                           {
                                               var current = ViewModelManager.GetCurretBaseModel();
                                               if (current != null) return current.CanExecteDelete();
                                               return true;
                                           });
            }
        } 
       
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) current.ExecuteEdit();
                }, () =>
                   {
                       var current = ViewModelManager.GetCurretBaseModel();
                       if (current != null) return current.CanExecuteEdit();
                       return true;
                   });
            }
        } 

        public ICommand ExportCurrentControl
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            try
                                            {
                                                var gridReport = ViewModelManager.mainWindow;

                                                if (gridReport != null)
                                                {
                                                    PrintHelper.Print(gridReport);
                                                    
                                                   
                                                }
                                                else
                                                {
                                                    throw new ApplicationException("Reporte No encontrado.");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }

                                        });
            }
        }

        //exclusivos de Checkbook (por el momento)
        public ICommand ActivateCommand {
            get
            {
                return new RelayCommand(() =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) current.ExecuteActivation();
                }, () =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) return current.CanExecuteActivation();
                    return true;
                });
            }
        }

        //exclusivo de Checkbook (por el momento)
        public ICommand CancelCommand {
            get {
                return new RelayCommand(() =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) current.ExecuteCancelation();
                }, () =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) return current.CanExecuteCancelation();
                    return true;
                });
            }
        }


        public ICommand NewCreditNoteCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) current.ExecuteNewCreditNote();
                }, () =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) return current.CanExecuteNewCreditNote();
                    return true;
                });
            }
        }
        public ICommand ViewReportCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) current.ViewReport();
                }, () =>
                {
                    var current = ViewModelManager.GetCurretBaseModel();
                    if (current != null) return current.CanViewReport();
                    return true;
                });
            }
        }
        #endregion  ACTIONBAR BUTTON COMMANDS

        #region FILTROS DE GRID

        public bool IsCleanFilter
        {
            get { return _iscleanfilter; }
            set
            {
                _iscleanfilter = value;
                RaisePropertyChanged("IsCleanFilter");
                if (!IsCleanFilter) return;
                limpiar();
                IsCleanFilter = false;
            }
        }

        public string EditViewButtonLabel
        {
            get
            {
                return PropertiesHelper.EntityStatus == LocalStatus.Guardado ? "Editar pedido" : "Ver pedido";
            }
        }

        public string TextFilter
        {
            get { return _textFilter; }
            set
            {
                if (_textFilter != value)
                {
                    _textFilter = value;
                    RaisePropertyChanged("TextFilter");
                    if (!IsCleanFilter) RefreshItemSource();
                }
            }
        }

        public DateTime? StartDate
        {
            get { return _startDate ; }
            set
            {
                if (value == null)
                {
                    MessageBox.Show("Se necestia fecha de referencia");
                    return;
                }

                if (_startDate == value) return;
                _startDate = value.Value.Date;
                RaisePropertyChanged("StartDate");
                if (!IsCleanFilter) RefreshItemSource();
            }
        }

        public DateTime? LastDate
        {
            get { return _lastDate; }
            set
            {
                if (value == null)
                {
                    MessageBox.Show("Se necestia fecha de referencia");
                    return;
                }

                if (_lastDate == value) return;
                _lastDate = value.Value.Date.AddHours(23).AddMinutes(59);
                RaisePropertyChanged("LastDate");
                if (!IsCleanFilter) RefreshItemSource();
            }
        }

        public ObservableCollection<String> Statuses
        {
            get
            {
                var values = Enum.GetValues(typeof (LocalStatus)).Cast<LocalStatus>().Select(stat=> stat.ToString());
                return new ObservableCollection<string>(values);
            }
        }

        public string FilterStatus
        {
            get { return SelectedStatus.HasValue ? SelectedStatus.Value.ToString() : null; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    SelectedStatus = (LocalStatus?) Enum.Parse(typeof (LocalStatus), value);
                }
                else
                {
                    SelectedStatus = null;

                }
                RaisePropertyChanged("FilterStatus");
            }
        }


        public LocalStatus? SelectedStatus
        {
            get { return localStatus; }
            set
            { 
                if (localStatus != value)
                {
                    //localStatus = string.IsNullOrEmpty(value) ? (LocalStatus?)null : (LocalStatus)Enum.Parse(typeof(LocalStatus), value);
                    localStatus = value;
                    if (!IsCleanFilter) RefreshItemSource();
                    
                }
                RaisePropertyChanged("SelectedStatus");
            }
        }

        #region branch

        public OWHS_Branch Branch
        {
            get { return _branch ??(_branch = BranchsHelper.GetBranch(Config.WhsCode,excludeNavigationProperties:true)); }
        }

        public List<OWHS_Branch> BranchStores
        {
            get { return _branchStores ?? (_branchStores = BranchsHelper.GetBranchStores()); }
            set
            {
                if (_branchStores == value) return;
                _branchStores = value;
                RaisePropertyChanged("BranchStores");

            }
        }

        public OWHS_Branch SelectedBranch
        {
            get
            {
                return selectedBranch;
            }
            set
            {
                selectedBranch = value;
                RaisePropertyChanged("SelectedBranch");
                if (!IsCleanFilter) RefreshItemSource();
            }
        }

        #endregion branch

        #region Series

        public string SeriesCode
        {
            get; set;
        }

        public ObservableCollection<NNM1_Series> Series
        {
            get { return series = SeriesHelper.GetSeries(SeriesCode); }
        }

        public NNM1_Series SelectedSerie
        {
            get { return selectedserie; }
            set
            {
                selectedserie = value;
                RaisePropertyChanged("SelectedSerie");
                if (!IsCleanFilter) RefreshItemSource();
            }
        }

        #endregion series

        #region Report Types

        public ObservableCollection<ReportMapping> ReportTypes
        {
            get
            {
                return mappnigCollection ?? (mappnigCollection = ReportMappingRepository.GetReportCollection());                
            }
            set
            {
                mappnigCollection = value;
                RaisePropertyChanged("ReportTypes");
            }
        }

        public ReportMapping ReportType
        {
            get { return reportType; }
            set
            {
                reportType = value;
                if (reportType != null) IsvisibilityGroupsFilter = reportType.ParameterType == 1;
                RaisePropertyChanged("ReportType");
            }
        }
        #endregion

        public   void RefreshItemSource()
        {
            // TODO : Refresh with a new Query.
            var current = ViewModelManager.GetCurretBaseModel();
            var warehouseCode = selectedBranch != null && selectedBranch.WhsCode != null
                ? SelectedBranch.WhsCode
                : string.Empty;
            if (current == null) return;
            
            this.Keyword = TextFilter;
            current.Keyword = TextFilter;
            current.SelectedSerie = this.SelectedSerie;
            current.localStatus = localStatus;
            current.WarehouseCode = warehouseCode;
            current.StartDate = _startDate;
            current.LastDate = _lastDate;
            current.ForceRefresh = true;
            current.ABSelectedGroup = selectedgroup;
            current.RefreshItemSource();
        }

        public bool IsvisibilityGroupsFilter
        {
            get
            {                
                return IsVisibilityddl;
            }
            set { IsVisibilityddl = value; RaisePropertyChanged("IsvisibilityGroupsFilter"); }
        }



        #endregion FILTROS DE GRID

        //Colections

        
      
        #region movement

        public ObservableCollection<UMovement> GoodReceitpMovement
        {
            get { return movements ?? (movements = UMovementHelper.GetMovementsCollection("E")); }
        }

        public UMovement SelectedReceiptMovement
        {
            get { return selectedmovement; }
            set
            {
                selectedmovement = value;
                RaisePropertyChanged("SelectedReceiptMovement");
                RefreshItemSource();
            }
        }

        public ObservableCollection<UMovement> GoodIssuesMovement
        {
            get { return movements ?? (movements = UMovementHelper.GetMovementsCollection("S")); }
        }

        public UMovement SelectedIssuesMovement
        {
            get { return selectedmovement; }
            set
            {
                selectedmovement = value;
                RaisePropertyChanged("SelectedIssuesMovement");
                RefreshItemSource();
            }
        }

        #endregion movement

        #region Groups

        public ObservableCollection<OITB_Groups> Groups
        {
            get { return groups ?? (groups = BranchsHelper.GetGroups()); }
        }

        public OITB_Groups SelectedGroup
        {
            get { return selectedgroup; }
            set
            {
                selectedgroup = value;
                RaisePropertyChanged("SelectedGroup");
                RefreshItemSource();
            }
        }

        #endregion Groups

        #region PRIVATE PROPERTIES


        private DateTime? _startDate;
        private DateTime? _lastDate;
        private string _textFilter;
        
        private OWHS_Branch _branch;
        private List<OWHS_Branch> _branchStores;
       
        private OWHS_Branch selectedBranch;

        private ObservableCollection<NNM1_Series> series;
        private NNM1_Series selectedserie;
        private ObservableCollection<UMovement> movements;
        private UMovement selectedmovement;
        private ObservableCollection<OITB_Groups> groups;
        private OITB_Groups selectedgroup;
        private bool isFilterByWarehouse;
        private bool IsVisibilityddl;
        private bool _iscleanfilter;
        private string _seriesCode;

        private ReportMapping reportType;
        private ObservableCollection<ReportMapping> mappnigCollection; 

        #endregion PRIVATE PROPERTIES

    }
}
