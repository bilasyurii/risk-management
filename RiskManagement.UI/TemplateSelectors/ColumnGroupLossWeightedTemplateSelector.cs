using RiskManagement.Model;
using System.Windows;
using System.Windows.Controls;

namespace RiskManagement.UI.TemplateSelectors
{
    public class ColumnGroupLossWeightedTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            LossTableRow row = item as LossTableRow;
            if (element != null && row != null)
            {
                if (row.Guesses.IsText)
                    return element.FindResource("textWeightedCGTemplate") as DataTemplate;
                return element.FindResource("datagridWeightedCGTemplate") as DataTemplate;
            }
            return null;
        }
    }
}
