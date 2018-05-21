using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SigiFluent.ViewModels;

namespace SigiFluent
{
    public static class ViewModelManager
    {
        static ViewModelManager()
        {
               
        }
         
        public static Tmodel GetViewModel<Tmodel,Tview>() where Tmodel : new() where Tview : UserControl ,new()
        {
            var view = GetView<Tmodel, Tview>();

            return (Tmodel) view.DataContext;
        }

        public static UserControl GetView<Tmodel,Tview>()where Tmodel:new () where Tview : UserControl,new()
        {
            UserControl view;

            if(!Views.TryGetValue(typeof(Tmodel),out view ))
            {
                view = new Tview();
                view.DataContext = new Tmodel();
                Views.Add(typeof(Tmodel),view);
            }
            
            return view;
        }
         
        public static BaseViewModel GetCurretBaseModel()
        {
            return CurrentBaseModel;
        }

        public static void LoadToGrid<TModel,Tview>() where TModel : new()where Tview :UserControl, new()
        {
            var view = GetView<TModel, Tview>();
             
            CleanGrid();
             
            CurrentBaseModel = view.DataContext as BaseViewModel;

         
            mainWindow.MainContainer.Children.Add(view);

            if (OnLoadViewToGrid != null) OnLoadViewToGrid(typeof(TModel));
        }

        public static void LoadReport(UserControl view)
        {
            CleanGrid();
            mainWindow.MainContainer.Children.Add(view);
        }

        public static void LoadActionBar<TActionBar>(TActionBar bar, string seriesCode="") where TActionBar : UserControl
        {
            if (ActionBarViewModel == null) ActionBarViewModel = new ActionBarViewModel();
            ActionBarViewModel.SeriesCode = seriesCode;
            bar.DataContext = ActionBarViewModel;
            mainWindow.ActionBarContainer.Content = bar;
        }
         

        public static void CleanGrid()
        {
            mainWindow.MainContainer.Children.Clear();
        }

         

        public static void ShowModal<Tmodel, Tview>()where Tmodel : new() where Tview : UserControl, new()
        {
            
            var view = GetView<Tmodel, Tview>();
            mainWindow.modalBorder.Margin = new Thickness(25, 40, 25, 40);
            mainWindow.modalMain.Visibility = Visibility.Visible;
            mainWindow.modalContainer.Children.Clear();
            mainWindow.modalContainer.Children.Add(view);
        }

        public static void ShowModal<Tmodel, Tview>(Tmodel model)where Tview : UserControl, new()
        {
            var view = new Tview();
            view.DataContext = model;
            mainWindow.modalMain.Visibility = Visibility.Visible;
            mainWindow.modalContainer.Children.Add(view); 
        }

        public static void ShowModal< Tview>(  Tview view) where Tview :UserControl  
        {
            mainWindow.modalMain.Visibility = Visibility.Visible;
            mainWindow.modalBorder.Margin = new Thickness(10,10,10,10);
            mainWindow.modalContainer.Children.Add(view);
        }

        public static void ShowModal<Tview>(Tview view, Thickness margin) where Tview : UserControl
        {
            mainWindow.modalMain.Visibility = Visibility.Visible;
            mainWindow.modalBorder.Margin = margin;
            mainWindow.modalContainer.Children.Add(view);
        }

        public static void ShowDialog<Tmodel, Tview>() where Tview : UserControl, new() where Tmodel : new()
        {
            var view = new Tview();
            view.DataContext = new Tmodel();
            mainWindow.MainContainer.Children.Clear();
            mainWindow.MainContainer.Children.Add(view); 
        }

        public static void CloseModal()
        {
            if (IsLockedModal) return;
            mainWindow.modalContainer.Children.Clear();
            mainWindow.modalMain.Visibility = Visibility.Collapsed;
        }

        //Close Locked Modal 
        public static void CloseProcessLoader()
        {
            IsLockedModal = false;
            GetCurretBaseModel().FormTitle = string.Empty;
            CloseModal();
        }

        public static bool CanCloseMainModal()
        {
            if (CurrentBaseModel == null) return true;
            return  CurrentBaseModel != null && !CurrentBaseModel.IsModalVisible; 
        }

        public static MainWindow mainWindow { get; set; }

        public static BaseViewModel CurrentBaseModel { get; set; }

        public static ActionBarViewModel ActionBarViewModel { get; set; }
         
        private static  Dictionary<Type,UserControl> Views = new Dictionary<Type, UserControl>();
        

        public static event Action<Type> OnLoadViewToGrid;

        public static bool IsLockedModal { get; set; }
       
    }


    
    
}
