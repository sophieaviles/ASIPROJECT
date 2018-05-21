using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;

namespace SigiFluent.ViewModels
{
    public class TransfersViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryResume> TransferDetailsResume
        {
            get
            {
                IsBusy = true;
                var res = Transfer.WTR1_TransferDetails.Any() ? CategoriesHelper.GetCategoryResume(Transfer.WTR1_TransferDetails, Transfer.WhsCode) : new ObservableCollection<CategoryResume>();
                IsBusy = false;
                return res;
            }
        }

        public ObservableCollection<OWTR_Transfers> TransfersCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || _transfers == null)
                {
                    _transfers = TransferHelper.GetTransfers(WarehouseCode, StartDate, LastDate, Keyword, localStatus);

                    ForceRefresh = false;
                }
                IsBusy = false;
                return _transfers;
            }
            set { _transfers = value; RaisePropertyChanged("TransfersCollection"); }
        }

        public OWTR_Transfers Transfer
        {
            get { return _transfer; }
            set
            {
                if(value == null) return;
                _transfer = value;
                RaisePropertyChanged("Transfer");
                SelectedSerie = SeriesHelper.GetSerie(value.Series);
            }
        }

        #region COMMANDS
        #region EDITAR ENTREGA
        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }

        public override void ExecuteEdit()
        {
            TransferEditing();
            
        }

        public override bool CanExecuteEdit()
        {
            return Transfer != null;
        }

        private void TransferEditing()
        {
            if (Transfer != null && Transfer.IdTransferL > 0)
            {
                if (Transfer.Series != null) SelectedSerie = SeriesHelper.GetSerie((short)Transfer.Series);
               // SelectedBranchStore = BranchsHelper.GetFiller(TransferRequest.Filler);
            }

            FormTitle = "Detalles de Entrega " + Transfer.DocNum.ToString();
            ShowDialog(new NewTransferView(), this);
        }
        #endregion EDITAR ENTREGA

        #region PROCESA NUEVO PEDIDO
        public ICommand CmdProcessTransfer
        {
            get { return new RelayCommand(ExecuteDoProcess, CanExecuteDoProcess); }
        }


        #endregion PROCESA NUEVO PEDIDO

        #region PROCESA ENTREGA
        public override void ExecuteDoProcess()
        {
            if (!ConfirmDialog("¿Desea procesar el registro?", "Procesar")) return;


            ViewModelManager.CloseModal(); // para cerrar la ventana detalles si esta abierta.
            
            AsyncHelper.DoAsync(() => TransferProcessing(), () =>
            {
                IsBusy = false;
                RefreshItemSource();
                if (OnUpdateNotifications != null) OnUpdateNotifications();
            });
        }

        public override bool CanExecuteDoProcess()
        {
            return Transfer != null && Transfer.ReceptionDateL == null;
        }

        private void TransferProcessing()
        {
            IsBusy = true;

            Transfer.ModifiedDateL = Transfer.ReceptionDateL= DateTime.Now;
            Transfer.ModifiedByL = Config.CurrentUser;

            ContextFactory.SaveChanges(); //TransferHelper.Update();

        }

        #endregion PROCESA ENTREGA

        #endregion COMMANDS

        #region Overrides

        public override void RefreshItemSource()
        { 
            RaisePropertyChanged("TransfersCollection");
        }

       

        #endregion
      
        #region Private properties
        private ObservableCollection<OWTR_Transfers> _transfers { get; set; }
        private OWTR_Transfers _transfer { get; set; }
        
        #endregion

        public static event Action OnUpdateNotifications;
    }
}
