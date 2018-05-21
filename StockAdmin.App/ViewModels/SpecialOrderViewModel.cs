using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using StockAdminContext.WebServices;
using SigiFluent.Views;
//using SigiFluent.Views.Reports;
using SigiFluent.Views.UserControls;

namespace SigiFluent.ViewModels
{
    public class SpecialOrderViewModel : BaseViewModel
    {

        #region Public properties
        public ObservableCollection<ORDR_SpecialOrder> SpecialOrdersCollection
        {
            get
            {
                IsBusy = true;
                if (ForceRefresh || orders == null)
                {
                    orders = SpecialOrdersHelper.GetOrders(WarehouseCode, StartDate, LastDate, Keyword, localStatus);
                    ForceRefresh = false;
                }
                IsBusy = false;
                return orders;
            }
            set { orders = value; RaisePropertyChanged("SpecialOrdersCollection"); }
        }

        public ORDR_SpecialOrder SelectedOrder
        {
            get { return selectedOrder; }
            set
            {

                selectedOrder = value;
                SetCurrentDefaultValuesFor();
                RaisePropertyChanged("SelectedOrder");
                RaisePropertyChanged("AttachmentsCollection");
            }

        }

        public ObservableCollection<RDR1_SpecialOrderDetail> DetailsCollection
        {
            get
            {
                IsDetailsBusy = true;
                if (SelectedOrder != null)
                {
                    detailscollection = new ObservableCollection<RDR1_SpecialOrderDetail>(SelectedOrder.RDR1_SpecialOrderDetail);
                }
                return detailscollection;
            }
            set
            {
                detailscollection = value;
                RaisePropertyChanged("DetailsCollection");
            }
        }
        public RDR1_SpecialOrderDetail SelectedSpecialOrderDetails
        {
            get { return orderDetail; }
            set { orderDetail = value; RaisePropertyChanged("SelectedSpecialOrderDetails"); }
        }

        public bool IsFocusedAddButton
        {
            get { return isFocusedAddButton; }
            set { isFocusedAddButton = value; RaisePropertyChanged("IsFocusedAddButton"); }
        }

        public bool IsReadOnly
        {
            get { return SelectedOrder != null && SelectedOrder.StateL != LocalStatus.Guardado; }
        }

        public bool IsEnabled
        {
            get { return !IsReadOnly; }
        }

        public decimal? Total
        {
            get
            {
                if (DetailsCollection != null) return DetailsCollection.Sum(d => d.LineTotal);
                else return (decimal?)null;
            }
        }

        public ATC1_Attachments SelectedAttach
        {
            get { return attachment; }
            set
            {
                attachment = value;
                RaisePropertyChanged("SelectedAttach");
            }
        }

        public static event Action OnSelectedArticle;

        #region SelectListcollection

        public ObservableCollection<ATC1_Attachments> AttachmentsCollection
        {
            get
            {
                if (SelectedOrder != null)
                {
                    return new ObservableCollection<ATC1_Attachments>(SelectedOrder.ATC1_Attachments);
                }
                else return new ObservableCollection<ATC1_Attachments>();
            }
            set
            {

                if (SelectedOrder != null)
                {
                    SelectedOrder.ATC1_Attachments = value.ToList();
                    RaisePropertyChanged("AttachmentsCollection");
                }
            }
        }

        public ObservableCollection<UCake> CakesCollection // Conected
        {
            get { return cakes ?? (cakes = SpecialOrdersHelper.GetCakes()); }
            set { cakes = value; RaisePropertyChanged("CakesCollection"); }
        }

        public ObservableCollection<UCategory> CategoriesCollection
        {
            get { return categories ?? (categories = SpecialOrdersHelper.GetCategories()); }
            set { categories = value; RaisePropertyChanged("CategoriesCollection"); }
        }

        public ObservableCollection<UColor_Covert> ColorCovertCollection // Conected
        {
            get
            {
                if (colorCoverts != null && SelectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UColor_Covert>(colorCoverts.Where(c => c.IdStyle == selectedOrder.Style.Code));
                }

                return colorCoverts ?? (colorCoverts = SpecialOrdersHelper.GetColorCoverts());
            }
            set { colorCoverts = value; RaisePropertyChanged("ColorCovertCollection"); }
        }

