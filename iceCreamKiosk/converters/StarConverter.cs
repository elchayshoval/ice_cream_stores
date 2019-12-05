using BE;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace iceCreamKiosk.converters
{
    class StarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = (int)((Enums.Stars)value);
            return result+1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enums.Stars result = (Enums.Stars)((int)value);
            return result-1;
        }
    }
}
