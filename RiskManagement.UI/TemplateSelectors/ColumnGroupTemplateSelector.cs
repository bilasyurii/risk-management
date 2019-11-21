using RiskManagement.Model;
using System.Windows;
using System.Windows.Controls;

namespace RiskManagement.UI.TemplateSelectors
{
    public class ColumnGroupTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            ProbabilityTableRow row = item as ProbabilityTableRow;
            if(element != null && row != null)
            {
                if (row.Guesses.IsText)
                    return element.FindResource("textCGTemplate") as DataTemplate;
                return element.FindResource("datagridCGTemplate") as DataTemplate;
            }
            return null;
        }
    }
}