        public ObservableCollection<UColor_Flower> ColorFlowersCollection // Conected
        {
            get
            {
                if (colorFlowers != null && SelectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UColor_Flower>(colorFlowers.Where(c => c.IdStyle == selectedOrder.Style.Code));
                }

                return colorFlowers ?? (colorFlowers = SpecialOrdersHelper.GetColorFlowers());
            }
            set { colorFlowers = value; RaisePropertyChanged("ColorFlowersCollection"); }
        }

        public ObservableCollection<UColor_Ribbon> ColorRibbonCollection //COnected
        {
            get
            {
                if (colorRibbon != null && selectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UColor_Ribbon>(colorRibbon.Where(c => c.IdStyle == selectedOrder.Style.Code));
                }

                return colorRibbon ?? (colorRibbon = SpecialOrdersHelper.GetColorRibbon());
            }
            set { colorRibbon = value; RaisePropertyChanged("ColorRibbonCollection"); }

        }

        public ObservableCollection<UColor_top> ColorTopsCollection //Connected
        {
            get
            {
                if (colorTops != null && selectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UColor_top>(colorTops.Where(c => c.IdStyle == SelectedOrder.Style.Code));
                }

                return colorTops ?? (colorTops = SpecialOrdersHelper.GetColorTops());
            }
            set { colorTops = value; RaisePropertyChanged("ColorTopsCollection"); }
        }

        public ObservableCollection<UCover> CoversCollection //Conected
        {
            get
            {
                if (covers != null && SelectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UCover>(covers.Where(c => c.IdStyle == SelectedOrder.Style.Code));
                }

                return covers ?? (covers = SpecialOrdersHelper.GetCovers());
            }
            set { covers = value; RaisePropertyChanged("CoversCollection"); }
        }

        public ObservableCollection<ULaza> LazasCollection //Conected
        {
            get
            {
                if (colorLazas != null && SelectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<ULaza>(colorLazas.Where(c => c.IdStyle == SelectedOrder.Style.Code));
                }

                return colorLazas ?? (colorLazas = SpecialOrdersHelper.GetLazas());
            }
            set { colorLazas = value; RaisePropertyChanged("LazasCollection"); }
        }

        public ObservableCollection<UGuirnalda> GuirnaldasCollection //Conected
        {
            get
            {
                if (colorGuirnaldas != null && SelectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UGuirnalda>(colorGuirnaldas.Where(c => c.IdStyle == SelectedOrder.Style.Code));
                }

                return colorGuirnaldas ?? (colorGuirnaldas = SpecialOrdersHelper.GetGuirnaldas());
            }
            set { colorGuirnaldas = value; RaisePropertyChanged("GuirnaldasCollection"); }
        }

        public ObservableCollection<UEspecialty> EspecialtiesCollection //Conected
        {
            get { return specialities ?? (specialities = SpecialOrdersHelper.GetSpecialities()); }
            set { specialities = value; RaisePropertyChanged("EspecialtiesCollection"); }
        }

        public ObservableCollection<UFlower> FlowersCollection //Conected
        {
            get
            {
                if (flowers != null && SelectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UFlower>(flowers.Where(c => c.IdStyle == SelectedOrder.Style.Code));
                }
                return flowers ?? (flowers = SpecialOrdersHelper.GetFlowers());
            }
            set { flowers = value; RaisePropertyChanged("FlowersCollection"); }
        }


        public ObservableCollection<UStyle> StylesCollection // Conected
        {
            get { return styles ?? (styles = SpecialOrdersHelper.GetStyles()); }
            set { styles = value; RaisePropertyChanged("StylesCollection"); }
        }

        public ObservableCollection<Ufiller> FillerCollection //Conected
        {
            get
            {
                if (Fillers != null && SelectedOrder != null)
                {
                    if (SelectedOrder.Style != null && SelectedOrder.Especiality != null)
                    {
                        return new ObservableCollection<Ufiller>(Fillers.Where(c => c.IdStyle == SelectedOrder.Style.Code
                            && c.IdSpecialty == SelectedOrder.Especiality.Code));
                    }


                    var collection = new ObservableCollection<Ufiller>();

                    // items by style group
                    if (SelectedOrder.Style != null) Fillers.Where(c => c.IdStyle == SelectedOrder.Style.Code).ToList().ForEach(collection.Add);

                    // Specialty 
                    if (SelectedOrder.Especiality != null) Fillers.Where(c => c.IdSpecialty == SelectedOrder.Especiality.Code).ToList().ForEach(collection.Add);

                    return collection;
                }
                return Fillers ?? (Fillers = SpecialOrdersHelper.GetFillers());
            }
            set { Fillers = value; RaisePropertyChanged("FillerCollection"); }
        }


