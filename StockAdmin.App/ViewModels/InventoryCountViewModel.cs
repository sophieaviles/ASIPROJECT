using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.Views;

namespace SigiFluent.ViewModels
{
   public  class InventoryCountViewModel :BaseViewModel
   {

       public InventoryCountViewModel()
       {
           this.EditViewButtonLabel = "Editar Conteo";
           StoredCallbackProcessor.OnUpdate += Refresh;
       }

       private void Refresh()
       {
           RaisePropertyChanged("DetailsCount");
           
       }
       
       #region public properties 

       public bool IsEnabled
       {
           get
           {
               return isEnabled;
           }
           set
           {
               isEnabled = value;
               RaisePropertyChanged("IsEnabled");
           }
       }
       public ObservableCollection<InventoryCount> InventoryCountCollection
       {
           get
           {
               IsBusy = true;
                
                
                   invCollection = InventoryCountHelper.GetInventoryCount(ABSelectedGroup, StartDate, LastDate, Keyword,
                       localStatus);
                   ForceRefresh = false;
                
               IsBusy = false;
               return invCollection;
           }
           set { invCollection = value; RaisePropertyChanged("InventoryCountCollection"); }
       }

       public InventoryCount SelectedInventoryCount
       {
           get { return count; }
           set
           {
               if (count != value)
               {
                   ForceDetailsRefresh = true;
                   if (IsReadOnly) this.EditViewButtonLabel = "Ver Detalle";
                   else this.EditViewButtonLabel = "Editar Conteo";
                    count = value; 
                   RaisePropertyChanged("SelectedInventoryCount");
               }
              
           }
       } 

       public OITB_Groups SelectedGroupFromDetail
       {
           get
           {
               return _group;
           }
           set
           {
               if (_group != value)
               {
                   _group = value;
                   if (SelectedInventoryCount != null && value!=null)
                   {
                       SelectedInventoryCount.ItmsGrpCod = value.ItmsGrpCod;
                       ForceDetailsRefresh = true;
                       RaisePropertyChanged("DetailsCount");
                   }
               }
               RaisePropertyChanged("SelectedGroupFromDetail");
           }
       }
       public ObservableCollection<OITB_Groups> Groups
       {
           get
           {
               return groups ?? (groups = GroupHelper.GetGroups());
           }

           set
           {
               groups = value;
               RaisePropertyChanged("Groups");
           }

       }
       public ObservableCollection<InventoryCountDetail> DetailsCount
       {
           get
           {
               IsDetailsBusy = true;
               if (SelectedInventoryCount == null)  return null;

               if (ForceDetailsRefresh || _detailCount==null)
               {
                   _detailCount = InventoryCountHelper.GetDetalCollection(SelectedGroupFromDetail,
                       SelectedInventoryCount);
                   ForceDetailsRefresh = false;
               }

               IsDetailsBusy = false;
               return _detailCount;
           }
           set
           {
               if (value != null)
               {
                   _detailCount = value;
                   
               }
               RaisePropertyChanged("DetailsCount");
           }
       }

       public InventoryCountDetail SelectedDetail
       {
           get { return _selectedDetail; }
           set { _selectedDetail = value; RaisePropertyChanged("SelectedDetail"); }
       }

      

       public bool IsReadOnly
       {
           get
           {
               return SelectedInventoryCount != null &&
                      SelectedInventoryCount.StateL != LocalStatus.Guardado;
           }
       }

       #endregion 

       #region COMMANDS

       public ICommand ProcessCommand
       {
           get { return new RelayCommand(ExecuteDoProcess,CanExecuteDoProcess);}
       }

       public ICommand SaveInventoryCountCommand
       {
           get { return new RelayCommand(SaveInventoryCount,CanSaveInventoryCount);}
       }

       public ICommand DeleteCommand
       {
           get { return new RelayCommand(ExecuteDelete,CanExecteDelete);}
       }

       public ICommand EditCommand
       {
           get { return new RelayCommand(ExecuteEdit,CanExecuteEdit);}
       }

       
       #endregion

       #region private Methods

       #region Save Iventory Count

       private void SaveInventoryCount()
       {
           if (!ValidateChanges()) return;
           AsyncHelper.DoAsync(() =>
                               {
                                   IsBusy = true;
                                   InventoryCountHelper.AddOrUpdate(SelectedInventoryCount);
                                   SaveChanges();
                                   IsBusy = false;
                                      RefreshItemSource();

                               });
           ViewModelManager.CloseModal();
       }

       private bool CanSaveInventoryCount()
       {
           return !IsReadOnly && !IsBusy;
       }
       #endregion

       #region process new INventory Count 

