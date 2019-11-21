using RiskManagement.Model;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace RiskManagement.UI.Converters
{
    public class RiskToListItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TextBlock block = new TextBlock();
            block.TextTrimming = System.Windows.TextTrimming.CharacterEllipsis;
            block.Text = ((Risk)value).RiskName;
            block.Width = 180;
            block.ToolTip = block.Text;
            return block;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
