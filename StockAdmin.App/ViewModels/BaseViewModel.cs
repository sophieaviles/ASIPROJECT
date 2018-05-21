using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Models;
using SigiFluent.Views;
using SigiFluent.Views.UserControls;

namespace SigiFluent.ViewModels
{
    public  class BaseViewModel :BaseModel
    {
      

        public BaseViewModel()
        {
            StartDate = DateTime.Now;
            LastDate = DateTime.Now.AddDays(1);
            dialog = new WindowContentView();
           
        }

        #region PROPERTIES

        public string EditViewButtonLabel
        {
            get { return _editViewButtonLabel; }
            set
            {
                _editViewButtonLabel = value;
                RaisePropertyChanged("EditViewButtonLabel");
            }
        }

        public string FormTitle
        {
            get { return _formTitle ?? string.Empty; }
            set { _formTitle = value; RaisePropertyChanged("FormTitle"); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; RaisePropertyChanged("IsBusy"); }
        }

        public bool IsDetailsBusy
        {
            get { return isDetailsBusy; }
            set { isDetailsBusy = value; RaisePropertyChanged("IsDetailsBusy"); }
        }

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; RaisePropertyChanged("Keyword"); }
        }

        public string WarehouseCode
        {
            get { return warehouseCode; } set{ warehouseCode = value;RaisePropertyChanged("WarehouseCode"); }
        }

        public DateTime? StartDate
        {
            get { return startDate; } set { startDate = value; RaisePropertyChanged("StartDate"); }
        }

        public DateTime? LastDate
        {
            get { return lastDate; }
            set { lastDate = value; RaisePropertyChanged("LastDate"); }

        } 

        public LocalStatus? localStatus
        {
            get { return status; }
            set { status = value; RaisePropertyChanged("localStatus"); }
        }

        public bool ForceRefresh
        {
            get { return forceRefresh; }
            set { forceRefresh = value; RaisePropertyChanged("ForceRefresh"); }
        }

        public NNM1_Series SelectedSerie
        {
            get { return selectedserie; }
            set { selectedserie = value;RaisePropertyChanged("SelectedSerie"); }
        }

        public OITB_Groups ABSelectedGroup
        {
            get { return abSelectedGroup; }
            set { abSelectedGroup = value; RaisePropertyChanged("ABSelectedGroup"); }
        }

        public bool IsEditModeDetail
        { get { return isEditModeDetail; } set { isEditModeDetail = value; RaisePropertyChanged("IsEditModeDetail"); } }


        public bool IsModalVisible
        {
            get { return ismodalVisible; }
            set
            {
                ismodalVisible = value; RaisePropertyChanged("IsModalVisible");
                
            }
        }

        public string ErrorMessage { get { return _errormsg; } set { _errormsg = value; RaisePropertyChanged("ErrorMessage"); } }
        public DateTime DisplayDateStart { get { return DateTime.Now; } }

        #endregion

        #region Commands
        public ICommand CmdCleanErrorMessage { get { return new RelayCommand(() => { ErrorMessage = string.Empty; }); } }

        public ICommand CloseModalViewCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        { 
                                            if(!ViewModelManager.CurrentBaseModel.IsModalVisible) ViewModelManager.CloseModal(); 
                                            else ViewModelManager.CurrentBaseModel.IsModalVisible = false;
                                            IsModalVisible = false;
                                            if(dialog != null) dialog.DataContext = null;
                    });
            }
        } 

        #endregion

        #region Virtual Methods

        public virtual void OnCloseModal()
        {
            IsModalVisible = false;
            ViewModelManager.CloseModal();
        }

        public virtual void SaveChanges()
        {
          ContextFactory.SaveChanges();
            
        }

       
        public   void UndoChanges()
        {
            ContextFactory.RollBack();

        }
         
        public void VerifyPendingChanges()
        {
            if (ContextFactory.HasPendingChanges())
            {
                if (ConfirmDialog("Existen Cambios Pendientes Desea Guardarlos","Cambios Pendientes")) SaveChanges();
                else UndoChanges();    
            }
        }

        public virtual void ExecuteNewCommand()
        {
        }
        public virtual bool CanExecuteNewCommand()
        {
            return !IsBusy;
        }
        public virtual void ExecuteDoProcess()
        {

        }
        public virtual bool CanExecuteDoProcess()
        {
            return true;
        }
        public virtual void ExecuteDelete()
        {
        }
        public virtual bool CanExecteDelete()
        {
            return true;
        }
        public virtual void ExecuteEdit()
        {

        }

        public virtual bool CanExecuteEdit()
        {

            return true;
        }

        public virtual void ExecuteActivation() { }

        public virtual bool CanExecuteActivation() { return true; }

        public virtual void ExecuteCancelation() { }

        public virtual bool CanExecuteCancelation() { return true; }

        public virtual void ExecuteNewCreditNote()
        {
           
        }

        public virtual bool CanExecuteNewCreditNote() { return true;}

        public virtual void ViewReport()
        {
            
        }

        public virtual bool CanViewReport()
        {
            return true;
        }


        public virtual void RefreshItemSource( )
        {
            
        }

       
        #endregion

        #region Public Methods
        public void ShowDialog<View,TviewModel>(View view, TviewModel viewmodel, ResizeMode resizeMode = ResizeMode.CanResizeWithGrip, SizeToContent sizeToContent = SizeToContent.Manual)
          where View : UserControl where TviewModel: BaseViewModel
        {
            dialog = null;
            dialog = new WindowContentView();
            dialog.ContentView.Children.Add(view);
            dialog.DataContext = viewmodel;
            
            ViewModelManager.ShowModal(dialog, new Thickness(25,25,25,25)); 
        }

        
        public void ShowDialog<View, TviewModel>(View view, TviewModel viewmodel,  Thickness margin)
            where View : UserControl
            where TviewModel : BaseViewModel
        {
            dialog = null;
            dialog = new WindowContentView();
            dialog.ContentView.Children.Add(view);
            dialog.DataContext = viewmodel;

            ViewModelManager.ShowModal(dialog, margin);
        }

        
        public void ShowModal<View>(View view) where View : UserControl
        {
            IsModalVisible = true;
            dialog.modalContainer.Children.Clear();
            dialog.modalContainer.Children.Add(view);
        }

        // To Block UI while Upload
        public void ShowProcessLoader<ViewModel>( ViewModel viewmodel ) where ViewModel: BaseViewModel
        {
            FormTitle = "Sincronizando con el Servidor Por favor espere..";
            ViewModelManager.CloseModal();
            ViewModelManager.IsLockedModal = true;
             ShowDialog(new ProcessLoader(), viewmodel, new Thickness(400,250,400,250));
        }

      

        public bool ConfirmDelete()
        {
            var result = System.Windows.MessageBox.Show("Confirma que Desea Eliminar?", "Eliminar",
                System.Windows.MessageBoxButton.YesNo);

            return result == MessageBoxResult.Yes; 
        }

        public bool ConfirmDialog(string message,string caption)
        {
            var result = System.Windows.MessageBox.Show(ViewModelManager.mainWindow, message,caption,
                System.Windows.MessageBoxButton.YesNo,MessageBoxImage.Question);

            return result == MessageBoxResult.Yes;
        }
        public void ShowDialog(string message, string caption)
        {
             System.Windows.MessageBox.Show(ViewModelManager.mainWindow, message, caption, System.Windows.MessageBoxButton.OK,MessageBoxImage.Information);

        }

        public void ShowErrorMessageBox(string message)
        {
            System.Windows.MessageBox.Show(ViewModelManager.mainWindow, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowWarningMessage(string message)
        {
            return
                System.Windows.MessageBox.Show(ViewModelManager.mainWindow, message, "Advertencia!",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes;
        }
        #endregion
        
        #region Private Properties
        private string warehouseCode { get; set; }
        private DateTime? startDate { get; set; }
        private DateTime? lastDate { get; set; }
        private string keyword { get; set; }
        private  bool forceRefresh = false;
        private bool isBusy = false;
        private bool isDetailsBusy;
        private string _editViewButtonLabel;
        private LocalStatus? status;
        private NNM1_Series selectedserie;
        private OITB_Groups abSelectedGroup;
        private bool isEditModeDetail;

        private bool ismodalVisible ;
        private SigiFluent.Views.WindowContentView dialog;
        private string _errormsg;
        private string _formTitle;

        #endregion
        
    }
}