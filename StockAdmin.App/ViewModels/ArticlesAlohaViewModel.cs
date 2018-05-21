using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;

namespace SigiFluent.ViewModels
{
    public class ArticlesAlohaViewModel:BaseViewModel
    {
        #region PROPIEDADES PUBLICAS
     
        public ObservableCollection<ALOHA_Articles> GenericAlohaArticles
        {
            get
            {
                if (_AlohaArticles == null||ForceRefresh)
                {
                    IsBusy = true;
                    _AlohaArticles = new ObservableCollection<ALOHA_Articles>(ArticlesHelper.GetArticlesAlohaCollection());
                    IsBusy = false;
                }

                return _AlohaArticles;

            }
        }
        #endregion PROPIEDADES PUBLICAS


        public ICommand CmdStartSynchronization
        {
            get
            {
                return new RelayCommand(UpdateArticles);

            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(CloseModal);

            }
        }

        private void SaveStockTemplate()
        {
            BranchsHelper.SaveTemplateArticles();
            ViewModelManager.CloseModal();

        }

        private void CloseModal()

        {
            ViewModelManager.CloseModal();
        }


        private void UpdateArticles()
        {
            StoredCallbackProcessor.CallBack("SP_UpdateArticlesALOHA");
            ShowDialog("Actualización realizada correctamente", "Confirmación");
        }

        private ObservableCollection<ALOHA_Articles> _AlohaArticles;

    }
}
