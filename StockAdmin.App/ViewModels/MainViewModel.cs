#region Copyright and License Information

// Fluent Ribbon Control Suite
// http://fluent.codeplex.com/
// Copyright © Degtyarev Daniel, Rikker Serg., Weegen Patrick 2009-2013.  All rights reserved.
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://fluent.codeplex.com/license

#endregion

using System.ComponentModel;
using StockAdminContext.Models;
 


namespace SigiFluent.ViewModels
{
    /// <summary>
    /// Represents main view model
    /// </summary>
    public class MainViewModel : BaseModel
    {
        #region PROPERTIES

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                RaisePropertyChanged("FilterString");
                FilterCollection();
            }
        }

        public ICollectionView Purchases
        {
            get
            {
                if (_list != null) return _list;
               
                //_list = CollectionViewSource.GetDefaultView(SigiApi.Helpers.TransferRequestHelper.GetPurchases());
                IsBusy = false;
                return _list;
            }
            set
            {
                if (_list == value) return;
                _list = value;

                RaisePropertyChanged("BillSales");
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                if (!value)
                {
                    Purchases.Filter = Filter;
                }
                RaisePropertyChanged("IsBusy");
            }
        }

        #endregion PROPERTIES

        public MainViewModel()
        {

          
        }
        
        public bool Filter(object obj)
        {
            var data = obj as OPCH_Purchase;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(_filterString))
                {
                    return data.CreatedByL.ToLower().Contains(_filterString) ||
                           data.DocStatus.ToLower().Contains(_filterString) ||
                           data.Comments.ToLower().Contains(_filterString) ||
                           //data.DocDate.ToString().Contains(_filterString) ||
                           data.DocNum.ToString().Contains(_filterString);
                }
                return true;
            }
            return false;
        }

        private void FilterCollection()
        {
            if (_list != null)
            {
                _list.Refresh();
            }
        }

        private string _filterString;
        private ICollectionView _list;
        private bool _isBusy = true;
    }
}