        public ObservableCollection<UColor_Height> ColorHeightsCollection
        {
            get
            {
                if (colorHeights != null && SelectedOrder != null && SelectedOrder.Style != null)
                {
                    return new ObservableCollection<UColor_Height>(colorHeights.Where(c => c.IdStyle == SelectedOrder.Style.Code));
                }

                return colorHeights ?? (colorHeights = SpecialOrdersHelper.GetColorHeights());
            }
            set { colorHeights = value; RaisePropertyChanged("ColorHeightsCollection"); }
        }

        #endregion

        #endregion

        #region Private Methods

        private void SaveNewDetails()
        {
            if (ValidateDate() && ConfirmDialog("Confirma Guardar los cambios ", "Guardar"))
            {
                SpecialOrdersHelper.AddOrder(SelectedOrder);

                SaveChanges();
                ViewModelManager.CloseModal();
                ForceRefresh = true;
                RaisePropertyChanged("SpecialOrdersCollection");
            }
            else UndoChanges();
        }

        private void RefreshPriceTotal()
        {
            RaisePropertyChanged("Total");
            RaisePropertyChanged("Taxes");
            RaisePropertyChanged("Summary");
        }

        private void Process()
        {
            if (SelectedOrder != null && ValidateDate() && ConfirmDialog("Desea procesar el Pedido", "Confimar"))
            {
                //Guardar el pedido
                SpecialOrdersHelper.AddOrder(SelectedOrder);
                SaveChanges();

                var currentOrder = SelectedOrder;
                var param = new Dictionary<string, string>();
                param.Add("@IdSpecialOrder", selectedOrder.IdSpecialOrder.ToString());
                var data = StoredCallbackProcessor.CallDataSet("SP_INV_PEDIDOS_ES", param);
                //var report = new ReportContainer("rptPedidosEspeciales.rpt", data);

                 //report.Export(currentOrder.ReportFileName);

                ShowProcessLoader(this);
                AsyncHelper.DoAsync(() =>
                                    {
                                        Sync(currentOrder);
                                        RaisePropertyChanged("SpecialOrdersCollection");
                                    },
                ViewModelManager.CloseProcessLoader);
            }
            else UndoChanges();
        }

        private void GetSelectedArticle(OITM_Articles article)
        {
            IsModalVisible = false;

            if (SelectedOrder == null) SelectedOrder = new ORDR_SpecialOrder();

            if (SelectedOrder != null)
            {
                var detail = new RDR1_SpecialOrderDetail()
                {
                    ItemCode = article.ItemCode,
                    Price = articleChooserViewModel.ProductPriceDecimal,
                    Quantity = articleChooserViewModel.Quantity,
                    LineTotal = articleChooserViewModel.TotalPriceToDecimal,
                    OITM_Articles = article

                };
                SelectedOrder.RDR1_SpecialOrderDetail.Add(detail);
                articleChooserViewModel.CleanFields();
                DetailsCollection.Add(detail);
                RaisePropertyChanged("DetailsCollection");
                RefreshPriceTotal();
                if (OnSelectedArticle != null) OnSelectedArticle();
            }

        }

        private void Sync(ORDR_SpecialOrder currentOrder)
        {
            if (!WebApiClient.IsAvailableConnection) return;

            // UPLOAD REPORT FILE
            currentOrder.FileName = currentOrder.ReportFileName;

            var pathFie = System.IO.Path.Combine(Config.ReportPath, currentOrder.ReportFileName);

            //if (!WebApiClient.Upload(Config.WhsCode, Path.Combine(Config.ReportPath, pathFie))) return;


            //// Sync Customer Product pircure.
            //if (currentOrder.ATC1_Attachments.Any())
            //{

            //    currentOrder.ATC1_Attachments.Where(f => File.Exists(f.FileName))
            //        .ToList().ForEach(attch => WebApiClient.Upload(Config.WhsCode, attch.FileName));
            //}

            var order = WebApiClient.AddSpecialOrder(currentOrder).Result;

            // update model from . 
            currentOrder = SpecialOrdersHelper.GetOrder(currentOrder.IdSpecialOrder);

            currentOrder.UpdateModelPropertiesFrom(order.Model);
            order.UpdateEntityVersion();
        }

