using System.IO;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Documents.Model;
using PageOrientation = System.Printing.PageOrientation;

namespace SigiFluent
{
    /// <summary> 
    /// Support Printing Related Methods 
    /// </summary> 
    public static class PrintHelper
    {
        #region ___________ Properties ____________________________________________________________________________________________
        /// <summary> 
        /// Zoom Enumeration to specify how pages are stretched in print and preview 
        /// </summary> 
        public enum ZoomType
        {
            /// <summary> 
            /// 100% of normal size 
            /// </summary> 
            Full,

            /// <summary> 
            /// Page Width (fit so one page stretches to full width) 
            /// </summary> 
            Width,

            /// <summary> 
            /// Page Height (fit so one page stretches to full height) 
            /// </summary> 
            Height,

            /// <summary> 
            /// Display two columsn of pages 
            /// </summary> 
            TwoWide
        };
        #endregion
        #region ___________ Methods _______________________________________________________________________________________________
        /// <summary> 
        /// Print element to a document 
        /// </summary> 
        /// <param name="element">GUI Element to Print</param> 
        /// <param name="dialog">Reference to Print Dialog</param> 
        /// <param name="orientation">Page Orientation (i.e. Portrait vs. Landscape)</param> 
        /// <returns>Destination document</returns> 
        static FixedDocument ToFixedDocument(FrameworkElement element, PrintDialog dialog, PageOrientation orientation = PageOrientation.Portrait, UIElement header = null)
        {
            dialog.PrintTicket.PageOrientation = orientation;
            PrintCapabilities capabilities = dialog.PrintQueue.GetPrintCapabilities(dialog.PrintTicket);
            Size pageSize = new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);
            Size extentSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

            FixedDocument fixedDocument = new FixedDocument();

            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(new Point(0, 0), element.DesiredSize));

