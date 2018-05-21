using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SigiFluent.Converter
{
    public class BoolInverted : MarkupExtension, IValueConverter
    {
        private static BoolInverted _converter;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new BoolInverted());
        }

        public object Convert(object value, Type targeType, object parameter, CultureInfo culture)
        {
            var val = (bool) value;
            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool)value;
            return !val;
        }

    }
   

}
