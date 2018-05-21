﻿using System;
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

namespace SigiFluent.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SpecialOrderReport.xaml
    /// </summary>
    public partial class SpecialOrderReport : UserControl
    {
        public SpecialOrderReport()
        {
            InitializeComponent();
        }

        public SpecialOrderReport(ICommand saveToPdfCommand)
        {
            InitializeComponent();
            this.ExportPDFbutton.Command = saveToPdfCommand;
        }
    }
}
