using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
//using System.Data.Entity.Core.Objects.DataClasses;
using System.Deployment.Application;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
 
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using StockAdminContext.WebServices;
using SigiFluent.Helpers;
using SigiFluent.Views;
using SigiFluent.Views.ActionBars;
using SigiFluent.Views.Templates;


namespace SigiFluent.ViewModels
{

    /// <summary>
    /// Tool Bar View Model = MainViewModel .
    /// </summary>
    public class ToolBarViewModel: BaseModel
    {
        public ToolBarViewModel()
        {
            IsVisibleStartPage = true;
            IsLoading = false;
            
            Synchronization.onReportProgress += ReportProgres;
            Synchronization.onSyncComplete += OnCompleteSync;
            Synchronization.OnBeginSync += () => IsRunningSync = true;

            MainWindow.OnApplicationLoaded += OnApplicationLoaded;
            WebApiClient.OnWebApiError += ShowError;
            Synchronization.onConnectionError += ShowError;
            StoredCallbackProcessor.OnUpdate += ForceRefreshNotifications;
            TransfersViewModel.OnUpdateNotifications += ForceRefreshNotifications;
            TransferRequestViewModel.OnUpdateNotifications += ForceRefreshNotifications;
            AsyncHelper.onException += ShowError;
            
            SetIconConnection();
            ShowToolBarRegion();

        }

        public ObservableCollection<ToolMenuItem> ToolMenuCollection
        {
            get { return menu; }
            set { menu =value; RaisePropertyChanged("ToolMenuCollection"); }
        }

        public bool IsVisibleStartPage
        {
            get
            {
                return isVisibleStartPage;
            }
            set
            {
                isVisibleStartPage = value;
                RaisePropertyChanged("IsVisibleStartPage");
            }
        }
 
         
        public ICommand CmdLaunchProductStock {
            get 
            {
                return new RelayCommand(() =>
                                        {
                                            var model = ViewModelManager.GetViewModel <StockTempalteViewModel, ArticlesInventoryView>();
                                            model.ForceRefresh = true;
                                            ViewModelManager.ShowModal<StockTempalteViewModel, ArticlesInventoryView>(model);
                                            //ViewModelManager.ShowModal<StockTempalteViewModel, ArticlesInventoryView>();

                                        });      
            }
        }

        public ICommand CmdLaunchArticlesALOHA
        {
            get
            {
                return new RelayCommand(()=>
                                        {
                                            var model =ViewModelManager.GetViewModel<ArticlesAlohaViewModel, ArticlesAlohaView>();
                                            model.ForceRefresh = true;
                                            ViewModelManager.ShowModal<ArticlesAlohaViewModel, ArticlesAlohaView>(model);
                });
            }
        }

        public ICommand AccountAdminCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            var vm = ViewModelManager.GetViewModel<UsersViewModel, UsersGridView>();
                                            var view = ViewModelManager.GetView<UsersViewModel, UsersGridView>();
                                            ViewModelManager.CleanGrid();
                                            
