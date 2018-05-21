using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;

namespace SigiFluent.ViewModels
{
    public class CashMissingViewModel: BaseViewModel
    {

        #region PUBLIC PROPERTIES
        public ObservableCollection<CashMissing> CashCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || cashMissings == null)
                {
                    cashMissings = CashMissingHelper.GetCashMissings( StartDate, LastDate, Keyword, localStatus);
                    IsBusy = false;
                    ForceRefresh = false;
                }
                IsBusy = false;
                return cashMissings;
            }

        }

        public CashMissing SelectedCashMissing
        {
            get { return selectedCashMissing; }
            set { selectedCashMissing = value; RaisePropertyChanged("SelectedCashMissing"); }
        }


        public ObservableCollection<LocalUser> CashiersCollection
        {
            get { return cashierscollection ?? (cashierscollection = Membership.GetCashiers()); }
            set { cashierscollection = value; RaisePropertyChanged("CashiersCollection"); }
        }


        public bool IsFocusedCashierSelector
        {
            get { return isfocusedCashierSelector; }
            set { isfocusedCashierSelector = value; RaisePropertyChanged("IsFocusedCashierSelector"); }
        }

        public bool IsReadOnly
        {
            get { return SelectedCashMissing != null && SelectedCashMissing.StateL != LocalStatus.Guardado; }
        }

        

        public bool IsEnabled
        {
            get { return !IsReadOnly; }
        }


        #endregion 

        
        public void SaveNewDetails()
        {
            // TODo after save item
            if (SelectedCashMissing != null && ConfirmDialog("Desea Guardar los cambios", "Confirmar"))
            {
                CashMissingHelper.AddOrUpdate(SelectedCashMissing);
                SaveChanges();
                ForceRefresh = true;
                RaisePropertyChanged("CashCollection");

                ViewModelManager.CloseModal();
            }
            else UndoChanges();
            
        }


        private void Process()
        {
            if (SelectedCashMissing != null && ConfirmDialog("Desea guardar los cambios ?", "Guardar"))
            {
                SelectedCashMissing.StateL = LocalStatus.Procesado;
                SaveChanges();
            }
            else UndoChanges();
        }

        private void Sync()
        {
            // TODO implement Sync;
        }

        #region COMMANDS
        public ICommand SaveDetailsCommand
        {
            get { return new RelayCommand(SaveNewDetails, () => !IsReadOnly); }
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
            get { return new RelayCommand(Process, () => !IsReadOnly); }
        }


        #endregion


        #region OVERRIDES
        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("CashCollection");
        }

        public override void ExecuteNewCommand()
        {
            SelectedCashMissing = null;
            SelectedCashMissing = new CashMissing();
            isfocusedCashierSelector = true;
            FormTitle = "Nuevo Faltante de Efectivo";
            ShowDialog(new CashMissingView(), this, new Thickness(300,150,300,150));
        }


        public override bool CanExecuteEdit()
        {
            return SelectedCashMissing != null && !IsReadOnly;
        }

        public override void ExecuteEdit()
        {
            isfocusedCashierSelector = true;
            RaisePropertyChanged("SelectedCashMissing");
            FormTitle = "Detalle de Faltante de Efectivo";
            ShowDialog(new CashMissingView(), this, new Thickness(300, 150, 300, 150));
        }

        public override void ExecuteDelete()
        {
            if (SelectedCashMissing != null && ConfirmDelete())
            {
                CashMissingHelper.Delete(SelectedCashMissing);
                SaveChanges();
                CashCollection.Remove(SelectedCashMissing);
            }
        }

        public override bool CanExecteDelete()
        {
            return SelectedCashMissing != null && !IsReadOnly;
        }

        public override void ExecuteDoProcess()
        {
            Process();
        }

        

        #endregion


        #region PRIVATE PROPERTIES

        private ObservableCollection<CashMissing> cashMissings;
        private CashMissing selectedCashMissing;
        private ObservableCollection<LocalUser> cashierscollection;
        private bool isfocusedCashierSelector;

        #endregion

    }
}
