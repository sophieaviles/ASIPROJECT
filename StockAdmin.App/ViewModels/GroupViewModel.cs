using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SigiFluent.ViewModels
{
    public class GroupViewModel:BaseViewModel
    {

        #region PROPIEDADES PUBLICAS      

        public ObservableCollection<OITB_Groups> GenericBranchArticles
        {
            get
            {
                if (_groups == null)
                {
                    IsBusy = true;
                    _groups = new ObservableCollection<OITB_Groups>(BranchsHelper.GetGroups());
                    IsBusy = false;
                }

                return _groups;

            }
        }
        #endregion PROPIEDADES PUBLICAS


        public ICommand CmdSaveStockTemplate
        {
            get
            {
                return new RelayCommand(SaveStockTemplate);

            }
        }

        private void SaveStockTemplate()
        {
            BranchsHelper.SaveTemplateArticles();
            ViewModelManager.CloseModal();

        }

        private ObservableCollection<OITB_Groups> _groups;

    }
}