                                             ((BaseViewModel) vm).ShowDialog(view,vm);
                                        }, () => Config.CurrentUser.ToLower().Contains("admin"));
            }
        }

        public WindowState CurrentWindowState
        {
            get
            {
                return _curWindowState;
            }
            set
            {
                _curWindowState = value;
                RaisePropertyChanged("CurWindowState");
                ViewModelManager.mainWindow.WindowState = _curWindowState;
            }
            }
        public ICommand CommandMinimice
        {
            get { return new RelayCommand(() => CurrentWindowState = WindowState.Minimized); }
        }
        
        
        public ICommand CloseApplicationCommand
        {
            get { return new RelayCommand(()=> ViewModelManager.mainWindow.Close());}
        }

        private void ForceRefreshNotifications()
        {
            var notifications = StartPageNotifications.ToList();

            notifications.ForEach(n =>
                                  {
                                      System.Threading.Thread.Sleep( TimeSpan.FromSeconds(1) );
                                      StartPageNotifications.Remove(n);
                                  });

            RaisePropertyChanged("StartPageNotifications");
        }
       
        #region StartPage Commands & Properties

        public string BranchCode
        {
            get { return Config.WhsCode; }
        }

        public string UserName
        {
            get { return userName??string.Empty; } set { userName = value; RaisePropertyChanged("UserName");}
        }

        public bool IsLoggedIn
        {
            get { return isloggedin; }
            set { isloggedin = value;RaisePropertyChanged("IsloggedIn"); }
        }

        public bool HasErrorLogin
        {
            get { return hasErrorOnLogin; }
            set { hasErrorOnLogin = value;RaisePropertyChanged("HasErrorLogin"); }
        }

        public DateTime StartSessionTime
        {
            get { return startSession ; }
            set { startSession = value; RaisePropertyChanged("StartSessionTime"); }
        }

        public DateTime LastTimeSession
        {
            get { return lastSessinotime; }
            set { startSession = value; RaisePropertyChanged("LastTimeSession"); }
        }

        public string Version
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return string.Format("Version del Software: {0}", ApplicationDeployment.CurrentDeployment.CurrentVersion);
                }
                    
                else return  "Versión 2.5";
            }
        }

        public ObservableCollection<NotificationItemMenu> StartPageNotifications
        {
            get { return NotificationsManager.GetNotifications(); }
            //set { notifications = value; RaisePropertyChanged("StartPageNotifications"); }
        }
        public ObservableCollection<NotificationItemMenu> EndOfDayNotifications
        {
            get { return _endOfDayNotifications; }
            set { _endOfDayNotifications = value; RaisePropertyChanged("EndOfDayNotifications"); }
        }
       

        public  bool IsRunningSync
        {
            get
            {
                return _isRunningSync;
            }
            set
            {
                _isRunningSync= value;
                RaisePropertyChanged("IsRunningSync");
            }
        }

        public double ProgressValue
        {
            get { return _progressvalue; }
            set
            {
                _progressvalue = value;
                RaisePropertyChanged("ProgressValue");
            }
        } 
        
        public ICommand GotoSupplyRegionCommand
        {
            get
            {
                return  new RelayCommand(() =>
                                         {
                                             IsVisibleStartPage = false;
                                             ShowToolBarRegion(ToolGroupRegion.Supply);
                                             LoadTransferRequest();

                                         });
            }
        }

        public ICommand GotoPurchaseRegionCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            IsVisibleStartPage = false;
                                            ViewModelManager.LoadActionBar(new PurchasesBar(), "18");
                                            ShowToolBarRegion(ToolGroupRegion.Purchases);
                                            ViewModelManager.LoadToGrid<PurchaseViewModel, PurchaseView>();
                                        });
            }
        }        

        public ICommand GotoSellRegionCommand
        {
            get { return new RelayCommand(() =>
                                          {
                                              IsVisibleStartPage = false;
                                              ViewModelManager.LoadActionBar(new SellsBar(), "13");
                                              ShowToolBarRegion(ToolGroupRegion.Sales);
                                              ViewModelManager.LoadToGrid<SaleViewModel,SaleGridView>();
                                          });}
        }

        public ICommand GotoInventoryRegionCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            IsVisibleStartPage = false;
                                            ViewModelManager.LoadActionBar(new GoodReceiptBar());
                                            ViewModelManager.ActionBarViewModel.IsvisibilityGroupsFilter = true;
                                            ShowToolBarRegion(ToolGroupRegion.Inventory);
                                            ViewModelManager.LoadToGrid<GoodsReceiptViewModel,GoodReceiptGridView>();
                                        });
            }
        }

        public ICommand GotoCheckBookRegionCommand
        {
            get
            {
                return  new RelayCommand(() =>
                {
                    IsVisibleStartPage = false;
                    ViewModelManager.LoadActionBar(new CheckBookBar());
                    ShowToolBarRegion(ToolGroupRegion.CheckBook);
                    ViewModelManager.LoadToGrid<CheckBookViewModel, CheckbookGridView>();
                });
            }
        }

        public ICommand GoToReportsCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            IsVisibleStartPage = false;
                                            ViewModelManager.LoadActionBar(new ReportsBar());
                                            ShowToolBarRegion(ToolGroupRegion.Reports);
                                            ViewModelManager.LoadToGrid<ReportsViewModel,ReportsContainerView>();
                                        });
            }
        }

        public ICommand PowerOffCommand
        {
            get
            {
                return new RelayCommand(RunEndOfDay); 
            }
        }        

        public ICommand LoginCommand
        {
            get {return new RelayCommand<PasswordBox>((pass) =>
                                                      {
                                                          
                                                          //IsLoggedIn = true;
                                                          
                                                          HasErrorLogin =!(IsLoggedIn= Membership.Login(UserName, pass.Password));
                                                          if (isloggedin) StartSessionTime = DateTime.Now;
                                                          
                                                      });}
        }
         
        public ICommand LogoutCommand
        {
            get { return new RelayCommand<PasswordBox>((pass) =>
                                          {
                                              IsLoggedIn = false;
                                              if (OnLogout != null) OnLogout();
                                              UserName = string.Empty;
                                          });}
        }

        public ICommand GotoStartPageCommand
        {
            get { return new RelayCommand(() => IsVisibleStartPage = true); }
        }

        public ICommand CloseMainModalCommand
        {
            get { return new RelayCommand(ViewModelManager.CloseModal,ViewModelManager.CanCloseMainModal);}
        }

        public ICommand ForceCloseModalCommand
        {
            get { return new RelayCommand(ViewModelManager.CloseModal,()=> !IsLockedForEndOfDay);}
        }

        public ICommand SynchronizeCommand
        {
            get { return new RelayCommand(() =>
                                          {
                                              IsRunningSync = WebApiClient.IsAvailableConnection;
                                              SyncText = "Sincronizando...";
                                              AsyncHelper.DoAsync(() => Synchronization.CheckForUpdates(), () => RaisePropertyChanged("IsRunningSync"));
                                              
                                          });
            }
        }

        public ICommand RunAlohaSyncCommand
        {
            get
            {
                return new RelayCommand(ForceAlohaSync );
            }
        }

        private void LoadTransferRequest()
        {
            //IsLoading = true;
            //BusyContent = "Cargando...";
            IsVisibleStartPage = false;
            ViewModelManager.LoadActionBar(new TransferRequestBar());
            ViewModelManager.LoadToGrid<TransferRequestViewModel, TransferRequestsView>();

        }

        

       
        #endregion

        #region Fin De Dia

        public void ForceAlohaSync()
        {
            
            lock (Extensions.SyncLock)
            {
                BusyContent = string.Empty;
                AsyncHelper.DoAsync(() => 
                {
                    IsRunningSync = true;
                    StoredCallbackProcessor.StartAlohaSync(ignoreAsync: true); 
                    IsRunningSync = false;
                });

            }

           
        }


        public void RunEndOfDay()
        {
            EndOfDayNotifications = null;

            ViewModelManager.ShowModal(new DayToFinish(), new Thickness(350, 100, 350, 100));

            if (System.Windows.MessageBox.Show(ViewModelManager.mainWindow, "Desea Ejecutar Fin De Dia", "FIN DE DIA",
                System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes) AsyncHelper.DoAsync(RunEndOfDayAsync);
            
            else ViewModelManager.CloseModal();
        }

       

        public void RunEndOfDayAsync()
        {
            ProgressValue = 0;
            
            if (!WebApiClient.IsAvailableConnection)
            {
                BusyContent = "Error de Conneccion Intente nuevamente ..";
                return ;
            }

            BusyContent = "Ejectutando Validacion de Fin de Dia";
            IsLockedForEndOfDay= IsRunningEndOfDay = true;
            EndOfDayNotifications = NotificationsManager.GetEndOfDayNotifications();
            IsRunningEndOfDay = false;
            IsLoading = true;
            if (EndOfDayNotifications.Any(n => n.NotificationName.ToLower() != "completo"))
            {
                var error = BusyContent = "Error Existen Validaciones Pendientes, Favor verificar";
                MessageBox.Show(error, "Fin de dia Error", MessageBoxButton.OK, MessageBoxImage.Error);
                 IsLoading = IsLockedForEndOfDay = false;
                 return;
            }

            BusyContent = "Sincronizando Productos Aloha...";
            StoredCallbackProcessor.StartAlohaSync(ignoreAsync: true);
          
            BusyContent = "Procesando Venta Pendiente...";
            var idVenta = (int)StoredCallbackProcessor.CallDataSet("SP_INV_VNT_PROCESAR").Tables[0].Rows[0].ItemArray.FirstOrDefault();

            if (idVenta <= 0)
            {
                MessageBox.Show("Error al procesar venta", "Fin de dia Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                 IsLoading = IsLockedForEndOfDay = false;
            }
            else
            {
                var sale = SalesHelper.Get(idVenta);

                //Asiganado las formas de pago para la venta por ALOHA
                var pagos = SalesHelper.GetiInvoicePayments(sale);
                sale.paymentsAloha = pagos;

                if (sale != null)
                {
                    BusyContent = "Sincronizando Venta...";
                    Synchronization.Synchronize(sale);
                    if (sale.StateL == LocalStatus.Procesado)
                    {
                        var token = TransactionLogHelper.ConfirmationLog();
                        BusyContent = "Proceso completado con exito, Codigo de verificacion " + token;
                        MessageBox.Show(BusyContent, "Finalizado", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else 
                    {
                        BusyContent = "Error al procesar venta, Verifique ..";
                        ProgressValue = 0;
                        MessageBox.Show(BusyContent, "Error Fin de Dia", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        IsLoading = IsLockedForEndOfDay = false;
                    }
                }
                else
                {
                    BusyContent = "Error al procesar venta, Verifique ..";
                    ProgressValue = 0;
                    MessageBox.Show(BusyContent, "Error Fin de Dia", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                     IsLoading = IsLockedForEndOfDay = false;
                }
            }
              IsLoading = IsLockedForEndOfDay = false;
            
        }

        #endregion 


        #region CreateRegionDefinitions

        public List<ToolMenuItem> GetToolBarItems()
        {
            var menuItems = new List<ToolMenuItem>();

           // menuItems.Add(new ToolMenuItem("INICIO", "", new RelayCommand(() => IsVisibleStartPage = true), ToolGroupRegion.Supply));
            menuItems.Add(new ToolMenuItem("SOLICITUD DE TRASLADO", "SUPPLYGROUP", new RelayCommand(LoadTransferRequest)
                                                                                   , isChecked: true, region: ToolGroupRegion.Supply));
            menuItems.Add(new ToolMenuItem("PEDIDOS ESPECIALES", "SUPPLYGROUP", new RelayCommand(() =>
                                                                                                 {
                                                                                                     ViewModelManager.LoadActionBar(new SpecialOrderBar());
                                                                                                     ViewModelManager.LoadToGrid<SpecialOrderViewModel, SpecialOrdersGridView>();
                                                                                                 }),region:ToolGroupRegion.Supply));
            menuItems.Add(new ToolMenuItem("ENTREGAS", "SUPPLYGROUP", new RelayCommand(() =>
            {
                ViewModelManager.LoadActionBar(new TransferBar());
                ViewModelManager.LoadToGrid<TransfersViewModel, TransfersGridView>();
            }), region: ToolGroupRegion.Supply));

            // set isChecked cuando es el unico elemento en la toolbar o es seleccionado por defecto,
            menuItems.Add(new ToolMenuItem("COMPRAS", "", new RelayCommand(() =>
                                                                          {
                                                                              IsVisibleStartPage = false;
                                                                              ViewModelManager.LoadActionBar(new PurchasesBar(),"18");
                                                                              ViewModelManager.LoadToGrid<PurchaseViewModel, PurchaseView>();
                                                                          }),ToolGroupRegion.Purchases,isChecked:true ));

            menuItems.Add(new ToolMenuItem("NOTAS DE CREDITO", "", new RelayCommand(() =>
            {
                IsVisibleStartPage = false;
                ViewModelManager.LoadActionBar(new SupplierCreditNoteBar());
                ViewModelManager.LoadToGrid<SupplierCreditNoteViewModel, SupplierCreditNotesView>();
            }), ToolGroupRegion.Purchases));


            menuItems.Add(new ToolMenuItem("VENTAS", "", new RelayCommand(() =>
                                                                          {
                                                                              IsVisibleStartPage = false;
                                                                              ViewModelManager.LoadActionBar(new SellsBar(), "13");
                                                                              ViewModelManager.LoadToGrid<SaleViewModel, SaleGridView>();
                                                                          }), ToolGroupRegion.Sales, isChecked: true));

            menuItems.Add(new ToolMenuItem("NOTAS DE CREDITO DE VENTAS", "", new RelayCommand(() =>
            {
                IsVisibleStartPage = false;
                ViewModelManager.LoadActionBar(new ClientCreditNotesBar(), "14");
                ViewModelManager.LoadToGrid<ClientCreditNoteViewModel, ClientCreditNotesView>();
            }), ToolGroupRegion.Sales));
           

            menuItems.Add(new ToolMenuItem("ANTICIPOS","",new RelayCommand(() =>
                                                                           {
                                                                               ViewModelManager.LoadActionBar(new DownPaymentBar(), "203");
                                                                               ViewModelManager.LoadToGrid<DownPaymentViewModel,DownPaymentGridView>();
                                                                           }), ToolGroupRegion.Sales));
            menuItems.Add(new ToolMenuItem("DEPOSITOS", "", new RelayCommand(() =>
                                                                             {
                                                                                 ViewModelManager.LoadActionBar(new DepositBar());
                                                                                 ViewModelManager.LoadToGrid<DepositViewModel,DepositGridView>();

                                                                             }), ToolGroupRegion.Sales));
            //menuItems.Add(new ToolMenuItem("CAJA CHICA", "", new RelayCommand(() => { }), ToolGroupRegion.Sells));

            menuItems.Add(new ToolMenuItem("FALTANTE DE EFECTIVO", "", new RelayCommand(() =>
                                                                                        {
                                                                                            ViewModelManager.LoadActionBar(new CashMissingBar() );
                                                                                            ViewModelManager.LoadToGrid<CashMissingViewModel, CashMissingGridView>();
                                                                                        }), ToolGroupRegion.Sales));

            menuItems.Add(new ToolMenuItem("ENTRADAS", "", new RelayCommand(() =>
            {
                IsVisibleStartPage = false;
                ViewModelManager.LoadActionBar(new GoodReceiptBar());
                ViewModelManager.ActionBarViewModel.IsvisibilityGroupsFilter = true;
                ShowToolBarRegion(ToolGroupRegion.Inventory);
                ViewModelManager.LoadToGrid<GoodsReceiptViewModel, GoodReceiptGridView>();
            }), ToolGroupRegion.Inventory, isChecked: true));

            menuItems.Add(new ToolMenuItem("SALIDAS", "", new RelayCommand(() =>
            {
                IsVisibleStartPage = false;
                ViewModelManager.LoadActionBar(new GoodIssuesBar());
                ViewModelManager.ActionBarViewModel.IsvisibilityGroupsFilter = true;
               
                ViewModelManager.LoadToGrid<GoodsIssuesViewModel, GoodIssuesGridView>();
            }), ToolGroupRegion.Inventory));

            menuItems.Add(new ToolMenuItem("CONTEO FISICO", "", new RelayCommand(() =>
            {
                IsVisibleStartPage = false;
                ViewModelManager.ActionBarViewModel.IsvisibilityGroupsFilter = true;
                ViewModelManager.LoadActionBar(new InventoryCountBar());
                ViewModelManager.LoadToGrid<InventoryCountViewModel,InventoryCountGridView>();
            }), ToolGroupRegion.Inventory));


            menuItems.Add(new ToolMenuItem("TALONARIOS","", new RelayCommand(() =>
            {
                isVisibleStartPage = false;
                ViewModelManager.LoadActionBar(new CheckBookBar());
                ViewModelManager.LoadToGrid<CheckBookViewModel, CheckbookGridView>();
            }), ToolGroupRegion.CheckBook, isChecked: true));

            menuItems.Add(new ToolMenuItem("EXCEPCIONES", "", new RelayCommand(() => 
            {
                
                isVisibleStartPage = false;
                ViewModelManager.LoadActionBar(new CanceledCheckBookBar());
                
                ViewModelManager.LoadToGrid<CanceledCheckBookViewModel, CanceledCheckBookGridView>();

            }), ToolGroupRegion.CheckBook));

            //menuItems.Add(new ToolMenuItem("REPORTES", "", new RelayCommand(() =>
            //{

            //    isVisibleStartPage = false;
            //    ViewModelManager.LoadActionBar(new ReportsBar());

            //    //ViewModelManager.LoadToGrid<object,>();

            //}), ToolGroupRegion.CheckBook));
            
            return   menuItems;

        }

        public void ShowToolBarRegion(ToolGroupRegion? region =null)
        {
            var menuItems = region.HasValue ? GetToolBarItems().Where(t => t.Region == region).ToList() : GetToolBarItems();

            ToolMenuCollection = new ObservableCollection<ToolMenuItem>(menuItems);
        }

        #endregion 

        #region IconConnectionStatus

        public string IconConnectionStatusPath
        {
            get { return connectionIconStatus; }
            set { connectionIconStatus = value; RaisePropertyChanged("IconConnectionStatusPath"); }
        }

        public string LabelStatus
        {
            get { return connectionLabelStatus; }
            set { connectionLabelStatus = value; RaisePropertyChanged("LabelStatus"); }
        }

        private void SetIconConnection()
        {
            SetCurrenStatus();

            ConnectionBridge.OnError += () => UpdateConnectionStatus("remove-30", "Error de coneccion.");

            ConnectionBridge.OnReconnected += () => UpdateConnectionStatus("check-30", "Conectado");

            ConnectionBridge.OnReconnecting += () => UpdateConnectionStatus("check-30", "Conectado.");
            
            ConnectionBridge.OnConnectionSlow += () => UpdateConnectionStatus("check-30", "Conectado..");

            ConnectionBridge.OnConnectionClosed += () =>UpdateConnectionStatus("remove-30", "Sin Conexion.");

            ConnectionBridge.OnStateChanged += (stat)=> SetCurrenStatus();
        }

        private void UpdateConnectionStatus(string iconname, string tooltip)
        {
            IconConnectionStatusPath = ApplicationPath.GetPathByFileNameFromResources(iconname);
            LabelStatus = tooltip;
             
        }


        private void SetCurrenStatus()
        {
            var connection = ConnectionBridge.StateConnectionString();
            switch (connection)
            
            {
                case "Connecting": UpdateConnectionStatus("check-30", "Conectado..");
                    break;
                case "Connected": UpdateConnectionStatus("check-30","Conectado");
                    break;
                case "Reconnecting": UpdateConnectionStatus("check-30", "Conectado...");
                    break;
                case "Disconnected":UpdateConnectionStatus("remove-30","Sinconeccion");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Loader /Busy

        public bool IsLoading
        {
            get
            {
                return IsBusy;
            }
            set
            {
                IsBusy = value;
                RaisePropertyChanged("IsLoading");
            }
        }

       

        public string BusyContent
        {
            get
            {
                return busyConntent;
            }
            set
            {
                busyConntent = value;
                RaisePropertyChanged("BusyContent");
            }
        }

        public bool IsRunningEndOfDay
        {
            get
            {
                return _isRunningEndOfDay;
            }
            set
            {
                _isRunningEndOfDay = value; RaisePropertyChanged("IsRunningEndOfDay");
            }
        }

        public bool IsLockedForEndOfDay
        {
            get
            {
                return _isLockedForEndOfDay;
            }
            set
            {
                _isLockedForEndOfDay = value;
                ViewModelManager.IsLockedModal = value;
                RaisePropertyChanged("IsLockedForEndOfDay");
            }
        }

        #endregion
 
         
        #region Private Properties
        private WindowState _curWindowState;
       

        private bool isVisibleStartPage;
       
        
        private string busyConntent= string.Empty;

        private string connectionIconStatus;
        private string connectionLabelStatus;

        private ObservableCollection<ToolMenuItem> menu; 
        private ObservableCollection<NotificationItemMenu> notifications;
        private ObservableCollection<NotificationItemMenu> _endOfDayNotifications; 

        private string userName;
        private bool hasErrorOnLogin;
        private bool isloggedin;
        private DateTime startSession;
        private DateTime lastSessinotime;
        private bool IsBusy;

        private string _syncText;
        private bool _isRunningSync;
        private bool _isRunningEndOfDay;
        private bool _isLockedForEndOfDay;
        private double _progressvalue;
        

        #endregion
         
        #region Sync Events

        public string SyncText
        {
            get { return _syncText; }
            set { _syncText = value; RaisePropertyChanged("SyncText"); }
        }

        public  void OnApplicationLoaded()
        {

             IsLoading = true;
            BusyContent = "Inicializando";
            lock (Extensions.SyncLock)
            {
                Synchronization.Connect();
                IsLoading = false;
                StoredCallbackProcessor.Run();
            }

        }

        

        public void OnCompleteSync()
        {
            RaisePropertyChanged("StartPageNotifications");
            IsLoading = false;
            IsRunningSync = false;
            BusyContent = string.Empty;
            //ViewModelManager.LoadActionBar(new TransferRequestBar());   
        }

        public void ReportProgres(string content)
        {
            BusyContent = content;
        }

        public static void AddMessage()
        {
            
        }

        
        private void ShowError(ApiException error)
        {
            if (IsRunningSync) return;
            Application.Current.Dispatcher.Invoke(new Action(() => System.Windows.MessageBox.Show(ViewModelManager.mainWindow, error.Message + " " + error.ExceptionMessage, "Error",
                System.Windows.MessageBoxButton.OK, MessageBoxImage.Error)));

            
        }


        #endregion

        public static event Action OnLogout = null;
    }
}
