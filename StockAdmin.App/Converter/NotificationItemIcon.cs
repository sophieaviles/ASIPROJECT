using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using SigiFluent.Helpers;

namespace SigiFluent.Converter
{
    public class NotificationItemIcon : MarkupExtension, IValueConverter
    {
        public NotificationItemIcon()
        {
            
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new NotificationItemIcon());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value.ToString();
            var path = "";
            

            if (val.ToLower() == "completo")  path= ApplicationPath.GetPathByFileNameFromResources("check-30.png");
            if (val.ToLower() == "pendiente") path= ApplicationPath.GetPathByFileNameFromResources("attention-30.png");
            
            return string.Format("/SigiFluent;component/{0}", path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static NotificationItemIcon _converter;
    }
}
