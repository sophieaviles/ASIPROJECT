using System.Windows;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SigiFluent.ViewModels
{
    class CanceledCheckBookViewModel : BaseViewModel
    {
        private StockAdminContext.Models.CanceledCheckBook _canceledcheckbook;
        private ObservableCollection<CanceledCheckBook> _canceledcheckbookscollection;
        private Checkbook _activecheckbook;

        public CanceledCheckBookViewModel() {
            ForceRefresh = true;
        }

        public CanceledCheckBook CanceledCheckBook {
            get { return _canceledcheckbook; }
            set { _canceledcheckbook = value;
            RaisePropertyChanged("CanceledCheckBook");
            }
        }

        public Checkbook ActiveCheckBook {
            get
            {
                if (_activecheckbook != null && !ForceRefresh) return _activecheckbook;
                _activecheckbook = CheckBookHelper.GetActiveCheckBook();
                ForceRefresh = false;
                return _activecheckbook;
            }
        }


        public ObservableCollection<CanceledCheckBook> CanceledCheckBooksCollection {
            get {
                IsBusy = true;
                if (ForceRefresh || _canceledcheckbookscollection == null) {
                   _canceledcheckbookscollection = CanceledCheckBookHelper.GetCollection();
                }
                IsBusy = false;
                return _canceledcheckbookscollection;
            }
        }

        public override void ExecuteNewCommand()
        {
            FormTitle = "Nueva Excepción";
            CanceledCheckBook = new CanceledCheckBook();
          //  ShowDialog(new NewCanceledCheckBookView(), this, ResizeMode.NoResize, SizeToContent.WidthAndHeight);
        }

        public override bool CanExecuteNewCommand() {
            return ActiveCheckBook != null;
        }

        public override void ExecuteDoProcess()
        {
            CanceledCheckBook.Processed = true;
            CanceledCheckBook.Update();
            ForceRefresh = true;
            RaisePropertyChanged("CanceledCheckBooksCollection");
        }

        public override bool CanExecuteDoProcess()
        {
            var ret = false;
            if (CanceledCheckBook != null)
            {
                if (CanceledCheckBook.IdL > 0 && !CanceledCheckBook.Processed) ret = true;
            }
            return ret;
        }

        public ICommand CmdAddCanceledCheckBook
        {
            get
            {
                return new RelayCommand(CreateCanceledCheckbook, CanCreateCanceledCheckbook);
            }
        }

        private bool CanCreateCanceledCheckbook()
        {
            var res = false;
            if (CanceledCheckBook != null)
            {
                res = CanceledCheckBook.LastNumL > 0 || CanceledCheckBook.LastNumL > CanceledCheckBook.InitialNumL;
            }
            return res;
        }

        private void CreateCanceledCheckbook()
        {
            ErrorMessage = string.Empty;
            if (CanceledCheckBook.InitialNumL > CanceledCheckBook.LastNumL) {
                ErrorMessage = "El valor inicial debe ser menor que el valor final";
                return;
            }

            if (CanceledCheckBook.InitialNumL < ActiveCheckBook.InitialNumL || CanceledCheckBook.LastNumL > ActiveCheckBook.LastNumL) {
                ErrorMessage = "El rango de valores de la excepción no corresponde al rango de valores del talonario activo";
                return;
            }

            CanceledCheckBook.CreatedDateL = DateTime.Now;
            CanceledCheckBook.CreatedByL = Config.CurrentUser;
            CanceledCheckBook.ModifiedDateL = DateTime.Now;
            CanceledCheckBook.ModifiedByL = Config.CurrentUser;
            CanceledCheckBook.IdCheckBookL = ActiveCheckBook.IdCheckbookL;
            
            CanceledCheckBookHelper.Add(CanceledCheckBook);
            CanceledCheckBooksCollection.Add(CanceledCheckBook);
            ViewModelManager.CloseModal();
            ForceRefresh = true;
            

        }

      

    }
}
