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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SigiFluent.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ArticleChooser.xaml
    /// </summary>
    public partial class ArticleChooser : UserControl
    {
        public ArticleChooser()
        {
            InitializeComponent();
            this.Loaded += (sender, args) => this.ArticleAutoCompleteBox.Focus();
        }

        private void TxtQty_OnGotFocus(object sender, RoutedEventArgs e)
        {
            txtQty.SelectAll();
        }

        private void TxtPrice_OnGotFocus(object sender, RoutedEventArgs e)
        {
            txtPrice.SelectAll();
        }
    }
}