        private void RefreshDetailsCollections()
        {
            IsCleanDetailsRequired();
            RaisePropertyChanged("DetailsCollection");
            RaisePropertyChanged("ColorCovertCollection");
            RaisePropertyChanged("ColorFlowersCollection");
            RaisePropertyChanged("ColorRibbonCollection");
            RaisePropertyChanged("ColorTopsCollection");
            RaisePropertyChanged("CoversCollection");
            RaisePropertyChanged("FlowersCollection");
            RaisePropertyChanged("FillerCollection");
            RaisePropertyChanged("ColorHeightsCollection");
            RaisePropertyChanged("GuirnaldasCollection");
            RaisePropertyChanged("LazasCollection");
        }

        private void IsCleanDetailsRequired()
        {
            if (SelectedOrder == null) return;
            if (currentStyle != selectedOrder.Style ||
                currentCake != selectedOrder.Cake ||
                currentEspecialty != selectedOrder.Especiality)
            {
                // Clean Detals 
                if (!IsReadOnly) SelectedOrder.RDR1_SpecialOrderDetail.Clear();

                // por codigo champero 
                SetCurrentDefaultValuesFor();
            }

            if (currentStyle == null) currentStyle = SelectedOrder.Style;
            if (currentCake == null) currentCake = SelectedOrder.Cake;
            if (currentEspecialty == null) currentEspecialty = SelectedOrder.Especiality;


            //if (currentCake == null && currentEspecialty == null && currentStyle == null)
            //{
            //    currentStyle = SelectedOrder.Style;
            //    currentCake = SelectedOrder.Cake;
            //    currentEspecialty = selectedOrder.Especiality;
            //}
        }

        private void SetCurrentDefaultValuesFor()
        {
            if (selectedOrder == null) return;
            currentStyle = selectedOrder.Style;
            currentCake = selectedOrder.Cake;
            currentEspecialty = selectedOrder.Especiality;
        }

        private bool CanSaveChanges()
        {
            if (SelectedOrder == null) return false;

            bool valor = !IsReadOnly &&
                   (Validate(StylesCollection, SelectedOrder.Style) &&
                    Validate(CoversCollection, SelectedOrder.Cover) &&
                    Validate(LazasCollection, SelectedOrder.Laza) &&
                    Validate(GuirnaldasCollection, SelectedOrder.Guirnalda) &&
                    Validate(EspecialtiesCollection, SelectedOrder.Especiality) &&
                    Validate(CakesCollection, SelectedOrder.Cake) &&
                    Validate(FillerCollection, SelectedOrder.filler) &&
                    Validate(ColorCovertCollection, SelectedOrder.ColorCovert) &&
                    Validate(ColorFlowersCollection, SelectedOrder.ColorFlower) &&
                    Validate(ColorRibbonCollection, SelectedOrder.ColorRibbon) &&
                    Validate(FlowersCollection, SelectedOrder.Flower) &&
                    Validate(ColorTopsCollection, SelectedOrder.ColorTop) &&
                    Validate(ColorHeightsCollection, SelectedOrder.ColorHeight)&&
                    SelectedOrder.RDR1_SpecialOrderDetail.Any() &&
                    !string.IsNullOrEmpty(SelectedOrder.CustomerName) &&
                    !string.IsNullOrEmpty(SelectedOrder.NumAtCard) &&
                    !string.IsNullOrEmpty(SelectedOrder.Dedication) &&
                    !string.IsNullOrEmpty(SelectedOrder.DownPaymentCode)&&
                    !string.IsNullOrEmpty(SelectedOrder.CustomerAddress) &&
                    SelectedOrder.DeliveryTime.HasValue &&
                    SelectedOrder.DocDueDate.HasValue
            );          

            return valor;
        }

        public bool Validate(IEnumerable<object> collection, object property)
        {
            if (collection == null) return true;
            if (!collection.Any()) return true;

            var valor = property != null;
            return valor;
        }

