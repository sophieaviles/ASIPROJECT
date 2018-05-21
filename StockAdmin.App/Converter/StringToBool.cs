using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace SigiFluent.Converter
{
    class StringToBool : MarkupExtension, IValueConverter
    {
        public StringToBool() { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ?? (converter = new StringToBool());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value.ToString();
            if (string.IsNullOrEmpty(val)) return false;
            return (val == "Y")
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static CollectionPriceConverter converter;
    }
}
