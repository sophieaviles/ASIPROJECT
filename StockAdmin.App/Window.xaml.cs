#region Copyright and License Information

// Fluent Ribbon Control Suite
// http://fluent.codeplex.com/
// Copyright © Degtyarev Daniel, Rikker Serg., Weegen Patrick 2009-2013.  All rights reserved.
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://fluent.codeplex.com/license

#endregion

using System;
using SigiFluent.ViewModels;
using Telerik.Windows.Controls;
using System.Windows;
namespace SigiFluent
{
    /// <summary>
    /// Represents the main window of the application
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ToolBarViewModel();
            ViewModelManager.mainWindow = this;

        }

      

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (OnApplicationLoaded != null) OnApplicationLoaded();
        }

        public static event Action OnApplicationLoaded;

      
    }
}