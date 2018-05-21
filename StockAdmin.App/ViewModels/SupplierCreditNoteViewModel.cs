using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;
using SigiFluent.Views.ActionBars;

namespace SigiFluent.ViewModels
{
    public class SupplierCreditNoteViewModel:BaseViewModel
    {

        #region PUBLIC PROPERTIES
        public bool IsEnabledSave
        {
            get
            {
                if (CreditNote == null) return false;
                if (CreditNoteDetailsCollection == null) return false;
                if (CreditNote != null && CreditNote.StateL != LocalStatus.Guardado) return false;
                return CreditNoteDetailsCollection.Any(d => d.Quantity > 0) && !string.IsNullOrEmpty(CreditNote.Comments);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return CreditNote != null && CreditNote.StateL == LocalStatus.Procesado;
            }
            
        }

        public ObservableCollection<ORPC_SupplierCreditNotes> CreditNotesCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || _creditnotescollection == null)
                {
                    _creditnotescollection = SupplierCreditNoteHelper.GetPurchase(SelectedSerie, StartDate, LastDate, Keyword, localStatus);
                    ForceRefresh = false;
                }
                IsBusy = false;
                return _creditnotescollection;
            }
           
        }

        public ORPC_SupplierCreditNotes CreditNote
        {
            get { return _creditnote; }
            set
            {
                if (value == null) return;
                _creditnote = value;
                RaisePropertyChanged("CreditNote");

                SelectedPartner = BusinessPartnerHelper.GetBusinessPartner(_creditnote.CardCode);

                SelectedSerie = SeriesHelper.GetSerie(_creditnote.Series);
                RaisePropertyChanged("SelectedSerie");
            }
        }

        public ObservableCollection<RPC1_SupplierCreditNoteDetail> CreditNoteDetailsCollection
        {
            get
            {
                if (CreditNote != null && _creditNoteDetailsCollection == null)
                {
                    _creditNoteDetailsCollection = new ObservableCollection<RPC1_SupplierCreditNoteDetail>(CreditNote.RPC1_SupplierCreditNoteDetail);

                }
                return _creditNoteDetailsCollection;
            }
            set
            {
                _creditNoteDetailsCollection = value;
                RaisePropertyChanged("CreditNoteDetailsCollection");
            }
        }
        

        public RPC1_SupplierCreditNoteDetail SelectedtCreditNoteDetail
        {
            get { return _selectedtCreditNoteDetail; }
            set
            {
                _selectedtCreditNoteDetail = value;
                RaisePropertyChanged("SelectedtPurchaseDetail");
            }
        }


        public OCRD_BusinessPartner SelectedPartner
        {
            get { return _selectedPartner; }
            set { _selectedPartner = value; RaisePropertyChanged("SelectedPartner"); }
        }

        public decimal Summary
        {
            get
            {
                return _creditNoteDetailsCollection != null ? (decimal)(_creditNoteDetailsCollection.Where(d => d.LineTotal != null).Sum(d => d.LineTotal)) : 0;               
            }
        }

        public decimal Taxes
        {
            get { return (decimal)0.13 * Summary; }
        }

        public decimal Total
        {
            get
            {
                return Summary + Taxes;
            }

        }

        #endregion PUBLIC PROPERTIES
        
        #region COMMANDS
        #region  ACTUALIZA RESUMEN

        public ICommand UpdateCreditNoteDetail
        {
            get
            {
                return new RelayCommand(() =>
                {
                   
                    SelectedtCreditNoteDetail.Price = SelectedtCreditNoteDetail.LineTotal/SelectedtCreditNoteDetail.Quantity;
                    
                    RefreshPriceTotal();
                });
            }
        }

        
        #endregion ACTUALIZA RESUMEN

        #region ELIMINA DETALLE SELECCIONADO
        public ICommand DeleteSelectedDetailsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CreditNote == null || !ConfirmDelete() || CreditNote.StateL == LocalStatus.Procesado) return; 
                    if (CreditNote.IdSupplierCreditNote > 0) SupplierCreditNoteHelper.DeleteDetail(SelectedtCreditNoteDetail);
                    CreditNote.RPC1_SupplierCreditNoteDetail.Remove(SelectedtCreditNoteDetail);
                    CreditNoteDetailsCollection.Remove(SelectedtCreditNoteDetail);
                    RefreshPriceTotal();
                });
            }
        }
        #endregion ELIMINA DETALLE SELECCIONADO
        
        #region GUARDA NOTA DE CREDITO
        public ICommand CmdSaveCreditNote
        {
            get { return new RelayCommand(SaveCreditNote, () => IsEnabledSave); }
        }

        private void SaveCreditNote()
        {
            CreditNote.DocTotal = Total;
           // CreditNote.SeriesTitle = SelectedSerie.SeriesName + " " + SelectedSerie.Remark;
            CreditNote.RPC1_SupplierCreditNoteDetail = CreditNoteDetailsCollection.ToList();

            if (CreditNote.IdSupplierCreditNote > 0)
            {

                CreditNote.ModifiedDateL = DateTime.Now;
                CreditNote.ModifiedByL = Config.CurrentUser;
                CreditNote.StateL = LocalStatus.Guardado;
                SaveChanges(); //SupplierCreditNoteHelper.Update();
                RaisePropertyChanged("CreditNotesCollection");
            }
            else
            {
                FillData();
                CreditNote.StateL = LocalStatus.Guardado;
                SupplierCreditNoteHelper.Add(CreditNote);
               // CreditNotesCollection.Add(CreditNote);
            }

            ViewModelManager.CloseModal();

            if (CreditNote.IdSupplierCreditNote == 0)
                MessageBox.Show(
                    "La Nota de Crédito fue creada exitosamente, por favor seleccione la pestaña NOTAS DE CRÉDITO para ver el detalle");


        }
        #endregion GUARDA NOTA DE CREDITO
        
        #region PROCESA NOTA DE CREDITO
        public ICommand CmdProcessCreditNote
        {
            get { return new RelayCommand(ProcessCreditNote, CanExecuteDoProcess); }
        }

        private void ProcessCreditNote()
        {
            CreditNote.DocTotal = Total;
            //CreditNote.SeriesTitle = SelectedSerie.SeriesName + " " + SelectedSerie.Remark;
            CreditNote.RPC1_SupplierCreditNoteDetail = CreditNoteDetailsCollection.ToList();

            if (CreditNote.IdSupplierCreditNote > 0)
            {
                CreditNote.HasToBeSync = true;
                CreditNote.ModifiedDateL = DateTime.Now;
                CreditNote.ModifiedByL = Config.CurrentUser;
                //CreditNote.StateL = LocalStatus.Pendiente;
                SaveChanges();//SupplierCreditNoteHelper.Update();
                RaisePropertyChanged("CreditNotesCollection");
            }
            else
            {
                FillData();
                //CreditNote.StateL = LocalStatus.Pendiente;
                SupplierCreditNoteHelper.Add(CreditNote);
                // CreditNotesCollection.Add(CreditNote);
            }

            ViewModelManager.CloseModal();

            if (CreditNote.IdSupplierCreditNote == 0)
                MessageBox.Show(
                    "La Nota de Crédito esta en proceso, por favor seleccione la pestaña NOTAS DE CRÉDITO para ver el detalle");
        }
        #endregion PROCESA NOTA DE CREDITO

        #region BORRA NOTA DE CREDITO SELECCIONADA
        public ICommand DeleteCurrentComand
        {
            get { return new RelayCommand(ExecuteDelete, CanExecteDelete); }
        }

        public override void ExecuteDelete()
        {
          Deleting();
        }

        public override bool CanExecteDelete()
        {
            return CreditNote != null && !IsReadOnly &&IsBusy;
        }

        private void Deleting()
        {
            if (!ConfirmDelete()) return;
            SupplierCreditNoteHelper.Delete(CreditNote);
            CreditNotesCollection.Remove(CreditNote);
        }
        #endregion BORRA NOTA DE CREDITO SELECCIONADA

        #region PROCESA NOTA DE CREDITO SELECCIONADA
        public override void ExecuteDoProcess()
        {
            IsBusy = true;
            ShowProcessLoader(this);
            AsyncHelper.DoAsync(CreditNoteProcessing, () =>
            {
                ViewModelManager.CloseProcessLoader();
                IsBusy = false;
                ForceRefresh = true;
                RaisePropertyChanged("CreditNotesCollection");
            });
        }

        public override bool CanExecuteDoProcess()
        {
            return CreditNote != null && !IsReadOnly && !IsBusy;
        }

        private void CreditNoteProcessing()
        {

            //CreditNote.StateL = LocalStatus.Pendiente;
            CreditNote.HasToBeSync = true;
            SaveChanges();

            Synchronization.Synchronize(CreditNote);

        }
        #endregion  PROCESA NOTA DE CREDITO SELECCIONADA

        #region EDITAR NOTA DE CREDITO SELECCIONADA
        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }

        public override void ExecuteEdit()
        {
            CreditNoteEditing();
        }

        public override bool CanExecuteEdit()
        {
            return CreditNote != null;
        }

        private void CreditNoteEditing()
        {
            CreditNoteDetailsCollection = null;
            RaisePropertyChanged("CreditNote");
            
            FormTitle = "Editar Nota de Crédito";

            ShowDialog(new NewCreditNotes(), this);

        }
        #endregion EDITAR NOTA DE CREDITO SELECCIONADA

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("CreditNotesCollection");
        }
        #endregion COMMANDS

        #region PRIVATE METHODS
        private void RefreshPriceTotal()
        {
            RaisePropertyChanged("Total");
            RaisePropertyChanged("Taxes");
            RaisePropertyChanged("Summary");
        }

        private void FillData()
        {
           
            CreditNote.DocDate = DateTime.Now.Date;
            CreditNote.DocDueDate = DateTime.Now.Date;
            CreditNote.CreatedByL = Config.CurrentUser;
            CreditNote.CreatedDateL = DateTime.Now;
            CreditNote.ModifiedByL = Config.CurrentUser;
            CreditNote.ModifiedDateL = DateTime.Now;
            
        }
        #endregion PRIVATE METHODS
        
        #region PRIVATE PROPERTIES
        private ORPC_SupplierCreditNotes _creditnote;
        private OCRD_BusinessPartner _selectedPartner;
        private ObservableCollection<RPC1_SupplierCreditNoteDetail> _creditNoteDetailsCollection;
        private RPC1_SupplierCreditNoteDetail _selectedtCreditNoteDetail;
        private ObservableCollection<ORPC_SupplierCreditNotes> _creditnotescollection;

        #endregion PRIVATE PROPERTIES
    }
}
