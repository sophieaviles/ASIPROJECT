using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;

namespace SigiFluent.ViewModels
{
    public class TransferRequestViewModel: BaseViewModel{

        public TransferRequestViewModel()
        {
            IsBusy = IsDetailsBusy = IsDetailsResumeBusy = true;
        }

        #region PROPIEDADES PUBLICAS

        public bool IsDetailsResumeBusy
        {
            get { return _isDetailsResumeBusy; }
            set { _isDetailsResumeBusy = value; RaisePropertyChanged("IsDetailsResumeBusy"); }
        }

        public OWTQ_TransferRequest TransferRequest
        {
            get
            {
                return _transferRequest;
            }
            set
            {
                _transferRequest = value;                
                RaisePropertyChanged("TransferRequest");
            }
        }
         
        public NNM1_Series SelectedSerie
        {
            get { return _selectedSerie; }
            set
            {
                _selectedSerie = value;
                if(value != null) TransferRequest.Series = value.Series;
                RaisePropertyChanged("SelectedSerie");
            }
        }
        
        public OWHS_Branch SelectedBranchStore
        {
            get {
                return _selectedBranchStore;  
            }
            set
            {                
               // if (_selectedBranchStore == value) return;
                _selectedBranchStore = value;
                if(value != null) TransferRequest.Filler =  value.WhsCode;                
                RaisePropertyChanged("SelectedBranchStore");
                RaisePropertyChanged("FilteredTransferRequestDetails");

            }
        }

        public bool IsReadOnly
        {
            get { return TransferRequest != null && TransferRequest.StateL != LocalStatus.Guardado; }
        }

        public bool IsEnabled
        {
            get { return !IsReadOnly; }
        }

        public WTQ1_TransferRequestDetails SelectedDetail
        {
            get { return _selectedTransferRequestDetail; }
            set { _selectedTransferRequestDetail = value;RaisePropertyChanged("SelectedDetail"); }
        }
        #region CATALOGOS
        /// <summary>
        /// Obtiene la lista de detalles filtrado por almacen seleccionado
        /// </summary>
        public ObservableCollection<WTQ1_TransferRequestDetails> FilteredTransferRequestDetails
        {
            get
            {
                IsDetailsBusy = true;
                _filteredTransferRequestDetails = SelectedBranchStore != null
                                         ? TransferRequestHelper.GetFilteredTransferRequestDetails( SelectedBranchStore, TransferRequest)
                                         : new ObservableCollection<WTQ1_TransferRequestDetails>();
                IsDetailsBusy = false;
                //todo: la asigna a la siguiente lista?
                TransferRequestDetails = _filteredTransferRequestDetails;
                RaisePropertyChanged("TransferRequestDetailsResume");
                return _filteredTransferRequestDetails;
            } 
        }
       
        public ObservableCollection<WTQ1_TransferRequestDetails> TransferRequestDetails
        {
            get
            {                
                return _transferRequestDetails;
            }
            set
            {
                _transferRequestDetails = value;
                RaisePropertyChanged("TransferRequestDetails");
               
            }
        }

        public ObservableCollection<CategoryResume> TransferRequestDetailsResume
        {
            get
            {
                IsDetailsResumeBusy = true;
                var res = TransferRequestDetails != null ? CategoriesHelper.GetCategoryResume(TransferRequestDetails, Config.WhsCode) : new ObservableCollection<CategoryResume>();
                IsDetailsResumeBusy = false;
                return res;
            }
        }

