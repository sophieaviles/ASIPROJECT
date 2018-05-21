using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SigiFluent.ViewModels;

namespace SigiFluent.Views
{
    /// <summary>
    /// Interaction logic for WindowContentView.xaml
    /// </summary>
    public partial class WindowContentView : UserControl
    {
        public WindowContentView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (BaseViewModel) this.DataContext;
            if(vm!=null) vm.IsModalVisible = false;
        }

        //private void WindowContentView_OnClosed(object sender, EventArgs e)
        //{
        //    var viewModel = ((BaseViewModel) ContentView.DataContext);
        //    viewModel.IsModalVisible = false;

        //    this.modalContainer.Children.Clear();

        //    viewModel.VerifyPendingChanges();
        //}
    }
}
