using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.ViewModels;
using SigiFluent.Views;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace SigiFluent.Commands
{
    class ValidatorBehavior : BaseModel
    {
        private readonly RadGridView gridView = null;
        private readonly bool isValidationEnabled = false;
       
        public static readonly DependencyProperty IsValidationEnabledProperty =
          DependencyProperty.RegisterAttached("IsValidationEnabled", typeof(bool), typeof(ValidatorBehavior),
          new PropertyMetadata(new PropertyChangedCallback(OnValidationSummaryPropertyChanged)));


        public static void SetIsValidationEnabled(DependencyObject dependencyObject, bool isEnabled)
        {
            dependencyObject.SetValue(IsValidationEnabledProperty, isEnabled);
        }

        public static bool GetIsValidationEnabled(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsValidationEnabledProperty);
        }

        public static void OnValidationSummaryPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RadGridView gridView = dependencyObject as RadGridView;
            bool isEnabled = (bool)e.NewValue;

            if (gridView != null && isEnabled)
            {
                ValidatorBehavior behavior = new ValidatorBehavior(gridView, isEnabled);
            }
        }

        public ValidatorBehavior(RadGridView gridView, bool isEnabled)
        {
            this.gridView = gridView;
            this.isValidationEnabled = isEnabled;

            this.gridView.CellValidating += this.GridView_CellValidating;
        }

        void GridView_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            
            
            GridViewCell cell = e.Cell;

            //isValid = IsValidInventoryRange(e.NewValue);

            var viewmodel = ViewModelManager.GetViewModel<TransferRequestViewModel, TransferRequestsView>();
            var qty = (decimal)0.0;
            string validationText = "";
            bool isValid = false;
            if (decimal.TryParse(e.NewValue.ToString(), out qty))
            {


                qty = e.NewValue != null ? (decimal) e.NewValue : 0;
                var rangeMessage = TransferRequestHelper.GetInventoryRange(viewmodel.SelectedDetail.ItemCode, qty);

                isValid = (string.IsNullOrEmpty(rangeMessage.Item1)) ||
                          viewmodel.ConfirmDialog(rangeMessage.Item1, "Alerta inventario");

                validationText = rangeMessage.Item2;

                if (!isValid)
                {
                    validationText += "\n Porfavor ingrese un valor valido";
                }

                if (!isValid)
                {
                    this.MarkCell(cell, validationText);
                }
                else
                {
                    this.RestoreCell(cell);
                }
            }
            else validationText = "Ingrese un valor valido";
            e.ErrorMessage = validationText;
            e.IsValid = isValid;
        }

 

        private void MarkCell(Control cell, string validationText)
        {
            ToolTipService.SetToolTip(cell, validationText);
        }

        private void RestoreCell(Control cell)
        {
            ToolTipService.SetToolTip(cell, null);
        }

    }
}
