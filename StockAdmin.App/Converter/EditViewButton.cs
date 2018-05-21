using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace SigiFluent.Converter
{
    public class EditViewButtonLabel: MarkupExtension, IValueConverter
    {
        private static EditViewButtonLabel _converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new EditViewButtonLabel());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TransferRequestStatus StateL
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
