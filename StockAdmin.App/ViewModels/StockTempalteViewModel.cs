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
    public class StockTempalteViewModel:BaseViewModel
    {
       

        #region PROPIEDADES PUBLICAS        
       
        public OWHS_Branch Branch
        {
            get
            {
                return BranchsHelper.GetBranch(Config.WhsCode,forceRefresh:true);
            }
           
        }


        public ObservableCollection<OITM_Articles> GenericBranchArticles
        {
            get
            {
                if (_branchArticles == null||ForceRefresh)
                {
                    IsBusy = true;
                    _branchArticles = new ObservableCollection<OITM_Articles>(BranchsHelper.GetGenericBranchArticles(Branch));
                    IsBusy = false;
                }
               
                return _branchArticles;

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

       

        
        private ObservableCollection<OITM_Articles> _branchArticles;
    }
}
