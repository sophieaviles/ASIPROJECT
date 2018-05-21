using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
//using Microsoft.Reporting.WinForms;
using StockAdminContext;
using StockAdminContext.Helpers;
using StockAdminContext.Models;
using SigiFluent.ViewModels;
using SigiFluent.Views;
using SigiFluent.Views.UserControls;

namespace SigiFluent.Helpers
{
    public static class PDFGenerator
    {

        public static void SaveSpecialOrderToFile(ORDR_SpecialOrder specialORder, string fullFilePath)
        {

            var dataManager = new ReportDataSourceManager(specialORder);

            //ReportViewer viewer = new ReportViewer();

            //viewer.ProcessingMode = ProcessingMode.Local;
            //viewer.LocalReport.Refresh();
            //viewer.LocalReport.ReportPath = ApplicationPath.GetFullPath(@"Views\UserControls\SpecialOrderDocument.rdlc");


            //dataManager.GetAllDataSources().ForEach(data => viewer.LocalReport.DataSources.Add(data));

            //Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            DataSet dsGrpSum,
                dsActPlan,
                dsProfitDetails,
                dsProfitSum,
                dsSumHeader,
                dsDetailsHeader,
                dsBudCom = null;

            //byte[] bytes = viewer.LocalReport.Render(
            //    "PDF", null, out mimeType, out encoding, out extension,
            //    out streamIds, out warnings);
            //byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          

            var path = ApplicationPath.GetFullPath(fullFilePath);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                //fs.Write(bytes, 0, bytes.Length);
            }

        }

        public static void BuildSpecialOrderPdf(ORDR_SpecialOrder order, ICommand savePdfCommand)
        {
            //var control = new SpecialOrderReport(savePdfCommand);

            //control.rViewer.LocalReport.ReportPath =
            //    ApplicationPath.GetFullPath(@"Views\UserControls\SpecialOrderDocument.rdlc");

            //var dataManager = new ReportDataSourceManager(order);

            //dataManager.GetAllDataSources().ForEach(data => control.rViewer.LocalReport.DataSources.Add(data));

            //control.rViewer.ZoomPercent = 100;
            //control.rViewer.RefreshReport();

            //var dialog = new WindowContentView();
            //dialog.ContentView.Children.Add(control);

            //if (this.CloseAction == null)
            //    this.CloseAction = new Action(() => dialog.Close());
            
        }

        private class ReportDataSourceManager
        {

            public ReportDataSourceManager(ORDR_SpecialOrder specialORder)
            {

               // DataSourceCache = BuilDataSource(specialORder);

            }

            //public ReportDataSource GetDataSource(string DataSourceName)
            //{
            //    ReportDataSource data;

            //    if (DataSourceCache.TryGetValue(DataSourceName, out data))
            //    {
            //        return data;
            //    }

            //    // TODO handle data Empty
            //    return (data = new ReportDataSource());
            //}

            //public List<ReportDataSource> GetAllDataSources()
            //{
            //    return DataSourceCache.Select(d => d.Value).ToList();
            //}

            //private Dictionary<string, ReportDataSource> BuilDataSource(ORDR_SpecialOrder specialORder)
            //{


            //    var source = new Dictionary<string, ReportDataSource>();

            //    var commonproperties = new List<ExportItem>()
            //                           {
            //                               GetPropertyItem("Cliente :", specialORder.CustomerName),
            //                               GetPropertyItem("Telefono :", specialORder.CustomerPhone),
            //                               GetPropertyItem("Direccion :", specialORder.CustomerAddress),
            //                               GetPropertyItem("", ""), // EMPTY ROW,
            //                               GetPropertyItem("Estilo",
            //                                   specialORder.Style != null ? specialORder.Style.Name : string.Empty),
            //                               GetPropertyItem("Cobertura",
            //                                   specialORder.Cover != null ? specialORder.Cover.Name : string.Empty),
            //                               GetPropertyItem("Especialidad",specialORder.Especiality!=null?
            //                                   specialORder.Especiality.Name : string.Empty),
            //                               GetPropertyItem("Torta",
            //                                   specialORder.Cake != null ? specialORder.Cake.Name : string.Empty),
            //                           };

            //    var articles =
            //        specialORder.RDR1_SpecialOrderDetail.Select(
            //            p => GetPropertyItem(p.OITM_Articles.ItemName, p.Price.ToString()));



            //    var styles = new List<ExportItem>()
            //                 {
            //                     GetPropertyItem("Fecha de Elaboracion :",specialORder.DocDate.HasValue? specialORder.DocDate.Value.ToShortDateString():string.Empty),
            //                     GetPropertyItem("Fecha De Entrega :",specialORder.DocDueDate.HasValue? specialORder.DocDueDate.Value.ToShortDateString():string.Empty),
            //                     GetPropertyItem("Relleno",specialORder.filler!=null? specialORder.filler.Name:string.Empty),
            //                     GetPropertyItem("Color de Cobertura,Crema o Combinaciones",specialORder.ColorCovert!=null? specialORder.ColorCovert.Name:string.Empty),
            //                     GetPropertyItem("Color de Flores: ",specialORder.Flower!=null? specialORder.Flower.Name:string.Empty),
            //                     GetPropertyItem("Color de flores medianas y pequeñas (Belfort)",specialORder.ColorFlower!=null? specialORder.ColorFlower.Name:string.Empty),
            //                     GetPropertyItem("Colorde listón/Laza",specialORder.ColorRibbon!=null?  specialORder.ColorRibbon.Name:string.Empty),
            //                     GetPropertyItem("Color de TOP (Lectura Divina)  Fuente de Fe, Bendición del Cielo)",specialORder.ColorTop!=null? specialORder.ColorTop.Name:string.Empty),
            //                     GetPropertyItem("Color de Altura (Lleno de orgullo, Fuente de Fe, Bendición del Gielo)",specialORder.ColorHeight!=null ? specialORder.ColorHeight.Name:string.Empty),
            //                     GetPropertyItem("Observaciones:",specialORder.Comments),
            //                     GetPropertyItem("Dedicatoria: ",specialORder.Dedication),
            //                 };
            //    source.Add("SpecialOrderProperties", new ReportDataSource("SpecialOrderProperties", commonproperties.Union(styles)));
            //    source.Add("SpecialOrderArticles", new ReportDataSource("SpecialOrderArticles", articles));
            //    source.Add("SpecialOrderStyles", new ReportDataSource("SpecialOrderStyles", styles));
            //    //source.Add(SideboardSource, new ReportDataSource(SideboardSource, sideBoardCards));
            //    //source.Add(DeckRightDataSource, new ReportDataSource(DeckRightDataSource, right));
            //    return source;
            //}

            private ExportItem GetPropertyItem(string propertyName, string propertyvalue)
            {
                return new ExportItem()
                       {
                           Name = propertyName,
                           Value = propertyvalue,


                       };
            }

           // private Dictionary<string, ReportDataSource> DataSourceCache;
            private string datasourceName;

        }

        
    }
}
