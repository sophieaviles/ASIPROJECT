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
    public class ClientCreditNoteViewModel: BaseViewModel
    {
        #region PUBLIC PROPERTIES
        public ObservableCollection<ORIN_ClientCreditNotes> CreditNotesCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || _creditnotescollection == null)
                {
                    _creditnotescollection = ClientCreditNoteHelper.GetCreditNotes(ClientCreditNoteType.Sale, SelectedSerie, StartDate, LastDate, Keyword, localStatus);
                    ForceRefresh = false;
                }
                IsBusy = false;
                return _creditnotescollection;
            }

        }

        public ORIN_ClientCreditNotes CreditNote
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

        public ObservableCollection<RIN1_ClientCreditNoteDetail> CreditNoteDetailsCollection
        {
            get
            {
                if (CreditNote != null && _creditNoteDetailsCollection == null)
                {
                    _creditNoteDetailsCollection = new ObservableCollection<RIN1_ClientCreditNoteDetail>(CreditNote.RIN1_ClientCreditNoteDetail);

                }
                return _creditNoteDetailsCollection;
            }
            set
            {
                _creditNoteDetailsCollection = value;
                RaisePropertyChanged("CreditNoteDetailsCollection");
            }
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
            get
            {
                if (Exento) return 0;
                return WithHolding ? (decimal)Config.IVARETValue * Summary : (decimal)Config.IVACOMValue * Summary;
               
                
            }
        }

        public decimal Total
        {
            get
            {
                return Summary + Taxes; 
            }

        }

        public bool Exento {
            get { return CreditNote.ExentoL; }
            set { CreditNote.ExentoL = value; }
        }

        public bool WithHolding {
            get { return CreditNote.WithHoldingL; }
            set { CreditNote.WithHoldingL = value; }
        }

        public OCRD_BusinessPartner SelectedPartner
        {
            get { return _selectedPartner; }
            set { _selectedPartner = value; RaisePropertyChanged("SelectedPartner"); }
        }

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
            get { return !IsEnabledSave; }
        }

        #endregion PUBLIC POPRTIES

#region COMMANDS
        #region GUARDA NOTA DE CREDITO
        public ICommand CmdSaveCreditNote
        {
            get { return new RelayCommand(SaveCreditNote, () => IsEnabledSave); }
        }

        private void SaveCreditNote()
        {
            CreditNote.DocTotal = Total;
            // CreditNote.SeriesTitle = SelectedSerie.SeriesName + " " + SelectedSerie.Remark;
            CreditNote.RIN1_ClientCreditNoteDetail = CreditNoteDetailsCollection.ToList();

            if (CreditNote.IdClientCreditNoteL > 0)
            {

                CreditNote.ModifiedDateL = DateTime.Now;
                CreditNote.ModifiedByL = Config.CurrentUser;
                CreditNote.StateL = LocalStatus.Guardado;
                ClientCreditNoteHelper.Update();
                RaisePropertyChanged("CreditNotesCollection");
            }
            else
            {
                FillData();
                CreditNote.StateL = LocalStatus.Guardado;
                ClientCreditNoteHelper.Add(CreditNote);
                // CreditNotesCollection.Add(CreditNote);
            }

            ViewModelManager.CloseModal();

            if (CreditNote.IdClientCreditNoteL == 0)
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
            IsBusy = IsDetailsBusy = true;

            if (!ConfirmDialog("Desea Procesar la Nota de Credito Seleccionada", "Procesar"))
            {
                IsBusy = IsDetailsBusy = false;
                return;
            }   

            ShowProcessLoader(this);

            AsyncHelper.DoAsync(() =>
                                {  
                                 
                                    CreditNote.DocTotal = Total;
                                    //CreditNote.SeriesTitle = SelectedSerie.SeriesName + " " + SelectedSerie.Remark;
                                    CreditNote.RIN1_ClientCreditNoteDetail = CreditNoteDetailsCollection.ToList();

                                    if (CreditNote.IdClientCreditNoteL > 0)
                                    {
                                        CreditNote.ModifiedDateL = DateTime.Now;
                                        CreditNote.ModifiedByL = Config.CurrentUser;
                                        //CreditNote.StateL = LocalStatus.Pendiente;
                                        ClientCreditNoteHelper.Update();
                                        //RaisePropertyChanged("CreditNotesCollection");
                                    }
                                    else
                                    {
                                        FillData();
                                        //CreditNote.StateL = LocalStatus.Pendiente;
                                        ClientCreditNoteHelper.Add(CreditNote);
                                        // CreditNotesCollection.Add(CreditNote);
                                    }
                                    Synchronization.Synchronize(CreditNote);
                                    SaveChanges();

                                    IsDetailsBusy = IsBusy = false;
                                    RaisePropertyChanged("CreditNotesCollection");
                                },ViewModelManager.CloseProcessLoader);

        ViewModelManager.CloseModal();

            if (CreditNote.IdClientCreditNoteL == 0)
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
            return CreditNote != null && CreditNote.StateL == LocalStatus.Guardado &&!IsBusy;
        }

        private void Deleting()
        {
            if (!ConfirmDelete()) return;
            ClientCreditNoteHelper.Delete(CreditNote);
            CreditNotesCollection.Remove(CreditNote);
        }
        #endregion BORRA NOTA DE CREDITO SELECCIONADA

        #region PROCESA NOTA DE CREDITO SELECCIONADA
        public override void ExecuteDoProcess()
        {
            ProcessCreditNote();

        }

        public override bool CanExecuteDoProcess()
        {
            return CreditNote != null && CreditNote.StateL== LocalStatus.Guardado &&!IsReadOnly &&!IsBusy;
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
            ShowDialog(new NewClientCreditNote(), this);

        }
        #endregion EDITAR NOTA DE CREDITO SELECCIONADA

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("CreditNotesCollection");
        }
#endregion COMMANDS

        private void FillData()
        {

            CreditNote.DocDate = DateTime.Now.Date;
            CreditNote.DocDueDate = DateTime.Now.Date;
            CreditNote.CreatedByL = Config.CurrentUser;
            CreditNote.CreatedDateL = DateTime.Now;
            CreditNote.ModifiedByL = Config.CurrentUser;
            CreditNote.ModifiedDateL = DateTime.Now;

        }

        #region PRIVATE PROPERTIES
        private ObservableCollection<ORIN_ClientCreditNotes> _creditnotescollection;
        private ORIN_ClientCreditNotes _creditnote;
        private OCRD_BusinessPartner _selectedPartner;
        private ObservableCollection<RIN1_ClientCreditNoteDetail> _creditNoteDetailsCollection;
       
        #endregion PRIVATE PROPERTIES
    }
}
