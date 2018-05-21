using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;

namespace SigiFluent.ViewModels
{
    public class DepositViewModel:BaseViewModel
    {

        #region PUBLIC PROPERTIES

        public ObservableCollection<Deposit> DepositCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || depositCollection  == null)
                {
                    depositCollection= DepositHelper.GetDeposits(null, StartDate, LastDate, Keyword, localStatus);
                    IsBusy = false;
                    ForceRefresh = false;
                }
                IsBusy = false;
                return depositCollection;
            }

        }

        public Deposit  SelectedDeposit
        {
            get { return selectedDeposit; }
            set { selectedDeposit = value; RaisePropertyChanged("SelecteDeposit"); }
        }

        public ObservableCollection<LocalUser> CashierCollection
        {
            get { return cashierscollection ??(cashierscollection = Membership.GetCashiers()); }
            set { cashierscollection = value; RaisePropertyChanged("CashierCollection"); }
        }

         
        public ObservableCollection<LocalUser> ShopManagersCollection
        {
            get { return shopManagerCollection ??( shopManagerCollection = Membership.GetShopManagers()); }
            set { shopManagerCollection = value; RaisePropertyChanged("ShopManagersCollection"); }
        }
         

        public ObservableCollection<NNM1_Series> Series
        {
            get { return seriescollection ?? (seriescollection= SeriesHelper.GetSeries("13")); }
            set { seriescollection = value; RaisePropertyChanged("Series"); }
        } 

        public bool IsReadOnly
        {
            get { return SelectedDeposit!=null && selectedDeposit.StateL!= LocalStatus.Guardado; }
        }

        public bool IsEnabled
        {
            get { return !IsReadOnly; }
        }

        public bool IsEnabledCommands
        {
            get
            {
                return IsEnabled && SelectedDeposit != null 
                        && SelectedDeposit.Number > 0;
            }
        }

        #endregion

        public void SaveNewDetails()
        {
            // TODo after save item
            if (SelectedDeposit != null && SelectedDeposit.Cash + selectedDeposit.Cheque > (decimal)Config.MaximoDepositos && !ConfirmDialog("Los depositos sobrepasan los $200, desea continuar", "Confirmar"))
            {
                return;
            }

            if (SelectedDeposit!=null && ConfirmDialog("Desea Guardar los cambios", "Confirmar"))
            {
               DepositHelper.AddOrUpdate(SelectedDeposit);
                 SaveChanges();
                    ForceRefresh = true;
                    RaisePropertyChanged("DepositCollection");

                    ViewModelManager.CloseModal();
            }
            else UndoChanges();
        }


        private void Process()
        {
            if (SelectedDeposit != null && SelectedDeposit.Cash + selectedDeposit.Cheque > (decimal)Config.MaximoDepositos && !ConfirmDialog("Los depositos sobrepasan los $200, desea continuar", "Confirmar"))
            {
                return;
            }
            if (SelectedDeposit != null && ConfirmDialog("Desea procesar los cambios ?", "Procesar"))
            {
                SelectedDeposit.StateL = LocalStatus.Procesado;
                DepositHelper.AddOrUpdate(SelectedDeposit);
                SaveChanges();
                ForceRefresh = true;
                RaisePropertyChanged("DepositCollection");

                ViewModelManager.CloseModal();
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
            get { return new RelayCommand(SaveNewDetails, ()=>IsEnabledCommands); }
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
            get { return new RelayCommand(Process, () => IsEnabledCommands); }
        }


        #endregion COMMANDS


        #region OVERRIDES

        public override void ExecuteNewCommand()
        {
            SelectedDeposit = null;
            SelectedDeposit = new Deposit();
            FormTitle = "Nuevo Depósito";
            ShowDialog(new DepositView(),this, new Thickness(300,150,300,150));
        }

        
        public override bool CanExecuteEdit()
        {
            return SelectedDeposit != null ;
        }

        public override void ExecuteEdit()
        {
            RaisePropertyChanged("SelectedDeposit");
            FormTitle = "Detale de Depósito";
            ShowDialog(new DepositView(),this, new Thickness(300,150,300,150));
        }

        public override void ExecuteDelete()
        {
            if (SelectedDeposit != null && ConfirmDelete())
            {
                DepositHelper.Delete(SelectedDeposit);
                SaveChanges();
                DepositCollection.Remove(SelectedDeposit);
            }
        }

       

        public override void ExecuteDoProcess()
        {
            Process();
        }

        public override bool CanExecuteDoProcess()
        {
            return SelectedDeposit != null && selectedDeposit.StateL == LocalStatus.Guardado;
        }

        public override bool CanExecteDelete()
        {
            return SelectedDeposit != null && selectedDeposit.StateL == LocalStatus.Guardado;
        }

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("DepositCollection");
        }
        #endregion 

        #region PRIVATE PROPERTIES

        private ObservableCollection<Deposit> depositCollection;
        private Deposit selectedDeposit;
        private ObservableCollection<LocalUser> cashierscollection;
        private ObservableCollection<LocalUser> shopManagerCollection;
        
        private ObservableCollection<NNM1_Series> seriescollection;

        #endregion
    }
}
