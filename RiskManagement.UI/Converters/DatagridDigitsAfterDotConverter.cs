using System;
using System.Globalization;
using System.Windows.Data;

namespace RiskManagement.UI.Converters
{
    public class DatagridDigitsAfterDotConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || !(values[0] is double) || !(values[1] is bool))
                return "";
            double val = (double)values[0];
            if ((bool)values[1])
                return String.Format("{0,0.00}", val);
            return String.Format("{0,0.##}", val);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
