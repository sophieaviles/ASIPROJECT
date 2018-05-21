using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SigiApi;
using SigiApi.Helpers;
using SigiFluent.Helpers;

namespace SigiFluent.Views.Reports
{
    /// <summary>
    /// Interaction logic for ReportContainer.xaml
    /// </summary>
    public partial class ReportContainer : UserControl
    {
        public ReportContainer(string fileName, string spName)
        {
            InitializeComponent();
            this.reportName = ApplicationPath.GetFullPath(@"Views\Reports\" + fileName);
            this.StoredProcedureName = spName;
            data = null;
        }

        public ReportContainer(string filename, DataSet dataset)
        {
            InitializeComponent();
            this.reportName = ApplicationPath.GetFullPath(@"Views\Reports\" + filename);
            data = dataset;
        }


        private void CrystalReportsViewer1_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                report = new ReportDocument();

                report.Load(reportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);

                if (data == null)
                    report.SetDataSource(StoredCallbackProcessor.CallDataSet(StoredProcedureName).Tables[0]);

                else report.SetDataSource(data.Tables[0]);
                crystalReportsViewer1.ViewerCore.ReuseParameterWhenRefresh = true;

                report.Refresh();

                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex);
                MessageBox.Show(ViewModelManager.mainWindow, "Error Al cargar Reporte, verifique configuracion",
                    "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void Export(string filename)
        {
            try
            {


                var report = new ReportDocument();

                report.Load(reportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault);

                if (data == null)
                    report.SetDataSource(StoredCallbackProcessor.CallDataSet(StoredProcedureName).Tables[0]);

                else report.SetDataSource(data.Tables[0]);
                // crystalReportsViewer1.ViewerCore.ReuseParameterWhenRefresh = true;

                report.Refresh();

                report.ExportToDisk(ExportFormatType.PortableDocFormat, System.IO.Path.Combine(Config.ReportPath, filename));
                //crystalReportsViewer1.ViewerCore.ReportSource = report;

                report.Database.Dispose();
                report.Close();
                report.Dispose();

                this.crystalReportsViewer1.ViewerCore.Dispose();
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteLog(ex);
                MessageBox.Show(ViewModelManager.mainWindow, "Error Al cargar Reporte, verifique configuracion",
                    "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                GC.Collect();
            }
        }


       

    private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                report.Database.Dispose();
                report.Close();
                report.Dispose();

                this.crystalReportsViewer1.ViewerCore.Dispose(); 
            }
            catch (Exception ex)
            {

            }
            finally
            {
                GC.Collect();    
            }
     }
   
        public string reportName { get; set; }
        public string StoredProcedureName { get; set; }

        public ReportDocument report { get; set; }

        public DataSet data { get; set; }
    }
}