        public ObservableCollection<OWTQ_TransferRequest> TransferRequestCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || _transferRequestsCollection == null)
                {
                    _transferRequestsCollection = TransferRequestHelper.GetTransferRequests(WarehouseCode, StartDate, LastDate, Keyword, localStatus);
                    IsBusy = false;
                    ForceRefresh = false;
                }
                IsBusy = false;
                return _transferRequestsCollection;
            }

        }
        
        public ObservableCollection<NNM1_Series> TransferRequestSeries
        {
            get
            {
                if (_series == null)
                {
                    _series = SeriesHelper.GetSeries("1250000001");
                    IsDetailsBusy = false;
                }
                return _series;
            }
        }

        public List<OWHS_Branch> BranchStores
        {
            get { return BranchsHelper.GetBranchStores(); }
        }

        #endregion CATALOGOS

        #endregion PROPIEDAES PUBLICAS

        #region COMANDOS

        public override void ExecuteNewCommand()
        {
            TransferRequest = null;
            TransferRequest = new OWTQ_TransferRequest();
            SelectedSerie = null;
            SelectedBranchStore = null;
            FormTitle = "Nuevo Pedido";
           ShowDialog(new NewTransferRequestView(), this);
             
        }

        #region  ACTUALIZA RESUMEN

        public ICommand UpdateResumeCommand
        {
            get
            {
                return new RelayCommand(() => RaisePropertyChanged("TransferRequestDetailsResume"));
            }
        }
        #endregion ACTUALIZA RESUMEN

        #region GUARDA UN PEDIDO
        public ICommand CmdSaveTransferRequest
        {
            get { return new RelayCommand( ()=>SaveTransferRequest(), () => IsEnabledSave); }
        }

        private void SaveTransferRequest(LocalStatus? status=null)
        {
            //si ya existe
            if (TransferRequest.IdTransferRequestL > 0)
            {

                TransferRequest.ModifiedDateL = DateTime.Now;
                TransferRequest.ModifiedByL = Config.CurrentUser;
                TransferRequest.StateL = status.HasValue ? status.Value : LocalStatus.Guardado;
                if (TransferRequestDetails != null) //TransferRequestHelper.Update(TransferRequest, GetTransferRequestDetail());
               SavePendingChanges();
                TransferRequestHelper.AddOrUpdate(TransferRequest);
               SaveChanges();
                RaisePropertyChanged("TransferRequestCollection");
            }
            else //sino
            {
                CreateTransferRequest();
                TransferRequest.StateL = status.HasValue ? status.Value : LocalStatus.Guardado;
                TransferRequestHelper.AddOrUpdate(TransferRequest);
                //TransferRequestHelper.Add(TransferRequest);
                TransferRequestCollection.Add(TransferRequest);
                SaveChanges();
            }

            if (OnUpdateNotifications != null) OnUpdateNotifications();
            ViewModelManager.CloseModal();
           
        }

        private void SavePendingChanges()
        {
            //Agregar los que no existen en la tabla detalles
            var existingIds = TransferRequest.WTQ1_TransferRequestDetails
                .Select(t => t.ItemCode).ToList();

            var newDetails = TransferRequestDetails.ToList()
                .Where(v => v.Quantity > 0 && !existingIds.Contains(v.ItemCode))
                .ToList();

            TransferRequest.WTQ1_TransferRequestDetails
                .AddRange(newDetails);

            // todos los cambios en la colleccion detalles.
            var existingDetails = TransferRequestDetails.ToList()
                .Where(v => existingIds.Contains(v.ItemCode)).ToList();


            //Actualizar la cantidad de el producto seleccionado y modificado en la colleccion. 
            foreach (var detail in existingDetails)
            {
                var d = TransferRequest.WTQ1_TransferRequestDetails.FirstOrDefault(c => c.ItemCode == detail.ItemCode);
                d.Quantity = detail.Quantity;
            }

            var codes = existingDetails.Where(q => q.Quantity == 0).Select(d => d.ItemCode).ToList();
            if (TransferRequest.WTQ1_TransferRequestDetails != null)
            {
                var toDelete = TransferRequest.WTQ1_TransferRequestDetails
                    .Where(d => codes.Contains(d.ItemCode))
                    .ToList();

                TransferRequestHelper.DeleteZeroQtyDetails(toDelete);
            }

        
        }
       

        #endregion GUARDA UN PEDIDO

        #region PROCESA NUEVO PEDIDO 
        public ICommand CmdProcessTransferRequest
        {
            get { return new RelayCommand(ExecuteDoProcess, () => IsEnabledSave &&!IsBusy); }
        }

        
        #endregion PROCESA NUEVO PEDIDO

        #region EDITAR PEDIDO SELECCIONADO
        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }

        public override void ExecuteEdit()
        {
            TransferRequestEditing();
        }

        public override bool CanExecuteEdit()
        {
            return TransferRequest != null && !IsBusy;
        }

        private void TransferRequestEditing()
        {
            if (TransferRequest != null && TransferRequest.IdTransferRequestL > 0)
            {
                if (TransferRequest.Series != null) SelectedSerie = SeriesHelper.GetSerie((short)TransferRequest.Series);
                SelectedBranchStore = BranchsHelper.GetFiller(TransferRequest.Filler);
            }
            //FormTitle = string.Concat("Detalles de pedido ",TransferRequest.DocNum.ToString());
            FormTitle = "Detalles de pedido " + TransferRequest.DocNum.ToString();
            ShowDialog(new NewTransferRequestView(), this);
        }
        #endregion EDITAR PEDIDO SELECCIONADO

        #region PROCESA PEDIDO SELECCIONADO
        public override void ExecuteDoProcess()
        {
            IsBusy = true;
            if (!ConfirmDialog("¿Desea procesar el registro?", "Procesar"))
            {
                IsBusy = false;
                return;
            }

            ViewModelManager.CloseModal();
            ShowProcessLoader(this);
            AsyncHelper.DoAsync(() =>
                                {
                                   
                                    TransferRequestProcessing();
                                    RefreshItemSource();
                                    if (OnUpdateNotifications != null) OnUpdateNotifications();
                                    IsBusy = false;
                                },ViewModelManager.CloseProcessLoader);
            //ViewModelManager.CloseModal(); // para cerrar la ventana detalles si esta abierta.
        }

        public override bool CanExecuteDoProcess()
        {
            return TransferRequest != null && !IsReadOnly && !IsBusy;
        }

        

        private void TransferRequestProcessing()
        {
             
            TransferRequest.HasToBeSync = true;
            IsBusy = true;
            if (TransferRequest.IdTransferRequestL > 0)
            {

                TransferRequest.ModifiedDateL = DateTime.Now;
                TransferRequest.ModifiedByL = Config.CurrentUser;
                //TransferRequest.StateL = LocalStatus.Pendiente;
                //if (TransferRequestDetails != null)  CODIGO CHAMPERO REMOVIDO
                    //TransferRequestHelper.Update(TransferRequest, GetTransferRequestDetail());
                SaveChanges();

            }
            else //sino
            {
                CreateTransferRequest();
                //TransferRequest.StateL = LocalStatus.Pendiente;
                TransferRequestHelper.Add(TransferRequest);
                TransferRequestCollection.Add(TransferRequest);
            }
            SaveChanges();
            Synchronization.Synchronize(TransferRequest); 
            SaveChanges();
        }

        #endregion PROCESA PEDIDO SELECCIONADO

        #region BORRAR PEDIDO SELECCIONADO
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
            return TransferRequest != null && !IsReadOnly &&!IsBusy; 
        }

        private void TransferRequestDeleting()
        {
            if (!ConfirmDelete()) return;
            TransferRequestHelper.Delete(TransferRequest);
            TransferRequestCollection.Remove(TransferRequest);
        }
        #endregion BORRAR PEDIDO SELECCIONADO

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("TransferRequestCollection");
        }


        #endregion COMANDOS

        #region METODOS PRIVADOS

        /// <summary>
        /// Valida la habilitacion de botones en el formulario de detalle de pedido
        /// </summary>
        private bool IsEnabledSave
        {
            get
            {
                if (TransferRequest == null) return false;
                if (TransferRequestDetails == null) return false;
                if (TransferRequest != null && TransferRequest.StateL != LocalStatus.Guardado) return false;
                return TransferRequestDetails.Any(d => d.Quantity > 0) &&
                    TransferRequest.DocDueDate != null &&
                    TransferRequest.Series != null &&
                    !string.IsNullOrEmpty(TransferRequest.Filler);// &&!string.IsNullOrEmpty(TransferRequest.Comments);

            }
        }
               
        /// <summary>
        /// Crea un nuevo transfer request para guardarlo en la db
        /// </summary>        
        /// <returns>nuevo transfer request para guardar</returns>
        private void CreateTransferRequest()
        {
           // var filler = BranchsHelper.GetFiller(SelectedBranchStore.WhsCode);

            TransferRequest.CardCode = Config.BusinessPartner; //Properties.Resources.BussinesPartner; //parametro x defecto, es el socio de negocio de la sucursal
            TransferRequest.DocDate = DateTime.Now; //siempre va a ser now
            TransferRequest.WhsCode = Config.WhsCode;
            //TransferRequest.FillerTitle = string.Format("{0} {1}", SelectedBranchStore.WhsCode, filler != null ? filler.WhsName : "--");
            TransferRequest.CreatedByL = Config.CurrentUser;
            TransferRequest.CreatedDateL = DateTime.Now;
            TransferRequest.ModifiedByL = Config.CurrentUser;
            TransferRequest.ModifiedDateL = DateTime.Now;
            TransferRequest.DeliveryDateL = DateTime.Now;
            TransferRequest.HasToBeSync = true;

            AddTransferRequestDetail();
        }

        private void AddTransferRequestDetail()
        {
            var list = TransferRequestDetails.Where(t => t.Quantity > 0);
            foreach (var detail in list.Select(d => new WTQ1_TransferRequestDetails()
            {
                ItemCode = d.ItemCode,
                Quantity = d.Quantity,
                WhsCode = Config.WhsCode,
                ItemName = d.ItemName,
                InvntryUom = d.InvntryUom,
                OnHand1 = d.OnHand1,
                CreatedDateL = DateTime.Now,
                CreatedByL = Config.CurrentUser,
                ModifiedByL = Config.CurrentUser,
                ModifiedDateL = DateTime.Now,

            }))
            {

                TransferRequest.WTQ1_TransferRequestDetails.Add(detail);
            }
        }

        private List<WTQ1_TransferRequestDetails> GetTransferRequestDetail()
        {
            var list = TransferRequestDetails.Where(t => t.Quantity > 0);
            return list.Select(d => new WTQ1_TransferRequestDetails()
            {
                ItemCode = d.ItemCode, 
                Quantity = d.Quantity, 
                WhsCode = Config.WhsCode, 
                ItemName = d.ItemName, 
                InvntryUom = d.InvntryUom, 
                OnHand1 = d.OnHand1, 
                CreatedDateL = DateTime.Now, 
                CreatedByL = Config.CurrentUser, 
                ModifiedByL = Config.CurrentUser, 
                ModifiedDateL = DateTime.Now,
            }).ToList();
        }

        #endregion METODOS PRIVADOS

        #region PROPIEDADES PRIVADAS
        private OWTQ_TransferRequest _transferRequest;
        private ObservableCollection<OWTQ_TransferRequest> _transferRequestsCollection;
        private OWHS_Branch branch;
        private ObservableCollection<NNM1_Series> _series;
        private ObservableCollection<WTQ1_TransferRequestDetails> _transferRequestDetails;
        private ObservableCollection<WTQ1_TransferRequestDetails> _filteredTransferRequestDetails;
        private OWHS_Branch _selectedBranchStore;
        private NNM1_Series _selectedSerie;
        private bool _isDetailsResumeBusy;
        private WTQ1_TransferRequestDetails _selectedTransferRequestDetail;
       
        #endregion PROPIEDADES PRIVADAS

        public static event Action OnUpdateNotifications;
        
    }
   
}