       public void ProcessInventoryCount()
       {
           
           IsDetailsBusy = true;
           InventoryCountHelper.AddOrUpdate(SelectedInventoryCount);
           SaveChanges();
           StoredCallbackProcessor.StartAlohaSync(ignoreAsync:true);
           StoredCallbackProcessor.UpdateStock();
           InventoryCountHelper.Process(SelectedInventoryCount);
          RefreshItemSource();
           
       }

       
       private bool ValidateChanges()
       {
           if (!ConfirmDialog("Desea guardar los cambios", "Guardar")) return false;

           if (!DetailsCount.Any())
           {
               ShowErrorMessageBox("Verifique los valores ingresados");
               return false;
           }

           if (DetailsCount.Any(d => d.Quantity < 0))
           {
               ShowErrorMessageBox("Error Existen valores negativos , verificar!");
               return false;
           }
           if (SelectedInventoryCount.IdInventoryCountL != 0) ForceDetailsRefresh = true;

           // Update details
           var details = DetailsCount.Where(d => d.Quantity.HasValue && d.Quantity.Value >= 0).ToList();

           // Agregar si no tiene
           if (!SelectedInventoryCount.InventoryCountDetail.Any())
           {
               SelectedInventoryCount.InventoryCountDetail.AddRange(details);
           }
           else
           {
               var newDetails = details
                              .Where(d => SelectedInventoryCount.InventoryCountDetail.All(inv => inv.ItemCode != d.ItemCode))
                              .ToList();
               // add if we have new details.
               if (newDetails.Any()) SelectedInventoryCount.InventoryCountDetail.AddRange(newDetails);
           }

           return true;
       }

        #endregion

       #endregion

       #region Overrides

       public override void RefreshItemSource()
       {
           ForceRefresh = true;
           RaisePropertyChanged("InventoryCountCollection");
       }

       public override void ExecuteNewCommand()
       {
           IsEnabled = true;
           SelectedInventoryCount = null;
           SelectedInventoryCount = new InventoryCount();
           SelectedGroupFromDetail = null;
           FormTitle = "Nuevo Conteo Fisico";
           //ForceDetailsRefresh = true; para no mostar la lista de prod. al cargar el control 
           if(DetailsCount!=null) DetailsCount.Clear();
           
           ShowDialog(new InventoryCountDetailView(),this);
       }

       
       // OVerride Process Inventory Count 

       public override void ExecuteDoProcess()
       {
           IsBusy = true;

           if (!ValidateChanges())
           {
               IsBusy = false;
               return;
           }
            
           ViewModelManager.CloseModal();
           ShowProcessLoader(this);
           AsyncHelper.DoAsync(ProcessInventoryCount,OnCompleteProcess);
       }

       private void OnCompleteProcess()
       {
           ViewModelManager.CloseProcessLoader();
           IsBusy = false;
           IsDetailsBusy = false;
           RefreshItemSource();
           
       }



       public override bool CanExecuteDoProcess()
       {
           return SelectedInventoryCount != null && !IsReadOnly && !IsBusy;
       }

       public override void ExecuteDelete()
       {
           if (!ConfirmDelete()) return;
           InventoryCountHelper.Delete(SelectedInventoryCount);
           ForceDetailsRefresh = true;
           SaveChanges();
           InventoryCountCollection.Remove(SelectedInventoryCount);
           RaisePropertyChanged("InventoryCountCollection");
       }

       public override bool CanExecteDelete()
       {
           return SelectedInventoryCount != null && !IsReadOnly && !IsBusy;
       }

       public override void ExecuteEdit()
       {
           _group = Groups.FirstOrDefault(g => g.ItmsGrpCod == SelectedInventoryCount.ItmsGrpCod);

           IsEnabled = SelectedInventoryCount != null && SelectedInventoryCount.IdInventoryCountL == 0;
           FormTitle = "Detalles de conteo";
           ForceDetailsRefresh = true;
           ShowDialog(new InventoryCountDetailView(), this);
           
           
       }

       public override bool CanExecuteEdit()
       {
           return SelectedInventoryCount != null;
       }
       
       #endregion 

       #region Private PRoperties
       private InventoryCount count { get; set; }
       private ObservableCollection<InventoryCount> invCollection  { get; set; }
       private ObservableCollection<InventoryCountDetail> _detailCount { get; set; }
       private InventoryCountDetail _selectedDetail { get; set; }
       private OITB_Groups _group;
       private OITB_Groups _groupFilter;
       private ObservableCollection<OITB_Groups> groups { get; set; }
       private UMovement _movement;
       private OWHS_Branch _brach { get; set; }

       private bool ForceDetailsRefresh { get; set; }
       private bool isEnabled { get; set; }
       #endregion
   }
}
