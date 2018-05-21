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
using SigiFluent.ViewModels;

namespace SigiFluent.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LoginSection.xaml
    /// </summary>
    public partial class LoginSection : UserControl
    {
        public LoginSection()
        {
            ToolBarViewModel.OnLogout += () => PassBox.Password = string.Empty;
            InitializeComponent();
        }
    }
}
