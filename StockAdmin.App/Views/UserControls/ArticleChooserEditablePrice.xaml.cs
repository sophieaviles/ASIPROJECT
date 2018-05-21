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

namespace SigiFluent.Views.SubControls
{
    /// <summary>
    /// Interaction logic for ArticleChooserEditablePrice.xaml
    /// </summary>
    public partial class ArticleChooserEditablePrice : UserControl
    {
        public ArticleChooserEditablePrice()
        {
            InitializeComponent();
            Loaded += (sender, args) => ArticleAutoCompleteBox.Focus();
        }
    }
}