            for (double y = 0; y < element.DesiredSize.Height; y += extentSize.Height)
            {
                for (double x = 0; x < element.DesiredSize.Width; x += extentSize.Width)
                {
                    VisualBrush brush = new VisualBrush(element);
                    brush.Stretch = Stretch.None;
                    brush.AlignmentX = AlignmentX.Left;
                    brush.AlignmentY = AlignmentY.Top;
                    brush.ViewboxUnits = BrushMappingMode.Absolute;
                    brush.TileMode = TileMode.None;
                    brush.Viewbox = new Rect(x, y, extentSize.Width, extentSize.Height);

                    PageContent pageContent = new PageContent();
                    FixedPage page = new FixedPage();
                    ((IAddChild)pageContent).AddChild(page);

                    fixedDocument.Pages.Add(pageContent);
                    page.Width = pageSize.Width;
                    page.Height = pageSize.Height;

                    Canvas canvas = new Canvas();                 
                    FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                    FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);
                    canvas.Width = extentSize.Width;
                    canvas.Height = extentSize.Height;
                    canvas.Background = brush;
                    if (header != null) 
                    {
                        page.Children.Add(header);
                    }
                    page.Children.Add(canvas);
                }
            }
            return fixedDocument;
        }

        /// <summary> 
        /// Convert GridView to Printer-Friendly version of a GridView 
        /// </summary> 
        /// <param name="source">Input GridView</param> 
        /// <returns>Printer-Friendly version of source</returns> 
        static GridViewDataControl ToPrintFriendlyGrid(GridViewDataControl source)
        {
            RadGridView grid = new RadGridView();

            grid.ItemsSource = source.ItemsSource;
            grid.RowIndicatorVisibility = Visibility.Collapsed;
            grid.ShowGroupPanel = false;
            grid.CanUserFreezeColumns = false;
            grid.IsFilteringAllowed = false;
            grid.AutoExpandGroups = true;
            grid.AutoGenerateColumns = false;

            foreach (GridViewDataColumn column in source.Columns.OfType<GridViewDataColumn>())
            {
                GridViewDataColumn newColumn = new GridViewDataColumn();
                newColumn.Width = column.ActualWidth;
                newColumn.DisplayIndex = column.DisplayIndex;
                //newColumn.DataMemberBinding = new System.Windows.Data.Binding(column.UniqueName); 
                newColumn.DataMemberBinding = column.DataMemberBinding; // Better to just copy the references to get all the custom formatting 
                newColumn.DataFormatString = column.DataFormatString;
                newColumn.TextAlignment = column.TextAlignment;
                newColumn.Header = column.Header;
                newColumn.Footer = column.Footer;
                grid.Columns.Add(newColumn);
            }

            StyleManager.SetTheme(grid, StyleManager.GetTheme(grid));

            grid.SortDescriptors.AddRange(source.SortDescriptors);
            grid.GroupDescriptors.AddRange(source.GroupDescriptors);
            grid.FilterDescriptors.AddRange(source.FilterDescriptors);

            return grid;
        }

        /// <summary> 
        /// Perform a Print Preview on GridView source 
        /// </summary> 
        /// <param name="source">Input GridView</param> 
        /// <param name="orientation">Page Orientation (i.e. Portrait vs. Landscape)</param> 
        /// <param name="zoom">Zoom Enumeration to specify how pages are stretched in print and preview</param>        
        public static void PrintPreview(GridViewDataControl source, Grid Header, PageOrientation orientation = PageOrientation.Landscape, ZoomType zoom = ZoomType.TwoWide)
        {
            Window window = new Window();
            window.Title = "Print Preview";
            if (!string.IsNullOrWhiteSpace(source.ToolTip as string)) window.Title += " of " + source.ToolTip;
            window.Width = SystemParameters.PrimaryScreenWidth * 0.92;
            window.Height = SystemParameters.WorkArea.Height;
            window.Left = constrain(SystemParameters.VirtualScreenWidth - SystemParameters.PrimaryScreenWidth, 0, SystemParameters.VirtualScreenWidth - 11);
            window.Top = constrain(0, 0, SystemParameters.VirtualScreenHeight - 25);

            DocumentViewer viewer = new DocumentViewer();
            viewer.Document = ToFixedDocument(ToPrintFriendlyGrid(source), new PrintDialog(), orientation, header: Header);
            Zoom(viewer, zoom);
            window.Content = viewer;

            window.ShowDialog();
        }

        /// <summary> 
        /// Constrain val to the range [val_min, val_max] 
        /// </summary> 
        /// <param name="val">Value to be constrained</param> 
        /// <param name="val_min">Minimum that will be returned if val is less than val_min</param> 
        /// <param name="val_max">Maximum that will be returned if val is greater than val_max</param> 
        /// <returns>val in [val_min, val_max]</returns> 
        private static double constrain(double val, double val_min, double val_max)
        {
            if (val < val_min) return val_min;
            else if (val > val_max) return val_max;
            else return val;
        }

        /// <summary> 
        /// Perform a Print on GridView source 
        /// </summary> 
        /// <param name="source">Input GridView</param> 
        /// <param name="showDialog">True to show print dialog before printing</param> 
        /// <param name="orientation">Page Orientation (i.e. Portrait vs. Landscape)</param> 
        /// <param name="zoom">Zoom Enumeration to specify how pages are stretched in print and preview</param> 
        public static void Print(GridViewDataControl source, bool showDialog = true, PageOrientation orientation = PageOrientation.Landscape, ZoomType zoom = ZoomType.TwoWide)
        {
            PrintDialog dialog = new PrintDialog();
            bool? dialogResult = showDialog ? dialog.ShowDialog() : true;

            if (dialogResult == true)
            {
                DocumentViewer viewer = new DocumentViewer();
                viewer.Document = ToFixedDocument(ToPrintFriendlyGrid(source), dialog, orientation);
                Zoom(viewer, zoom);
                dialog.PrintDocument(viewer.Document.DocumentPaginator, "");
            }
        }

        public static void Print(FrameworkElement source, bool showDialog = true, PageOrientation orientation = PageOrientation.Landscape, ZoomType zoom = ZoomType.TwoWide)
        {
            PrintDialog dialog = new PrintDialog();
            bool? dialogResult = showDialog ? dialog.ShowDialog() : true;

            if (dialogResult == true)
            {
                DocumentViewer viewer = new DocumentViewer();
                viewer.Document = ToFixedDocument(source, dialog, orientation);
                Zoom(viewer, zoom);
                dialog.PrintDocument(viewer.Document.DocumentPaginator, "");
            }
        }

        /// <summary> 
        /// Scale viewer to size specified by zoom 
        /// </summary> 
        /// <param name="viewer">Document to zoom</param> 
        /// <param name="zoom">Zoom Enumeration to specify how pages are stretched in print and preview</param> 
        public static void Zoom(DocumentViewer viewer, ZoomType zoom)
        {
            switch (zoom)
            {
                case ZoomType.Height: viewer.FitToHeight(); break;
                case ZoomType.Width: viewer.FitToWidth(); break;
                case ZoomType.TwoWide: viewer.FitToMaxPagesAcross(2); break;
                case ZoomType.Full: break;
            }
        }
        #endregion



        private static void ExportPNGToImage(FrameworkElement element, Stream stream)
        {
            Telerik.Windows.Media.Imaging.ExportExtensions.ExportToImage(element, stream, new PngBitmapEncoder());

        }

        private static RadDocument CreateDocument()
        {
            var doc = new RadDocument();


            return doc;
        }

        private static void BuildDocument()
        {

            var section = new Telerik.Windows.Documents.Model.Section();
            var  paragraph = new Telerik.Windows.Documents.Model.Paragraph();
            var paragraph1 = new Telerik.Windows.Documents.Model.Paragraph();
        }

    } 
}