        private bool ValidateDate()
        {
            if (SelectedOrder.DeliveryTime.HasValue && SelectedOrder.DocDueDate.HasValue)
            {
                var fecha = SelectedOrder.DocDueDate.Value.AddHours(SelectedOrder.DeliveryTime.Value.Hour);
                var fecha2 = DateTime.Now.AddHours(Config.HourOrder);
                if (fecha < fecha2)
                {
                    ShowDialog("La fecha y hora de entrega deben ser mayores a " + Config.HourOrder.ToString() + " horas", "Fechas");
                    return false;
                }
                return true;
            }
            return false;
        }

        #endregion

        #region Commands

        public ICommand ShowArticleChooserCommand
        {
            get
            {

                return new RelayCommand(() =>
                {
                    if (articleChooserViewModel == null)
                    {
                        articleChooserViewModel = new ArticleChooserViewModel() { ProductsSources = ProductsFor.SpecialOrders };
                        // conect to actions 
                        articleChooserViewModel.OnSelect += GetSelectedArticle;
                        articleChooserViewModel.OnClose += () => IsModalVisible = false;

                    }
                    articleChooserViewModel.IsFocusedAutoComplete = true;

                    articleChooserViewModel.SpecialOrder = SelectedOrder;
                    articleChooserViewModel.ForceRefresh = true;

                    ShowModal(new ArticleChooser()
                    {
                        DataContext = articleChooserViewModel
                    });


                }, () => !IsReadOnly);
            }
        }

        public ICommand SaveDetailsCommand
        {
            get { return new RelayCommand(SaveNewDetails, CanSaveChanges); }
        }

