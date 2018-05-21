using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;
using System.Collections.ObjectModel;

namespace SigiFluent.ViewModels
{
    public class CheckBookViewModel:BaseViewModel
    {
        private Checkbook _checkbook;
        private ObservableCollection<Checkbook> _checkbooksCollection;
        private bool _isactivable;

        public Checkbook CheckBook
        {
            get { return _checkbook; }
            set
            {
                _checkbook = value;
                RaisePropertyChanged("CheckBook");
                
            }
        }

        public bool IsActivable
        {
            get { return _isactivable; }
            set
            {
                _isactivable = value;
                RaisePropertyChanged("IsActivable");
            }
        }

        public ObservableCollection<Checkbook> CheckBooksCollection {
            get {
                IsBusy = true;
                if (ForceRefresh || _checkbooksCollection == null) {
                    _checkbooksCollection = CheckBookHelper.GetCollection();
                    IsActivable = _checkbooksCollection.All(cb => cb.StateL != CheckBookStatus.Activo);
                    ForceRefresh = false;
                }
                IsBusy = false;
                return _checkbooksCollection;
            }
        }
        
        #region COMMANDS
        public override void ExecuteActivation()
        { 
            CheckBook.StateL = CheckBookStatus.Activo;
            if (CheckBook.NextNumberL == 0) CheckBook.NextNumberL = 1;
            CheckBookHelper.Update();
            ForceRefresh = true;
            RaisePropertyChanged("CheckBooksCollection");
        }

        public override bool CanExecuteActivation() {
            var ret = false;
            if (CheckBook != null)
            {
                if (CheckBook.IdCheckbookL > 0 && CheckBook.StateL != CheckBookStatus.Activo && IsActivable) ret = true;
            }
            else ret = false;
            return ret;
            
        }


        public override void ExecuteCancelation() {
            CheckBook.StateL = CheckBookStatus.Anulado;
            CheckBookHelper.Update();
            ForceRefresh = true;
            RaisePropertyChanged("CheckBooksCollection");
        }

        public override bool CanExecuteCancelation() {
            var ret = false;
            if (CheckBook == null) return ret;
            if (CheckBook.IdCheckbookL > 0 && CheckBook.StateL != CheckBookStatus.Anulado) ret = true;
            return ret;
        }


        public override void ExecuteNewCommand()
        {
            FormTitle = "Nuevo Talonario";
            CheckBook = new Checkbook();
            ShowDialog(new NewCheckBookView(), this, new Thickness(300,200,300,200));
        }

        public ICommand CmdAddCheckBook
        {
            get 
            {                 
                return  new RelayCommand(CreateCheckbook, CanCreateCheckBook);
            }
        }

        private bool CanCreateCheckBook() {
            return CheckBook.InitialNumL > 0 && CheckBook.LastNumL > 0 && !string.IsNullOrEmpty(CheckBook.SerieL);    
        }

        private void CreateCheckbook()
        {
            if (CheckBook.LastNumL < CheckBook.InitialNumL)
            {
                ErrorMessage = "El rango de valores de la excepción no corresponde al rango de valores del talonario activo";
                return;
            }

            CheckBook.StateL = 0;
            CheckBook.NextNumberL = 1;
            CheckBook.DateL = DateTime.Now;
            CheckBook.CreatedDateL = DateTime.Now;
            CheckBook.CreatedByL = Config.CurrentUser;
            CheckBook.ModifiedDateL = DateTime.Now;
            CheckBook.ModifiedByL = Config.CurrentUser;

            CheckBookHelper.Add(CheckBook);

            ViewModelManager.CloseModal();
            CheckBooksCollection.Add(CheckBook);
            
        }

        #endregion COMMANDS
    }
}
