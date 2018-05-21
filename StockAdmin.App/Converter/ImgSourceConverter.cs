using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
//using SAPBusinessObjects.WPF.Viewer;

namespace SigiFluent.Converter
{
   public  class ImgSourceConverter:MarkupExtension,IValueConverter
    {
       public ImgSourceConverter() { }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //return converter ?? (converter = new ImageSourceConverter());
            return null;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;

            return GetImageFromPath(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        

        private static object GetImageFromPath(string path )
        {
            BitmapImage bitmap;

            try
            {
                if (!images.TryGetValue(path, out bitmap))
                {
                    bitmap = new BitmapImage();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bitmap.BeginInit();
               

                    var uri = new Uri(path);
                    bitmap.UriSource = uri;
                    bitmap.EndInit();

                        //Add thumb Format Name 
                        images.Add(path, bitmap);
                        return bitmap;
                    
                }
                return bitmap;
            }
            catch (Exception ex)
            {
                //TODO log This. 
                return null;
            }
        }
         


       
        private static readonly Dictionary<string, BitmapImage> images = new Dictionary<string, BitmapImage>();

       // public static ImageSourceConverter converter=null;
    }
}
