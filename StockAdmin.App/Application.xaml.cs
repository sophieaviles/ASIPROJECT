#region Copyright and License Information

// Fluent Ribbon Control Suite
// http://fluent.codeplex.com/
// Copyright © Degtyarev Daniel, Rikker Serg., Weegen Patrick 2009-2013.  All rights reserved.
// 
// Distributed under the terms of the Microsoft Public License (Ms-PL). 
// The license is available online http://fluent.codeplex.com/license

#endregion

using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using StockAdminContext;
using StockAdminContext.Helpers;
using SigiFluent.Helpers;
using Telerik.Windows.Controls;

namespace SigiFluent
{
    /// <summary>
    /// Entry point of the application
    /// </summary>
    public partial class Application : System.Windows.Application
    {
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {

            // logger Helper guardara toda excepcion generada. // TODO no eliminar.
            AppDomain.CurrentDomain.UnhandledException += StockAdminContext.Helpers.LoggerHelper.OnExceptionRaised;

            StyleManager.ApplicationTheme = new Windows8TouchTheme();

            var culture = new CultureInfo(Config.CurrentCulture);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof (FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)
                    )
                );

            LocalizationManager.DefaultResourceManager = new ResourceManager("Telerik.Windows.Examples.GridView.Localization.Spanish", Assembly.GetCallingAssembly());
            LocalizationManager.Manager = new LocalizationManager { Culture = Thread.CurrentThread.CurrentCulture };
            LocalizationManager.DefaultCulture = culture;

            InitializeApplicationConfig();
        }

        private void InitializeApplicationConfig()
        {
            ConfigInitializer.DirectoryName = "El Rosario SIGI";

            if (!ConfigInitializer.ExistConfig())
            {
                ConfigInitializer.CreateConfig();
            }
        }
    }
}