        public ICommand DeleteSelectedDetailsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SelectedOrder != null && ConfirmDelete())
                    {
                        //SpecialOrdersHelper.DeleteDetail(SelectedSpecialOrderDetails);
                        SelectedOrder.RDR1_SpecialOrderDetail.Remove(SelectedSpecialOrderDetails);
                        DetailsCollection.Remove(SelectedSpecialOrderDetails);
                        RaisePropertyChanged("DetailsCollection");
                    }
                }, () => !IsReadOnly);
            }
        }

        public ICommand EditCurrentCommand
        {
            get { return new RelayCommand(ExecuteEdit, CanExecuteEdit); }
        }

        public ICommand DeleteCurrentComand
        {
            get { return new RelayCommand(ExecuteDelete, CanExecteDelete); }
        }

        public ICommand ProcessCommand
        {
            get { return new RelayCommand(Process, () => !IsReadOnly); }
        }

        public ICommand StyleSelctionChange
        {
            get { return new RelayCommand(RefreshDetailsCollections); }
        }

        public ICommand RefreshArticlesDetailsCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            IsCleanDetailsRequired();
                                            RaisePropertyChanged("DetailsCollection");
                                        });
            }
        }

        public ICommand OnSpecialityChange
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            IsCleanDetailsRequired();
                                            RaisePropertyChanged("FillerCollection");
                                            RaisePropertyChanged("DetailsCollection");
                                        });
            }
        }

        public ICommand SelectPictureCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            var dlg = new Microsoft.Win32.OpenFileDialog();

                                            // Set filter for file extension and default file extension 
                                            //dlg.DefaultExt = ".txt";

                                            dlg.Filter = "Imagenes |*.jpeg;*.png;*.jpg;*.gif" +
                                             "|Todos los Archivos|*.*";

                                            // Display OpenFileDialog by calling ShowDialog method 
                                            var result = dlg.ShowDialog();

                                            // Get the selected file name and display in a TextBox 
                                            if (result.HasValue && result.Value && SelectedOrder != null)
                                            {
                                                // add file to listview.
                                                // dlg.FileName;
                                                var filepath = MoveFileToLocal(dlg.FileName, selectedOrder);

                                                SelectedOrder.ATC1_Attachments.Add(new ATC1_Attachments()
                                                                                    {
                                                                                        FileName = filepath,
                                                                                        FileExt = Path.GetExtension(filepath),

                                                                                    });

                                                RaisePropertyChanged("AttachmentsCollection");
                                            }
                                        }
                                        );
            }
        }

        public ICommand RemoveSelectedPicture
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            if (SelectedOrder != null || SelectedAttach != null)
                                            {
                                                SelectedOrder.ATC1_Attachments.Remove(SelectedAttach);
                                                ContextFactory.ChangeEntityState(SelectedAttach, EntityState.Deleted);
                                            }
                                            RaisePropertyChanged("AttachmentsCollection");
                                        });
            }
        }

        #endregion

        #region Overrides

        public override void RefreshItemSource()
        {
            ForceRefresh = true;
            RaisePropertyChanged("SpecialOrdersCollection");
        }
        public override void ExecuteNewCommand()
        {
            SelectedOrder = new ORDR_SpecialOrder();
            SelectedSpecialOrderDetails = null;
            FormTitle = "Nuevo Pedido Especial";
            ShowDialog(new SpecialOrderView(), this, ResizeMode.CanResize);
        }
        public override void ExecuteEdit()
        {
            IsCleanDetailsRequired();
            RaisePropertyChanged("SelectedOrder");
            RaisePropertyChanged("SpecialOrderDetailsCollection");
            if (SelectedOrder != null)
            {
                // TODO get pending collection 
                // Actualizar colecciones sin relaciones en la tabla SpecialOrder

            }
            FormTitle = "Editar Pedido Especial";

            // Para Evitar Limpiar detalles por validacion IsClean...
            SetCurrentDefaultValuesFor();

            ShowDialog(new SpecialOrderView(), this, ResizeMode.CanResize);
        }
        public override bool CanExecuteEdit()
        {
            return selectedOrder != null;
        }
        public override bool CanExecteDelete()
        {
            return !IsReadOnly;
        }
        public override void ExecuteDelete()
        {
            if (SelectedOrder != null && ConfirmDelete())
            {
                SpecialOrdersHelper.DeleteOrder(SelectedOrder);
                SaveChanges();
                SpecialOrdersCollection.Remove(SelectedOrder);
            }

        }
        public override bool CanExecuteDoProcess()
        {
            return !IsReadOnly;
        }
        public override void ExecuteDoProcess()
        {
            Process();
        }
        public override bool CanViewReport()
        {
            return SelectedOrder != null && selectedOrder.IdSpecialOrder != 0;
        }
        public override void ViewReport()
        {
            var param = new Dictionary<string, string>();

            param.Add("@IdSpecialOrder", selectedOrder.IdSpecialOrder.ToString());

            var data = StoredCallbackProcessor.CallDataSet("SP_INV_PEDIDOS_ES", param);
            //var report = new ReportContainer("rptPedidosEspeciales.rpt", data);
            //ShowDialog(report, this);
        }


        private string MoveFileToLocal(string path, ORDR_SpecialOrder order)
        {
            try
            {
                var extension = Path.GetExtension(path);
                var targetPath = Path.Combine(Config.ReportPath, order.WhsCode + '_' + Guid.NewGuid() + extension);

                File.Copy(path, targetPath);

                return targetPath;
            }
            finally
            {

            }
        }
        #endregion

        #region Private properties
        private ObservableCollection<ORDR_SpecialOrder> orders { get; set; }
        private ORDR_SpecialOrder selectedOrder { get; set; }
        private ObservableCollection<RDR1_SpecialOrderDetail> detailscollection { get; set; }
        private RDR1_SpecialOrderDetail orderDetail { get; set; }
        private ObservableCollection<UCake> cakes { get; set; }
        private ObservableCollection<UCategory> categories { get; set; }
        private ObservableCollection<UColor_Covert> colorCoverts { get; set; }
        private ObservableCollection<UColor_Flower> colorFlowers { get; set; }
        private ObservableCollection<UColor_Ribbon> colorRibbon { get; set; }
        private ObservableCollection<UColor_top> colorTops { get; set; }
        private ObservableCollection<ULaza> colorLazas { get; set; }
        private ObservableCollection<UGuirnalda> colorGuirnaldas { get; set; }
        private ObservableCollection<UCover> covers { get; set; }
        private ObservableCollection<UEspecialty> specialities { get; set; }
        private ObservableCollection<UFlower> flowers { get; set; }
        private ObservableCollection<UStyle> styles { get; set; }
        private ObservableCollection<Ufiller> Fillers { get; set; }
        private ObservableCollection<UColor_Height> colorHeights { get; set; }
        
        private ArticleChooserViewModel articleChooserViewModel;
        private ATC1_Attachments attachment { get; set; }
        private bool isFocusedAddButton { get; set; }
        private UStyle currentStyle { get; set; }
        private UCake currentCake { get; set; }
        private UEspecialty currentEspecialty { get; set; }

        #endregion
    }
}
