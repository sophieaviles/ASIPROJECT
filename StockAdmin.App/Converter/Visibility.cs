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
    public class BoolToCollapsed : MarkupExtension, IValueConverter
    {
        private static BoolToCollapsed _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new BoolToCollapsed());
        }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueCon = (bool)value;
            return valueCon ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToVisibility : MarkupExtension, IValueConverter {
        private static StringToVisibility _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new StringToVisibility());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueCon = (string)value;
            return string.IsNullOrEmpty(valueCon) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullToCollapsed : MarkupExtension, IValueConverter
    {
        private static NullToCollapsed _converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new NullToCollapsed());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class BoolToCollapsedNeg : MarkupExtension, IValueConverter
    {
        private static BoolToCollapsedNeg _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new BoolToCollapsedNeg());
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueCon = (bool)value;
            return valueCon ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
