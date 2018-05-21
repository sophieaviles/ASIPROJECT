using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using Telerik.Windows.Documents.RichTextBoxCommands;

namespace SigiFluent.ViewModels
{
    public class BusinessPartnerChooserViewModel :BaseViewModel
    {
        public BusinessPartnerChooserViewModel()
        {
            Synchronization.onBussinesPartnerUpdate += () => ForceRefresh = true;
        }

        public bool IsSell { get; set; }

        public ObservableCollection<OCRD_BusinessPartner> BusinessPartnerCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || partners == null)
                {
                    partners = BusinessPartnerHelper.GetCustomers(SearchKeyword);
                }


                IsBusy = false;
                IsSell = true;
                return partners;
                
            }
            set { partners = value; RaisePropertyChanged("BusinessPartnerCollection"); }
        }


        public ObservableCollection<OCRD_BusinessPartner> SupplierPartnerCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || partners == null)
                {
                    spartners = BusinessPartnerHelper.GetSupplier(SearchKeyword);
                }


                IsBusy = false;
                IsSell = false;
                return spartners;
            }
            set { spartners = value; RaisePropertyChanged("SupplierPartnerCollection"); }
        }

        public OCRD_BusinessPartner SelectedPartner
        {
            get { return selectedPartner; }
            set { selectedPartner = value; RaisePropertyChanged("SelectedPartner"); }
        }

        public string SearchKeyword
        {
            get { return searchkeyword; }
            set
            {
                searchkeyword = value;
                ForceRefresh = true;

            }
        }

        public bool IsFocusedSearchBox
        {
            get { return isfocusedSearchBox; }
            set
            {
                isfocusedSearchBox = value;
                RaisePropertyChanged("IsFocusedSearchBox");
            }
        }

        public bool IsFocusedPartnersGrid
        {
            get { return isfocusedpartnersGrid; }
            set { isfocusedpartnersGrid = value; RaisePropertyChanged("IsFocusedPartnersGrid"); }
        }

        #region COMMANDS
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<System.Windows.Controls.TextBox>((textbox) =>
                {
                    if (textbox != null && SearchKeyword != textbox.Text)
                    {
                        SearchKeyword = textbox.Text;
                        RaisePropertyChanged(IsSell ? "BusinessPartnerCollection" : "SupplierPartnerCollection");
                        ForceRefresh = true;
                    }
                });
            }
        }


        public ICommand SelectPartnerCommand
        {
            get { return new RelayCommand(() =>
                                          {
                                              if (OnSelect != null) OnSelect(SelectedPartner);
                                              IsModalVisible = false;
                                          });}
        }

        public ICommand SetFocusToGrid
        {
            get { return new RelayCommand(()=> IsFocusedPartnersGrid=true );}
        }

        #endregion COMMANDS 

        public override void OnCloseModal()
        {
            if (OnClose != null) OnClose();
        }

        public void OnSearchTextChanged(System.Windows.Controls.TextBox txt)
        {
            if (SelectedPartner == null || txt == null || selectedPartner.CardName == txt.Name) return;
            SearchKeyword = txt.Text;
            ForceRefresh = true;
        }


        #region private properties

        public  event Action OnClose;
        public event Action<OCRD_BusinessPartner> OnSelect;
        private ObservableCollection<OCRD_BusinessPartner> partners;
        private OCRD_BusinessPartner selectedPartner;
        private string searchkeyword;
        private bool isfocusedSearchBox;
        private bool isfocusedpartnersGrid;
        private ObservableCollection<OCRD_BusinessPartner> spartners;

        #endregion
    }
}
